using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.DTOs;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Models;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Reports.PeriodReport.Queries
{
    public sealed class GetPeriodReportQueryHandler(
        IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> expenseRepository,
        IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> incomeRepository) : IRequestHandler<GetPeriodReportQuery, ErrorOr<PeriodReport>>
    {
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _expenseRepository = expenseRepository;
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _incomeRepository = incomeRepository;

        public async Task<ErrorOr<PeriodReport>> Handle(GetPeriodReportQuery request, CancellationToken cancellationToken)
        {
            DatePeriod period = new (request.StartDate, request.EndDate);

            var matchesIncomes = await GetPeriodMatchesIncomeFinanceOperations(period);
            var matchesExpenses = await GetPeriodMatchesExpenseFinanceOperations(period);

            var incomeDtos = matchesIncomes.Select(i => new IncomeFinanceOperationDto(i)).ToList();
            var expenseDtos = matchesExpenses.Select(e => new ExpenseFinanceOperationDto(e)).ToList();

            return PeriodReport.Create(period, expenseDtos, incomeDtos);
        }

        private async Task<IReadOnlyCollection<IncomeFinanceOperation>> GetPeriodMatchesIncomeFinanceOperations(DatePeriod period)
        {
            List<IncomeFinanceOperation> dateMatchesOperations = [];
            
            await foreach(var income in _incomeRepository.GetAllAsAsyncEnumerable())
            {
                if (period.Contains(income.Date))
                {
                    dateMatchesOperations.Add(income);
                }
            }

            return dateMatchesOperations;
        }

        private async Task<IReadOnlyCollection<ExpenseFinanceOperation>> GetPeriodMatchesExpenseFinanceOperations(DatePeriod period)
        {
            List<ExpenseFinanceOperation> dateMatchesOperations = [];

            await foreach (var expense in _expenseRepository.GetAllAsAsyncEnumerable())
            {
                if (period.Contains(expense.Date))
                {
                    dateMatchesOperations.Add(expense);
                }
            }

            return dateMatchesOperations;
        }
    }
}
