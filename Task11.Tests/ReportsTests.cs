using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.DTOs;
using Task11.Application.Common.Persistance;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Application.Reports.PeriodReport;
using Task11.Application.Reports.PeriodReport.Queries;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task11.Tests
{
    public class ReportsTests
    {
        private readonly Mock<IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId>> _expensesMock;
        private readonly Mock<IRepository<IncomeFinanceOperation, IncomeFinanceOperationId>> _incomesMock;

        public ReportsTests()
        {
            _expensesMock = new Mock<IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId>>();
            _incomesMock = new Mock<IRepository<IncomeFinanceOperation, IncomeFinanceOperationId>>();
        }

        [Fact]
        public async Task GetDailyReport_CorrectDate_ReturnsReport()
        {
            //Arrange
            string date = "2022-01-01";
            ExpenseFinanceOperation expense = new(
                ExpenseFinanceOperationId.CreateUniq(),
                DateOnly.Parse(date),
                ExpenseTypeId.CreateUniq(),
                Amount.Create(100),
                "Test Expense");

            IncomeFinanceOperation income = new(
                IncomeFinanceOperationId.CreateUniq(),
                DateOnly.Parse(date),
                IncomeTypeId.CreateUniq(),
                Amount.Create(200),
                "Test Income");


            var expenseDto = new ExpenseFinanceOperationDto(expense);
            var incomeDto = new IncomeFinanceOperationDto(income);

            var expected = DailyReport.Create(
                new DateOnly(2022, 1, 1),
                [expenseDto],
                [incomeDto]);

            var query = new GetDailyReportQuery(date);
            var handler = new GetDailyReportQueryHandler(_incomesMock.Object, _expensesMock.Object);

            //Setup
            _expensesMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(expense));
            _incomesMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(income));

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsError);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetPeriodReport_ValidDates_ReturnsReport()
        {
            //Arrange
            string startDate = "2022-01-01";
            string endDate = "2022-01-31";
            DatePeriod datePeriod = new(startDate, endDate);

            ExpenseFinanceOperation expense = new(
                ExpenseFinanceOperationId.CreateUniq(),
                DateOnly.Parse("2022-01-05"),
                ExpenseTypeId.CreateUniq(),
                Amount.Create(100),
                "Test Expense");

            IncomeFinanceOperation income = new(
                IncomeFinanceOperationId.CreateUniq(),
                DateOnly.Parse("2022-01-21"),
                IncomeTypeId.CreateUniq(),
                Amount.Create(200),
                "Test Income");

            var expected = PeriodReport.Create(
                datePeriod,
                [new ExpenseFinanceOperationDto(expense)],
                [new IncomeFinanceOperationDto(income)]);

            var query = new GetPeriodReportQuery(startDate, endDate);
            var handler = new GetPeriodReportQueryHandler(_expensesMock.Object, _incomesMock.Object);


            //Setup
            _expensesMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(expense));
            _incomesMock.Setup(x => x.GetAllAsAsyncEnumerable()).Returns(Utilities.GetAsyncEnumerable(income));

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsError);
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("2022.01.31")]
        [InlineData("")]
        [InlineData("asdasddas")]
        public async Task GetDailyReportQueryValidaor_InvalidDate_ReturnsInvalid(string date)
        {
            //Arrange
            var query = new GetDailyReportQuery(date);
            var validator = new GetDailyReportQueryValidator();

            //Act
            var result = await validator.ValidateAsync(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task GetDailyReportQueryValidator_ValidDate_ReturnsValid()
        {
            //Arrange
            var query = new GetDailyReportQuery("2022-01-01");
            var validator = new GetDailyReportQueryValidator();

            //Act
            var result = await validator.ValidateAsync(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("2022.01.31", "2022.01.01")]
        [InlineData("", "2022-01-01")]
        [InlineData("asdasddas", "2022-01-01")]
        public async Task GetPeriodReportQueryValidator_InvalidDates_ReturnsInvalid(string startDate, string endDate)
        {
            //Arrange
            var query = new GetPeriodReportQuery(startDate, endDate);
            var validator = new GetPeriodReportQueryValidator();

            //Act
            var result = await validator.ValidateAsync(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task GetPeriodReportQueryValidator_ValidDates_ReturnsValid()
        {
            //Arrange
            var query = new GetPeriodReportQuery("2022-01-01", "2022-01-31");
            var validator = new GetPeriodReportQueryValidator();

            //Act
            var result = await validator.ValidateAsync(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

    }
}
