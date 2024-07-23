using Moq;
using Task11.Application.Common.Persistance;
using Task11.Application.IncomeTypes;
using Task11.Application.IncomeTypes.Commands.Create;
using Task11.Application.IncomeTypes.Commands.Delete;
using Task11.Application.IncomeTypes.Commands.Update;
using Task11.Application.IncomeTypes.Queries.GetIncomeTypes;
using Task11.Domain.Common.Errors;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;


namespace Task11.Tests
{
    public class IncomeTypeTests
    {
        private readonly Mock<IRepository<IncomeType, IncomeTypeId>> _repositoryMock;

        public IncomeTypeTests()
        {
            _repositoryMock = new Mock<IRepository<IncomeType, IncomeTypeId>>();
        }

        [Fact]
        public async Task CreateExpenseTypeCommandHandler_ValidData_TypeCreate()
        {
            // Arrange
            var handler = new CreateIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new CreateIncomeTypeCommand("Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetEmptyAsyncEnumerable<IncomeType>());

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<IncomeType>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task CreateExpenseTypeCommandHandler_SameTypeExists_ReturnsError()
        {
            // Arrange
            var incomeType = new IncomeType(IncomeTypeId.CreateUniq(), "Test", "Test");
            var handler = new CreateIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new CreateIncomeTypeCommand("Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(incomeType));

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.IncomeType.DuplicateIncomeType, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandHandler_ValidId_RetrunsResult()
        {
            //Arrange
            var expenseType = new IncomeType(IncomeTypeId.CreateUniq(), "Test", "Test");
            var expected = new IncomeTypesResult(expenseType);
            var handler = new DeleteIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new DeleteIncomeTypeCommand(expenseType.Id.ToString());

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<IncomeTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseType);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.IncomeType, result.Value.IncomeType);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandHandler_InvalidId_ReturnsError()
        {
            //Arrange
            var handler = new DeleteIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new DeleteIncomeTypeCommand(Guid.NewGuid().ToString());

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<IncomeTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IncomeType)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.IncomeType.IncomeTypeNotFound, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task GetExpenseTypesQueryHandler_ReturnsResult()
        {
            //Arrange
            var expenses = new List<IncomeType>
            {
                new (IncomeTypeId.CreateUniq(), "Test", "Test"),
                new (IncomeTypeId.CreateUniq(), "Test", "Test")
            };
            var expected = expenses.Select(x => new IncomeTypesResult(x));
            var expenseType = new IncomeType(IncomeTypeId.CreateUniq(), "Test", "Test");
            var handler = new GetIncomeTypesQueryHandler(_repositoryMock.Object);
            var query = new GetIncomeTypesQuery();

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
            var expenseTypeToUpdate = new IncomeType(IncomeTypeId.CreateUniq(), "Test", "Test");
            var updatedExpenseType = new IncomeType(expenseTypeToUpdate.Id, "Test2", "Test2");
            var expected = new IncomeTypesResult(updatedExpenseType);
            var handler = new UpdateIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateIncomeTypeCommand(expenseTypeToUpdate.Id.ToString(), expenseTypeToUpdate.Name, expenseTypeToUpdate.Description);

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<IncomeTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseTypeToUpdate);
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable())
                .Returns(Utilities.GetEmptyAsyncEnumerable<IncomeType>());

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(expected.IncomeType, result.Value.IncomeType);
            Assert.False(result.IsError);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandHandler_InvalidId_ReturnsError()
        {
            //Arrange
            var handler = new UpdateIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateIncomeTypeCommand(Guid.NewGuid().ToString(), "Test", "Test");

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<IncomeTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IncomeType)null);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.IncomeType.IncomeTypeNotFound, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandHandler_SameExpenseTypeExists_ReturnsError()
        {
            //Arrange
            var expenseTypeToUpdate = new IncomeType(IncomeTypeId.CreateUniq(), "Test", "Test");
            var updatedExpenseType = new IncomeType(expenseTypeToUpdate.Id, "Test2", "Test2");
            var handler = new UpdateIncomeTypeCommandHandler(_repositoryMock.Object);
            var command = new UpdateIncomeTypeCommand(expenseTypeToUpdate.Id.ToString(), "Test2", "Test2");

            //Setup
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<IncomeTypeId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expenseTypeToUpdate);
            _repositoryMock.Setup(x => x.GetAllAsAsyncEnumerable())
                .Returns(Utilities.GetAsyncEnumerable(updatedExpenseType));

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Equal(Errors.IncomeType.DuplicateIncomeType, result.FirstError);
            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new CreateIncomeTypeCommandValidator();
            var command = new CreateIncomeTypeCommand("Test", "Test");

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
            var validator = new CreateIncomeTypeCommandValidator();
            var command = new CreateIncomeTypeCommand(name, description);

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task UpdateExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new UpdateIncomeTypeCommandValidator();
            var command = new UpdateIncomeTypeCommand(Guid.NewGuid().ToString(), "Test", "Test");

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
            var validator = new UpdateIncomeTypeCommandValidator();
            var command = new UpdateIncomeTypeCommand(Guid.NewGuid().ToString(), name, description);

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandValidator_ValidData_IsValid()
        {
            //Arrange
            var validator = new DeleteIncomeTypeCommandValidator();
            var command = new DeleteIncomeTypeCommand(Guid.NewGuid().ToString());

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task DeleteExpenseTypeCommandValidator_InvalidData_IsNotValid()
        {
            //Arrange
            var validator = new DeleteIncomeTypeCommandValidator();
            var command = new DeleteIncomeTypeCommand("");

            //Act
            var result = await validator.ValidateAsync(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}
