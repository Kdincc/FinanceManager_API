using Task11.Application.Common.DTOs;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Application.Reports.PeriodReport
{
    public sealed class PeriodReport : IEquatable<PeriodReport>
    {
        private PeriodReport(
            DatePeriod period,
            IReadOnlyCollection<ExpenseFinanceOperationDto> expenses,
            IReadOnlyCollection<IncomeFinanceOperationDto> incomes,
            Amount totalExpenses,
            Amount totalIncomes)
        {
            Period = period;
            Expenses = expenses;
            Incomes = incomes;
            TotalExpenses = totalExpenses;
            TotalIncomes = totalIncomes;
        }

        public static PeriodReport Create(
            DatePeriod period,
            IReadOnlyCollection<ExpenseFinanceOperationDto> expenses,
            IReadOnlyCollection<IncomeFinanceOperationDto> incomes)
        {
            ThrowIfFinanceOperationsDatesNotMatchReportPeriod(expenses, incomes, period);

            Amount totalExpenses = expenses.Aggregate(Amount.Create(0), (acc, x) => acc + x.Amount);
            Amount totalIncomes = incomes.Aggregate(Amount.Create(0), (acc, x) => acc + x.Amount);

            return new PeriodReport(period, expenses, incomes, totalExpenses, totalIncomes);
        }

        public DatePeriod Period { get; }

        public IReadOnlyCollection<ExpenseFinanceOperationDto> Expenses { get; }

        public IReadOnlyCollection<IncomeFinanceOperationDto> Incomes { get; }

        public Amount TotalExpenses { get; }

        public Amount TotalIncomes { get; }

        private static void ThrowIfFinanceOperationsDatesNotMatchReportPeriod(
            IReadOnlyCollection<ExpenseFinanceOperationDto> expenses,
            IReadOnlyCollection<IncomeFinanceOperationDto> incomes,
            DatePeriod reportPeriod)
        {
            var financeOperationsUniqDates = expenses.Select(e => e.Date)
                .Concat(incomes.Select(i => i.Date))
                .Distinct()
                .ToList();

            if (financeOperationsUniqDates.Any(date => !reportPeriod.Contains(date)))
            {
                throw new ArgumentException("Finance operations dates do not match report period.");
            }
        }

        public bool Equals(PeriodReport other)
        {
            bool isDatesEqual = Period == other.Period;
            bool isExpensesEqual = Expenses.SequenceEqual(other.Expenses);
            bool isIncomesEqual = Incomes.SequenceEqual(other.Incomes);
            bool isTotalExpensesEqual = TotalExpenses == other.TotalExpenses;
            bool isTotalIncomesEqual = TotalIncomes == other.TotalIncomes;

            bool areEquals = isDatesEqual && isExpensesEqual && isIncomesEqual && isTotalExpensesEqual && isTotalIncomesEqual;

            return areEquals;
        }
    }
}
