using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.DTOs;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public sealed class GetDailyReportQueryHandler(
        IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> incomeOperationRepository,
        IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> expenseOperationRepository) : IRequestHandler<GetDailyReportQuery, ErrorOr<DailyReport>>
    {
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _incomeOperationRepository = incomeOperationRepository;
        private readonly IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId> _expenseOperationRepository = expenseOperationRepository;

        public async Task<ErrorOr<DailyReport>> Handle(GetDailyReportQuery request, CancellationToken cancellationToken)
        {
            var incomes = await GetDateMatchesIncomeFinanceOperations(request.Date);
            var expenses = await GetDateMatchesExpenseFinanceOperations(request.Date);

            var incomeDtos = incomes.Select(i => new IncomeFinanceOperationDto(i)).ToList();
            var expenseDtos = expenses.Select(e => new ExpenseFinanceOperationDto(e)).ToList();

            return DailyReport.Create(request.Date, expenseDtos, incomeDtos);
        }

        private async Task<IReadOnlyCollection<IncomeFinanceOperation>> GetDateMatchesIncomeFinanceOperations(DateOnly date)
        {
            List<IncomeFinanceOperation> dateMatchesOperations = [];

            await foreach (var income in _incomeOperationRepository.GetAllAsAsyncEnumerable())
            {
                if (income.Date == date)
                {
                    dateMatchesOperations.Add(income);
                }
            }

            return dateMatchesOperations;
        }

        private async Task<IReadOnlyCollection<ExpenseFinanceOperation>> GetDateMatchesExpenseFinanceOperations(DateOnly date)
        {
            List<ExpenseFinanceOperation> dateMatchesOperations = [];

            await foreach (var expense in _expenseOperationRepository.GetAllAsAsyncEnumerable())
            {
                if (expense.Date == date)
                {
                    dateMatchesOperations.Add(expense);
                }
            }

            return dateMatchesOperations;
        }
    }
}
