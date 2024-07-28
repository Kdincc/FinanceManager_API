using ErrorOr;
using Moq;
using Task11.Application.Common.Persistance;
using Task11.Application.ExpenseFinanceOperations;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Application.ExpenseFinanceOperations.Commands.Delete;
using Task11.Application.ExpenseFinanceOperations.Commands.Update;
using Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations;
using Task11.Domain.Common.Errors;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Tests
{
    public class ExpenseFinanceOperationTests
    {
        private readonly Mock<IExpenseFinanceOperationRepository> _expensesRepositoryMock;
        private readonly Mock<IExpenseTypeRepository> _expenseTypeRepositoryMock;
        private ExpenseTypeId _expenseTypeId = ExpenseTypeId.CreateUniq();
        private ExpenseFinanceOperationId _expenseFinanceOperationId = ExpenseFinanceOperationId.CreateUniq();

        public ExpenseFinanceOperationTests()
        {
            _expensesRepositoryMock = new Mock<IExpenseFinanceOperationRepository>();
            _expenseTypeRepositoryMock = new Mock<IExpenseTypeRepository>();
        }

        [Fact]
        public async Task CreateExpenseFinanceOperationCommandHandler_ValidData_OperationCreated()
        {
            //Arrange
            CreateExpenseFinanaceOperationCommand command = new(
                "2011-11-11",
                _expenseTypeId.ToString(),
                100,
                "test");
            CreateExpenseFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object, _expenseTypeRepositoryMock.Object);

            //Setup
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(_expenseTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExpenseType(_expenseTypeId, "Test", "Test"));

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            _expensesRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ExpenseFinanceOperation>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task CreateExpenseFinanceOperationCommandHandler_InvalidExpenseTypeId_ReturnsError()
        {
            //Arrange
            CreateExpenseFinanaceOperationCommand command = new(
                "2011-11-11",
                _expenseTypeId.ToString(),
                100,
                "test");
            CreateExpenseFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object, _expenseTypeRepositoryMock.Object);

            //Setup
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(_expenseTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseType)null);

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.ExpenseType.ExpenseTypeNotFound, actual.FirstError);
        }

        [Fact]
        public async Task DeleteExpenseFinanceOperationHandler_ValidId_ReturnsResult()
        {
            //Arrange
            var operationToDelete = new ExpenseFinanceOperation(_expenseFinanceOperationId, DateOnly.Parse("2011-11-11"), _expenseTypeId, Amount.Create(100), "test");
            ExpenseFinanceOperationResult expected = new(operationToDelete);
            DeleteExpenceFinanseOperationCommand command = new(_expenseFinanceOperationId.ToString());
            DeleteExpenceFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object);

            //Setup
            _expensesRepositoryMock.Setup(x => x.GetByIdAsync(_expenseFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToDelete);

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.FinanceOperation, actual.Value.FinanceOperation);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task DeleteExpenseFinanceOperationHandler_InvalidId_ReturnsError()
        {
            //Arrange
            DeleteExpenceFinanseOperationCommand command = new(_expenseFinanceOperationId.ToString());
            DeleteExpenceFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object);

            //Setup
            _expensesRepositoryMock.Setup(x => x.GetByIdAsync(_expenseFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseFinanceOperation)null);

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound, actual.FirstError);
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperationHandler_ValidData_OperationUpdated()
        {
            //Arrange
            var operationToUpdate = new ExpenseFinanceOperation(_expenseFinanceOperationId, DateOnly.Parse("2011-11-11"), _expenseTypeId, Amount.Create(100), "test");
            var updatedOperation = new ExpenseFinanceOperation(_expenseFinanceOperationId, DateOnly.Parse("2011-12-12"), _expenseTypeId, Amount.Create(200), "Test");
            var expected = new ExpenseFinanceOperationResult(updatedOperation);

            UpdateExpenceFinanceOperationCommand command = new(_expenseFinanceOperationId.ToString(), "2011-12-12", _expenseTypeId.ToString(), 200, "Test");
            UpdateExpenceFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object, _expenseTypeRepositoryMock.Object);

            //Setup
            _expensesRepositoryMock.Setup(x => x.GetByIdAsync(_expenseFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToUpdate);
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(_expenseTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExpenseType(_expenseTypeId, "Test", "Test"));

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.FinanceOperation, actual.Value.FinanceOperation);
            Assert.False(actual.IsError);
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperationHandler_InvalidExpenseTypeId_ReturnsError()
        {
            //Arrange
            var operationToUpdate = new ExpenseFinanceOperation(_expenseFinanceOperationId, DateOnly.Parse("2011-11-11"), _expenseTypeId, Amount.Create(100), "test");
            UpdateExpenceFinanceOperationCommand command = new(_expenseFinanceOperationId.ToString(), "2011-12-12", _expenseTypeId.ToString(), 200, "Test");
            UpdateExpenceFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object, _expenseTypeRepositoryMock.Object);

            //Setup
            _expensesRepositoryMock.Setup(x => x.GetByIdAsync(_expenseFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(operationToUpdate);
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(_expenseTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseType)null);

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.ExpenseType.ExpenseTypeNotFound, actual.FirstError);
        }

        [Fact]
        public async Task UpdateExpenseFinanceOperationHandler_InvalidOperationId_ReturnsError()
        {
            //Arrange
            UpdateExpenceFinanceOperationCommand command = new(_expenseFinanceOperationId.ToString(), "2011-12-12", _expenseTypeId.ToString(), 200, "Test");
            UpdateExpenceFinanceOperationCommandHandler handler = new(_expensesRepositoryMock.Object, _expenseTypeRepositoryMock.Object);

            //Setup
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(_expenseTypeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExpenseType(_expenseTypeId, "Test", "Test"));
            _expensesRepositoryMock.Setup(x => x.GetByIdAsync(_expenseFinanceOperationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseFinanceOperation)null);

            //Act
            ErrorOr<ExpenseFinanceOperationResult> actual = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(actual.IsError);
            Assert.Equal(Errors.ExpenceFinanceOperation.ExpenceFinanceOperationNotFound, actual.FirstError);
        }

        [Fact]
        public async Task GetExpenceFinanceOperationsQueryHandler_RetrunsOperations()
        {
            //Arrange
            var handler = new GetExpenceFinanceOperationsQueryHandler(_expensesRepositoryMock.Object);
            IReadOnlyCollection<ExpenseFinanceOperation> operations =
            [
                new(ExpenseFinanceOperationId.CreateUniq(), DateOnly.MinValue, ExpenseTypeId.CreateUniq(), Amount.Create(0), ""),
                new(ExpenseFinanceOperationId.CreateUniq(), DateOnly.MinValue, ExpenseTypeId.CreateUniq(), Amount.Create(0), "")
            ];

            IEnumerable<ExpenseFinanceOperationResult> expected = operations.Select(x => new ExpenseFinanceOperationResult(x));

            //Setup
            _expensesRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(operations);

            //Act
            IEnumerable<ExpenseFinanceOperationResult> actual = await handler.Handle(new GetExpenceFinanceOperationsQuery(), CancellationToken.None);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateExpenseFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            CreateExpenseFinanaceOperationCommand command = new(
                "2011-11-11",
                _expenseTypeId.ToString(),
                100,
                "test");
            CreateExpenceFinanceOperationCommandValidator validator = new();

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
        public void CreateExpenseFinanceOperationCommandValidator_InvalidData_IsInvalid(string date, decimal amount, string name)
        {
            //Arrange
            CreateExpenseFinanaceOperationCommand command = new(
                date,
                _expenseTypeId.ToString(),
                amount,
                name);
            CreateExpenceFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.False(actual.IsValid);
        }

        [Fact]
        public void UpdateExpenseFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            UpdateExpenceFinanceOperationCommand command = new(_expenseFinanceOperationId.ToString(), "2011-11-11", _expenseTypeId.ToString(), 100, "test");
            UpdateExpenceFinanceOperationCommandValidator validator = new();

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
        public void UpdateExpenseFinanceOperationCommandValidator_InvalidData_IsInvalid(string date, decimal amount, string name)
        {
            //Arrange
            UpdateExpenceFinanceOperationCommand command = new(_expenseFinanceOperationId.ToString(), date, _expenseTypeId.ToString(), amount, name);
            UpdateExpenceFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.False(actual.IsValid);
        }

        [Fact]
        public void DeleteExpenseFinanceOperationCommandValidator_ValidData_IsValid()
        {
            //Arrange
            DeleteExpenceFinanseOperationCommand command = new(_expenseFinanceOperationId.ToString());
            DeleteExpenceFinanceOperationCommandValidator validator = new();

            //Act
            var actual = validator.Validate(command);

            //Assert
            Assert.True(actual.IsValid);
        }
    }
}