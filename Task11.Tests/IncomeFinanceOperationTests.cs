using ErrorOr;
using Moq;
using Task11.Application.Common.Persistance;
using Task11.Application.IncomeFinanceOperations;
using Task11.Application.IncomeFinanceOperations.Commands.Create;
using Task11.Application.IncomeFinanceOperations.Commands.Delete;
using Task11.Application.IncomeFinanceOperations.Commands.Update;
using Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Tests
{
    public class IncomeFinanceOperationTests
    {
        private readonly Mock<IRepository<IncomeFinanceOperation, IncomeFinanceOperationId>> _incomesRepositoryMock;
        private readonly Mock<IRepository<Domain.IncomeType.IncomeType, IncomeTypeId>> _incomeTypeRepositoryMock;
        private IncomeTypeId _incomeTypeId = IncomeTypeId.CreateUniq();
        private IncomeFinanceOperationId _incomeFinanceOperationId = IncomeFinanceOperationId.CreateUniq();

        public IncomeFinanceOperationTests()
        {
            _incomesRepositoryMock = new Mock<IRepository<IncomeFinanceOperation, IncomeFinanceOperationId>>();
            _incomeTypeRepositoryMock = new Mock<IRepository<Domain.IncomeType.IncomeType, IncomeTypeId>>();
        }

        [Fact]
        public async Task CreateIncomeFinanceOperationCommandHandler_ValidData_OperationCreated()
        {
            //Arrange
            CreateIncomeFinanceOperationCommand command = new(
                "2011-11-11",
                _incomeTypeId.ToString(),
                100,
                "test");
            CreateIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object, _incomeTypeRepositoryMock.Object);

            //Setup
            _incomeTypeRepositoryMock.Setup(x => x.GetByIdAsync(_incomeTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.IncomeType.IncomeType(_incomeTypeId, "Test", "Test"));

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            _incomesRepositoryMock.Verify(x => x.AddAsync(It.IsAny<IncomeFinanceOperation>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task CreateIncomeFinanceOperationCommandHandler_InvalidExpenseTypeId_ReturnsError()
        {
            //Arrange
            CreateIncomeFinanceOperationCommand command = new(
                "2011-11-11",
                _incomeTypeId.ToString(),
                100,
                "test");
            CreateIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object, _incomeTypeRepositoryMock.Object);

            //Setup
            _incomeTypeRepositoryMock.Setup(x => x.GetByIdAsync(_incomeTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.IncomeType.IncomeType)null);

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.IncomeType.IncomeTypeNotFound, actual.FirstError);
        }

        [Fact]
        public async Task DeleteIncomeFinanceOperationHandler_ValidId_ReturnsResult()
        {
            //Arrange
            var operationToDelete = new IncomeFinanceOperation(_incomeFinanceOperationId, DateOnly.Parse("2011-11-11"), _incomeTypeId, Amount.Create(100), "test");
            IncomeFinanceOperationResult expected = new(operationToDelete);
            DeleteIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString());
            DeleteIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object);

            //Setup
            _incomesRepositoryMock.Setup(x => x.GetByIdAsync(_incomeFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToDelete);

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.IncomeFinanceOperation, actual.Value.IncomeFinanceOperation);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task DeleteIncomeFinanceOperationHandler_InvalidId_ReturnsError()
        {
            //Arrange
            DeleteIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString());
            DeleteIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object);

            //Setup
            _incomesRepositoryMock.Setup(x => x.GetByIdAsync(_incomeFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((IncomeFinanceOperation)null);

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound, actual.FirstError);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperationHandler_ValidData_OperationUpdated()
        {
            //Arrange
            var operationToUpdate = new IncomeFinanceOperation(_incomeFinanceOperationId, DateOnly.Parse("2011-11-11"), _incomeTypeId, Amount.Create(100), "test");
            var updatedOperation = new IncomeFinanceOperation(_incomeFinanceOperationId, DateOnly.Parse("2011-12-12"), _incomeTypeId, Amount.Create(200), "Test");
            var expected = new IncomeFinanceOperationResult(updatedOperation);

            UpdateIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString(), _incomeTypeId.ToString(), "2011-12-12", 200, "Test");
            UpdateIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object, _incomeTypeRepositoryMock.Object);

            //Setup
            _incomesRepositoryMock.Setup(x => x.GetByIdAsync(_incomeFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToUpdate);
            _incomeTypeRepositoryMock.Setup(x => x.GetByIdAsync(_incomeTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.IncomeType.IncomeType(_incomeTypeId, "Test", "Test"));

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.IncomeFinanceOperation, actual.Value.IncomeFinanceOperation);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperationHandler_InvalidExpenseTypeId_ReturnsError()
        {
            //Arrange
            var operationToUpdate = new IncomeFinanceOperation(_incomeFinanceOperationId, DateOnly.Parse("2011-11-11"), _incomeTypeId, Amount.Create(100), "test");
            UpdateIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString(), _incomeTypeId.ToString(), "2011-12-12", 200, "Test");
            UpdateIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object, _incomeTypeRepositoryMock.Object);

            //Setup
            _incomesRepositoryMock.Setup(x => x.GetByIdAsync(_incomeFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToUpdate);
            _incomeTypeRepositoryMock.Setup(x => x.GetByIdAsync(_incomeTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.IncomeType.IncomeType)null);

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.IncomeType.IncomeTypeNotFound, actual.FirstError);
        }

        [Fact]
        public async Task UpdateIncomeFinanceOperationHandler_InvalidOperationId_ReturnsError()
        {
            //Arrange
            UpdateIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString(), _incomeTypeId.ToString(), "2011-12-12", 200, "Test");
            UpdateIncomeFinanceOperationCommandHandler handler = new(_incomesRepositoryMock.Object, _incomeTypeRepositoryMock.Object);

            //Setup
            _incomeTypeRepositoryMock.Setup(x => x.GetByIdAsync(_incomeTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.IncomeType.IncomeType(_incomeTypeId, "Test", "Test"));
            _incomesRepositoryMock.Setup(x => x.GetByIdAsync(_incomeFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((IncomeFinanceOperation)null);

            //Act
            ErrorOr<IncomeFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound, actual.FirstError);
        }

        [Fact]
        public async Task GetIncomeFinanceOperationsQueryHandler_RetrunsOperations()
        {
            //Arrange
            var handler = new GetIncomeFinanceOperationsQueryHandler(_incomesRepositoryMock.Object);
            IReadOnlyCollection<IncomeFinanceOperation> operations =
            [
                new(IncomeFinanceOperationId.CreateUniq(), DateOnly.MinValue, IncomeTypeId.CreateUniq(), Amount.Create(0), ""),
                new(IncomeFinanceOperationId.CreateUniq(), DateOnly.MinValue, IncomeTypeId.CreateUniq(), Amount.Create(0), "")
            ];

            IEnumerable<IncomeFinanceOperationResult> expected = operations.Select(x => new IncomeFinanceOperationResult(x));

            //Setup
            _incomesRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(operations);

            //Act
            IEnumerable<IncomeFinanceOperationResult> actual = await handler.Handle(new GetIncomeFinanceOperationsQuery(), CancellationToken.None);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateIncomeFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            CreateIncomeFinanceOperationCommand command = new(
                "2011-11-11",
                _incomeTypeId.ToString(),
                100,
                "test");
            CreateIncomeFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.True(actual.IsValid);
        }

        [Theory]
        [InlineData("2003.11.23", 100, "test")]
        [InlineData("2011-11-11", -1, "test")]
        [InlineData("2011-11-11", 100, "")]
        [InlineData("asdasdasdaslkf", 100, "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt")]
        public void CreateIncomeFinanceOperationCommandValidator_InvalidData_IsInvalid(string date, decimal amount, string name)
        {
            //Arrange
            CreateIncomeFinanceOperationCommand command = new(
                 date,
                 _incomeTypeId.ToString(),
                 amount,
                 name);
            CreateIncomeFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.False(actual.IsValid);
        }

        [Fact]
        public void UpdateIncomeFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            UpdateIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString(), _incomeTypeId.ToString(), "2011-11-11", 100, "test");
            UpdateIncomeFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.True(actual.IsValid);
        }

        [Theory]
        [InlineData("2003.11.23", 100, "test")]
        [InlineData("2011-11-11", -1, "test")]
        [InlineData("2011-11-11", 100, "")]
        [InlineData("asdasdasdaslkf", 100, "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt")]
        public void UpdateIncomeFinanceOperationCommandValidator_InvalidData_IsInvalid(string date, decimal amount, string name)
        {
            //Arrange
            UpdateIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString(), date, _incomeTypeId.ToString(), amount, name);
            UpdateIncomeFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.False(actual.IsValid);
        }

        [Fact]
        public void DeleteIncomeFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            DeleteIncomeFinanceOperationCommand command = new(_incomeFinanceOperationId.ToString());
            DeleteIncomeFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.True(actual.IsValid);
        }
    }
}
