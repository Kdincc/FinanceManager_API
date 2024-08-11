using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Task11.Application.ExpenseTypes;
using Task11.Application.IncomeFinanceOperations;
using Task11.Application.IncomeTypes;
using Task11.Contracts.ExpenseType;
using Task11.Contracts.IncomeFinanceOperation;
using Task11.Contracts.IncomeType;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;
using Task11.Infrastructure.Persistence;
using Task11.Presentation;
using Task11.Presentation.ApiRoutes;
using Task11.Tests.Integration.JsonConverters;
using Task11.Domain.Common.Сonstants;
using Task11.Contracts.ExpenseFinanceOperation;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Application.ExpenseFinanceOperations;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Common.DTOs;
using Task11.Contracts.Reports;
using System.Globalization;
using Task11.Application.Reports.PeriodReport;

namespace Task11.Tests.Integration
{
    public class ControllersTests(FinanceWebApplicationFactory factory) : IntegrationTestBase(factory)
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            Converters = 
            { 
                new IncomeTypesResultConverter(), 
                new ExpenseTypesResultConverter(),
                new IncomeFinanceOperationResultConverter(),
                new ExpenseFinanceOperationConverter(),
                new DailyReportConverter(),
                new PeriodReportConverter()
            }
        };

        private void Initialize()
        {
            _factory.ResetDatabase();
        }

        #region IncomeTypesControllerTests

        [Fact]
        public async Task GetIncomeTypes_ReturnsSuccessStatusCode_ReturnsAllIncomeTypes()
        {
            // Arrange
            Initialize();
            IEnumerable<IncomeType> incomeTypes =
            [
                new(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1"),
                new(IncomeTypeId.CreateUniq(), "IncomeType2", "Description2"),
                new(IncomeTypeId.CreateUniq(), "IncomeType3", "Description3")
            ];
            IEnumerable<IncomeTypesResult> expected = incomeTypes.Select(it => new IncomeTypesResult(it));

            // Act
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.AddRange(incomeTypes);
                db.SaveChanges();
            }
            var response = await _client.GetAsync(Routes.IncomeType.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<IncomeTypesResult>>(_jsonOptions);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateIncomeType_ValidRequest_ReturnsOk_AddedToDb()
        {
            //Arrange
            Initialize();
            var request = new CreateIncomeTypeRequest("IncomeType1", "Description1");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.IncomeType.Create, request);
            var result = await response.Content.ReadFromJsonAsync<IncomeTypesResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateIncomeType_InvalidRequest_DuplicateIncomeType_ReturnsConflict()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "Income", "Description");
            var request = new CreateIncomeTypeRequest("Income", "Description");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PostAsJsonAsync(Routes.IncomeType.Create, request);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            problem.Title.Should().Be(Errors.IncomeType.DuplicateIncomeType.Description);
        }

        [Fact]
        public async Task CreateIncomeType_InvalidRequestValidation_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new CreateIncomeTypeRequest("", "");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.IncomeType.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteIncomeType_ValidRequest_ReturnsOk_RemovedFromDb()
        {
            // Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.SaveChanges();
            }
            var request = new DeleteIncomeTypeRequest(incomeType.Id.ToString());

            // Act
            using var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteIncoemType_InvalidId_ReturnsNotFound()
        {
            // Arrange
            Initialize();
            var request = new DeleteIncomeTypeRequest(Guid.NewGuid().ToString());

            // Act
            using var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteIncomeType_InvalidRequestValidation_ReturnsBadRequest()
        {
            // Arrange
            Initialize();
            var request = new DeleteIncomeTypeRequest("adsgtwreq5y5346");

            // Act
            using var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateIncomeType_ValidRequest_ReturnsOk()
        {
            // Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var updatedIncomeType = new IncomeType(incomeType.Id, "IncomeType2", "Description2");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.SaveChanges();
            }
            var request = new UpdateIncomeTypeRequest(incomeType.Id.ToString(), "IncomeType2", "Description2");

            // Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeType.Update, request);
            var result = await response.Content.ReadFromJsonAsync<IncomeTypesResult>(_jsonOptions);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo(new IncomeTypesResult(updatedIncomeType));
        }

        [Fact]
        public async Task UpdateIncomeType_ValidRequest_SameNameAndDescription_ReturnsConflict()
        {
            // Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType", "Description");
            var incomeTypeToUpdate = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var request = new UpdateIncomeTypeRequest(incomeTypeToUpdate.Id.ToString(), "IncomeType", "Description");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.AddRange(incomeTypeToUpdate, incomeType);
                db.SaveChanges();
            }

            // Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeType.Update, request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task UpdateIncomeType_InvalidId_ReturnsNotFound()
        {
            // Arrange
            Initialize();
            var request = new UpdateIncomeTypeRequest(Guid.NewGuid().ToString(), "IncomeType1", "Description1");

            // Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeType.Update, request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateIncomeType_InvalidRequestValidation_ReturnsBadRequest()
        {
            // Arrange
            Initialize();
            var request = new UpdateIncomeTypeRequest("adsgtwreq5y5346", "", "");

            // Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeType.Update, request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region ExpenseTypesControllerTests

        [Fact]
        public async Task GetExpenseTypes_ReturnsSuccessStatusCode_ReturnsAllExpenseTypes()
        {
            // Arrange
            Initialize();
            IEnumerable<ExpenseType> expenseTypes =
            [
                new(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1"),
                new(ExpenseTypeId.CreateUniq(), "ExpenseType2", "Description2"),
                new(ExpenseTypeId.CreateUniq(), "ExpenseType3", "Description3")
            ];
            IEnumerable<ExpenseTypesResult> expected = expenseTypes.Select(it => new ExpenseTypesResult(it));
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.AddRange(expenseTypes);
                await db.SaveChangesAsync();
            }

            // Act
            var response = await _client.GetAsync(Routes.ExpenseType.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseTypesResult>>(_jsonOptions);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetExpenseTypes_NoneExpenseTypes_ReturnsSuccessStatusCode_ReturnsEmptyCollection()
        {
            // Arrange
            Initialize();

            // Act
            var response = await _client.GetAsync(Routes.ExpenseType.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseTypesResult>>(_jsonOptions);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateExpenseType_ValidRequest_ReturnsOk_AddedToDb()
        {
            //Arrange
            Initialize();
            var request = new CreateExpenseTypeRequest("ExpenseType1", "Description1");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.ExpenseType.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateExpenseType_ValidRequest_SameExpenseTypeAlreadeExists_ReturnsConflict()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var request = new CreateExpenseTypeRequest("ExpenseType1", "Description1");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PostAsJsonAsync(Routes.ExpenseType.Create, request);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            problem.Title.Should().Be(Errors.ExpenseType.DuplicateExpenseType.Description);
        }

        [Fact]
        public async Task CreateExpenseType_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new CreateExpenseTypeRequest("", "");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.ExpenseType.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateExpenseType_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var request = new UpdateExpenseTypeRequest(expenseType.Id.ToString(), "ExpenseType2", "Description2");
            var updatedExpenseType = new ExpenseType(expenseType.Id, "ExpenseType2", "Description2");
            var expected = new ExpenseTypesResult(updatedExpenseType);
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseType.Update, request);
            var actual = await response.Content.ReadFromJsonAsync<ExpenseTypesResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateExpenseType_ValidRequest_SameExpenseTypeExists_ReturnsConflict()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType", "Description");
            var expenseTypeToUpdate = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var request = new UpdateExpenseTypeRequest(expenseTypeToUpdate.Id.ToString(), "ExpenseType", "Description");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.AddRange(expenseTypeToUpdate, expenseType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseType.Update, request);
            var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            result.Title.Should().Be(Errors.ExpenseType.DuplicateExpenseType.Description);
        }

        [Fact]
        public async Task UpdateExpenseType_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new UpdateExpenseTypeRequest(Guid.NewGuid().ToString(), "ExpenseType1", "Description1");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseType.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateExpenseType_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new UpdateExpenseTypeRequest("adsgtwreq5y5346", "", "");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseType.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteExpenseType_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var expected = new ExpenseTypesResult(expenseType);
            var request = new DeleteExpenseTypeRequest(expenseType.Id.ToString());
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.SaveChanges();
            }

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var actual = await response.Content.ReadFromJsonAsync<ExpenseTypesResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteExpenseType_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new DeleteExpenseTypeRequest(Guid.NewGuid().ToString());

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            problem.Title.Should().Be(Errors.ExpenseType.ExpenseTypeNotFound.Description);
        }

        [Fact]
        public async Task DeleteExpenseType_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new DeleteExpenseTypeRequest("adsgtwreq5y5346");

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseType.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region IncomeFinanceOperationControllerTests

        [Fact]
        public async Task CreateIncomeFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var request = new CreateIncomeFinanceOperationRequest("2003-11-28", incomeType.Id.ToString(), 100, "name");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PostAsJsonAsync(Routes.IncomeFinanceOperation.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateIncomeFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new CreateIncomeFinanceOperationRequest("", "", -100, "");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.IncomeFinanceOperation.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetIncomeFinanceOperations_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var incomeFinanceOperation = new IncomeFinanceOperation(IncomeFinanceOperationId.CreateUniq(), DateOnly.Parse("2003-11-28"), incomeType.Id, Amount.Create(100), "name");
            IEnumerable<IncomeFinanceOperationResult> expected = [new IncomeFinanceOperationResult(incomeFinanceOperation)];
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.IncomeFinanceOperations.Add(incomeFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var response = await _client.GetAsync(Routes.IncomeFinanceOperation.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<IncomeFinanceOperationResult>>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetIncomeFinanceOperations_NoneIncomeFinanceOperations_ReturnsOk()
        {
            //Arrange
            Initialize();

            //Act
            var response = await _client.GetAsync(Routes.IncomeFinanceOperation.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<IncomeFinanceOperationResult>>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteIncomeFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var incomeFinanceOperation = new IncomeFinanceOperation(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(100), "name");
            var expected = new IncomeFinanceOperationResult(incomeFinanceOperation);
            var request = new DeleteIncomeFinanceOperationRequest(incomeFinanceOperation.Id.ToString());
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.IncomeFinanceOperations.Add(incomeFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var actual = await response.Content.ReadFromJsonAsync<IncomeFinanceOperationResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteIncomeFinanceOperation_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new DeleteIncomeFinanceOperationRequest(Guid.NewGuid().ToString());

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            problem.Title.Should().Be(Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound.Description);
        }

        [Fact]
        public async Task DeleteIncomeFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new DeleteIncomeFinanceOperationRequest("adsgtwreq5y5346");

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.IncomeFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var incomeFinanceOperation = new IncomeFinanceOperation(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(100), "name");
            var updatedIncomeFinanceOperation = new IncomeFinanceOperation(incomeFinanceOperation.Id, DateOnly.ParseExact("2003-11-29", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(200), "name2");
            var expected = new IncomeFinanceOperationResult(updatedIncomeFinanceOperation);
            var request = new UpdateIncomeFinanceOperationRequest(incomeFinanceOperation.Id.ToString(), "name2", incomeType.Id.ToString(), 200, "2003-11-29");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.IncomeFinanceOperations.Add(incomeFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeFinanceOperation.Update, request);
            var actual = await response.Content.ReadFromJsonAsync<IncomeFinanceOperationResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperation_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new UpdateIncomeFinanceOperationRequest(Guid.NewGuid().ToString(), "name", Guid.NewGuid().ToString(), 100, "2003-11-28");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeFinanceOperation.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new UpdateIncomeFinanceOperationRequest("adsgtwreq5y5346", "", "", -100, "");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeFinanceOperation.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperation_InvalidIncomeTypeId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var incomeFinanceOperation = new IncomeFinanceOperation(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(100), "name");
            var request = new UpdateIncomeFinanceOperationRequest(incomeFinanceOperation.Id.ToString(), "name", Guid.NewGuid().ToString(), 100, "2003-11-28");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.IncomeFinanceOperation.Update, request);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            problem.Title.Should().Be(Errors.IncomeType.IncomeTypeNotFound.Description);
        }

        #endregion

        #region ExpenseFinanceOperationControllerTests

        [Fact]
        public async Task CreateExpenseFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var request = new CreateExpenseFinanceOperationRequest("2003-11-28", expenseType.Id.ToString(), 100, "name");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PostAsJsonAsync(Routes.ExpenseFinanceOperation.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateExpenseFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new CreateExpenseFinanceOperationRequest("", "", -100, "");

            //Act
            var response = await _client.PostAsJsonAsync(Routes.ExpenseFinanceOperation.Create, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteExpenseFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var expenseFinanceOperation = new ExpenseFinanceOperation(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(100), "name");
            var expected = new ExpenseFinanceOperationResult(expenseFinanceOperation);
            var request = new DeleteExpenseFinanceOperationRequest(expenseFinanceOperation.Id.ToString());
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.ExpenseFinanceOperations.Add(expenseFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var actual = await response.Content.ReadFromJsonAsync<ExpenseFinanceOperationResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteExpenseFinanceOperation_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new DeleteExpenseFinanceOperationRequest(Guid.NewGuid().ToString());

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            problem.Title.Should().Be(Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound.Description);
        }

        [Fact]
        public async Task DeleteExpenseFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new DeleteExpenseFinanceOperationRequest("adsgtwreq5y5346");

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Routes.ExpenseFinanceOperation.Delete)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetExpenseFinanceOperations_ReturnsOk_ReturnsAll()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var expenseFinanceOperation = new ExpenseFinanceOperation(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(100), "name");
            IEnumerable<ExpenseFinanceOperationResult> expected = [new ExpenseFinanceOperationResult(expenseFinanceOperation)];
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.ExpenseFinanceOperations.Add(expenseFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var response = await _client.GetAsync(Routes.ExpenseFinanceOperation.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseFinanceOperationResult>>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetExpenseFinanceOperations_NoneExpenseFinanceOperations_ReturnsOk()
        {
            //Arrange
            Initialize();

            //Act
            var response = await _client.GetAsync(Routes.ExpenseFinanceOperation.GetAll);
            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseFinanceOperationResult>>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperation_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var expenseFinanceOperation = new ExpenseFinanceOperation(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(100), "name");
            var updatedExpenseFinanceOperation = new ExpenseFinanceOperation(expenseFinanceOperation.Id, DateOnly.ParseExact("2003-11-29", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(200), "name2");
            var expected = new ExpenseFinanceOperationResult(updatedExpenseFinanceOperation);
            var request = new UpdateExpenseFinanceOperationRequest(expenseFinanceOperation.Id.ToString(), "2003-11-29", expenseType.Id.ToString(), 200, "name2");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.ExpenseTypes.Add(expenseType);
                db.ExpenseFinanceOperations.Add(expenseFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseFinanceOperation.Update, request);
            var actual = await response.Content.ReadFromJsonAsync<ExpenseFinanceOperationResult>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperation_InvalidId_ReturnsNotFound()
        {
            //Arrange
            Initialize();
            var request = new UpdateExpenseFinanceOperationRequest(Guid.NewGuid().ToString(), "2003-11-29", Guid.NewGuid().ToString(), 200, "name2");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseFinanceOperation.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperation_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new UpdateExpenseFinanceOperationRequest("adsgtwreq5y5346", "", "", -100, "");

            //Act
            var response = await _client.PutAsJsonAsync(Routes.ExpenseFinanceOperation.Update, request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        #endregion

        #region ReportControllerTests

        [Fact]
        public async Task GetDailyReport_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var incomeFinanceOperation = new IncomeFinanceOperation(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(100), "name");
            var expenseFinanceOperation = new ExpenseFinanceOperation(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(100), "name");
            var expenseFinanceOperationDto = new ExpenseFinanceOperationDto(expenseFinanceOperation);
            var incomeFinanceOperationDto = new IncomeFinanceOperationDto(incomeFinanceOperation);
            var expected = DailyReport.Create(DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), [expenseFinanceOperationDto], [incomeFinanceOperationDto]);
            var request = new GetDailyReportRequest("2003-11-28");
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.ExpenseTypes.Add(expenseType);
                db.IncomeFinanceOperations.Add(incomeFinanceOperation);
                db.ExpenseFinanceOperations.Add(expenseFinanceOperation);
                db.SaveChanges();
            }

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, Routes.Reports.GetDailyReport)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var actual = await response.Content.ReadFromJsonAsync<DailyReport>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetDailyReport_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new GetDailyReportRequest("");

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, Routes.Reports.GetDailyReport)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetPeriodReport_ValidRequest_ReturnsOk()
        {
            //Arrange
            Initialize();
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "IncomeType1", "Description1");
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "ExpenseType1", "Description1");
            var incomeFinanceOperations = new List<IncomeFinanceOperation>
            {
                new(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(100), "name"),
                new(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-29", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(200), "name2"),
                new(IncomeFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-30", ValidationConstants.FinanceOperation.DateFormat), incomeType.Id, Amount.Create(300), "name3")
            };
            var expenseFinanceOperations = new List<ExpenseFinanceOperation>
            {
                new(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-28", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(100), "name"),
                new(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-29", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(200), "name2"),
                new(ExpenseFinanceOperationId.CreateUniq(), DateOnly.ParseExact("2003-11-30", ValidationConstants.FinanceOperation.DateFormat), expenseType.Id, Amount.Create(300), "name3")
            };

            var incomeFinanceOperationsDto = incomeFinanceOperations.Select(it => new IncomeFinanceOperationDto(it)).ToList();
            var expenseFinanceOperationsDto = expenseFinanceOperations.Select(it => new ExpenseFinanceOperationDto(it)).ToList();
            var datePeriod = new DatePeriod("2003-11-28", "2003-11-30");
            var expected = PeriodReport.Create(datePeriod, expenseFinanceOperationsDto, incomeFinanceOperationsDto);

            var request = new GetPeriodReportRequest("2003-11-28", "2003-11-30");

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
                db.IncomeTypes.Add(incomeType);
                db.ExpenseTypes.Add(expenseType);
                db.IncomeFinanceOperations.AddRange(incomeFinanceOperations);
                db.ExpenseFinanceOperations.AddRange(expenseFinanceOperations);
                db.SaveChanges();
            }

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, Routes.Reports.GetPeriodReport)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);
            var actual = await response.Content.ReadFromJsonAsync<PeriodReport>(_jsonOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPeriodReport_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            Initialize();
            var request = new GetPeriodReportRequest("", "");

            //Act
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, Routes.Reports.GetPeriodReport)
            {
                Content = JsonContent.Create(request)
            };
            var response = await _client.SendAsync(httpMessage);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion
    }
}