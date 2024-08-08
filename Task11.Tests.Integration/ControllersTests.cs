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
using Task11.Application.IncomeTypes;
using Task11.Contracts.ExpenseType;
using Task11.Contracts.IncomeType;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;
using Task11.Infrastructure.Persistence;
using Task11.Presentation;
using Task11.Presentation.ApiRoutes;
using Task11.Tests.Integration.JsonConverters;

namespace Task11.Tests.Integration
{
    public class ControllersTests : IntegrationTestBase
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            Converters = { new IncomeTypesResultConverter(), new ExpenseTypesResultConverter() }
        };

        public ControllersTests(FinanceWebApplicationFactory factory) : base(factory)
        {
        }

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
    }
}