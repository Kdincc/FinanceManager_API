using Moq;
using Task11.Application.Common.Persistance;
using Task11.Application.ExpenseTypes;
using Task11.Application.ExpenseTypes.Commands.Create;
using Task11.Application.ExpenseTypes.Commands.Delete;
using Task11.Application.ExpenseTypes.Commands.Update;
using Task11.Application.ExpenseTypes.Queries.GetExpenseTypes;
using Task11.Domain.Common.Errors;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Tests
{

    public class ExpenseTypeTests
    {
        private readonly Mock<IRepository<ExpenseType, ExpenseTypeId>> _repositoryMock;

        public ExpenseTypeTests()
        {
            _repositoryMock = new Mock<IRepository<ExpenseType, ExpenseTypeId>>();
        }

        [Fact]
        public async Task CreateExpenseTypeCommandHandler_ValidData_TypeCreate()
        {
            // Arrange
            var handler = new CreateExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new CreateExpenseTypeCommand("Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetEmptyAsyncEnumerable<ExpenseType>());

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<ExpenseType>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task CreateExpenseTypeCommandHandler_SameTypeExists_ReturnsError()
        {
            // Arrange
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "Test", "Test");
            var handler = new CreateExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new CreateExpenseTypeCommand("Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(expenseType));

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.ExpenseType.DuplicateExpenseType, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandHandler_ValidId_RetrunsResult()
        {
            //Arrange
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "Test", "Test");
            var expected = new ExpenseTypesResult(expenseType);
            var handler = new DeleteExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new DeleteExpenseTypeCommand(expenseType.Id.ToString());

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ExpenseTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseType);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.ExpenseType, result.Value.ExpenseType);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandHandler_InvalidId_ReturnsError()
        {
            //Arrange
            var handler = new DeleteExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new DeleteExpenseTypeCommand(Guid.NewGuid().ToString());

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ExpenseTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseType)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.ExpenseType.ExpenseTypeNotFound, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task GetExpenseTypesQueryHandler_ReturnsResult()
        {
            //Arrange
            var expenses = new List<ExpenseType>
            {
                new (ExpenseTypeId.CreateUniq(), "Test", "Test"),
                new (ExpenseTypeId.CreateUniq(), "Test", "Test")
            };
            var expected = expenses.Select(x => new ExpenseTypesResult(x));
            var expenseType = new ExpenseType(ExpenseTypeId.CreateUniq(), "Test", "Test");
            var handler = new GetExpenseTypesQueryHandler(_repositoryMock.Object);
            var query = new GetExpenseTypesQuery();

            //Setup
            _repositoryMock.Setup(x => x.GetAllAsync(CancellationToken.None))
                .ReturnsAsync(expenses);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandHandler_ValidId_ReturnsResult()
        {
            //Arrange
            var expenseTypeToUpdate = new ExpenseType(ExpenseTypeId.CreateUniq(), "Test", "Test");
            var updatedExpenseType = new ExpenseType(expenseTypeToUpdate.Id, "Test2", "Test2");
            var expected = new ExpenseTypesResult(updatedExpenseType);
            var handler = new UpdateExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateExpenseTypeCommand(expenseTypeToUpdate.Id.ToString(), expenseTypeToUpdate.Name, expenseTypeToUpdate.Description);

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ExpenseTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseTypeToUpdate);
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable())
                .Returns(Utilities.GetEmptyAsyncEnumerable<ExpenseType>());

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.ExpenseType, result.Value.ExpenseType);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandHandler_InvalidId_ReturnsError()
        {
            //Arrange
            var handler = new UpdateExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateExpenseTypeCommand(Guid.NewGuid().ToString(), "Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ExpenseTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ExpenseType)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.ExpenseType.ExpenseTypeNotFound, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandHandler_SameExpenseTypeExists_ReturnsError()
        {
            //Arrange
            var expenseTypeToUpdate = new ExpenseType(ExpenseTypeId.CreateUniq(), "Test", "Test");
            var updatedExpenseType = new ExpenseType(expenseTypeToUpdate.Id, "Test2", "Test2");
            var handler = new UpdateExpenseTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateExpenseTypeCommand(expenseTypeToUpdate.Id.ToString(), "Test2", "Test2");

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ExpenseTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseTypeToUpdate);
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable())
                .Returns(Utilities.GetAsyncEnumerable(updatedExpenseType));

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.ExpenseType.DuplicateExpenseType, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new CreateExpenseTypeCommandValidator();
            var command = new CreateExpenseTypeCommand("Test", "Test");

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("test", "")]
        [InlineData("", "test")]
        public async Task CreateExpenseTypeCommandValidator_InvalidData_IsNotValid(string name, string description)
        {
            //Arrange
            var validator = new CreateExpenseTypeCommandValidator();
            var command = new CreateExpenseTypeCommand(name, description);

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new UpdateExpenseTypeCommandValidator();
            var command = new UpdateExpenseTypeCommand(Guid.NewGuid().ToString(), "Test", "Test");

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("test", "")]
        [InlineData("", "test")]
        public async Task UpdateExpenseTypeCommandValidator_InvalidData_IsNotValid(string name, string description)
        {
            //Arrange
            var validator = new UpdateExpenseTypeCommandValidator();
            var command = new UpdateExpenseTypeCommand(Guid.NewGuid().ToString(), name, description);

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new DeleteExpenseTypeCommandValidator();
            var command = new DeleteExpenseTypeCommand(Guid.NewGuid().ToString());

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandValidator_InvalidData_IsNotValid()
        {
            //Arrange
            var validator = new DeleteExpenseTypeCommandValidator();
            var command = new DeleteExpenseTypeCommand("");

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}
