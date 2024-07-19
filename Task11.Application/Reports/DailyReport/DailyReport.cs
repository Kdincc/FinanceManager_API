using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate;

namespace Task11.Application.DailyReport
{
    public sealed class DailyReport
    {
        private DailyReport(
            DateTime date,
            IReadOnlyCollection<ExpenseFinanceOperation> expenses,
            IReadOnlyCollection<IncomeFinanceOperation> incomes,
            Amount totalExpenses,
            Amount totalIncomes)
        {
            Date = date;
            Expenses = expenses;
            Incomes = incomes;
            TotalExpenses = totalExpenses;
            TotalIncomes = totalIncomes;
        }

        public static DailyReport Create(
            DateTime date,
            IReadOnlyCollection<ExpenseFinanceOperation> expenses,
            IReadOnlyCollection<IncomeFinanceOperation> incomes)
        {
            ThrowIfFinanceOperationsDatesNotMatchReportDate(expenses, incomes, date);
            
            Amount totalExpenses = expenses.Aggregate(Amount.Create(0), (acc, x) => acc + x.Amount);
            Amount totalIncomes = incomes.Aggregate(Amount.Create(0), (acc, x) => acc + x.Amount);

            return new DailyReport(date, expenses, incomes, totalExpenses, totalIncomes);
        }

        public DateTime Date { get; }

        public IReadOnlyCollection<ExpenseFinanceOperation> Expenses { get; }

        public IReadOnlyCollection<IncomeFinanceOperation> Incomes { get; }

        public Amount TotalExpenses { get; }

        public Amount TotalIncomes { get; }

        private static void ThrowIfFinanceOperationsDatesNotMatchReportDate(
            IReadOnlyCollection<ExpenseFinanceOperation> expenses,
            IReadOnlyCollection<IncomeFinanceOperation> incomes,
            DateTime reportDate)
        {
            var financeOperationsUniqDates = expenses.Select(e => e.Date)
                .Concat(incomes.Select(i => i.Date))
                .Distinct()
                .ToList();

            if (financeOperationsUniqDates.Any(d => d != reportDate))
            {
                throw new InvalidOperationException("Finance operations dates do not match report date");
            }
        }
    }
}
