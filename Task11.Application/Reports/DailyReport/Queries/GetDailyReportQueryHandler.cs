using ErrorOr;
using MediatR;
using Task11.Application.Common.DTOs;
using Task11.Application.Common.Persistance;
using Task11.Domain.Common.Сonstants;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public sealed class GetDailyReportQueryHandler(
        IIncomeFinanceOperationRepository incomeOperationRepository,
        IExpenseFinanceOperationRepository expenseOperationRepository) : IRequestHandler<GetDailyReportQuery, ErrorOr<DailyReport>>
    {
        private readonly IIncomeFinanceOperationRepository _incomeOperationRepository = incomeOperationRepository;
        private readonly IExpenseFinanceOperationRepository _expenseOperationRepository = expenseOperationRepository;

        public async Task<ErrorOr<DailyReport>> Handle(GetDailyReportQuery request, CancellationToken cancellationToken)
        {
            DateOnly date = DateOnly.ParseExact(request.Date, ValidationConstants.FinanceOperation.DateFormat);

            var incomes = await GetDateMatchesIncomeFinanceOperations(date);
            var expenses = await GetDateMatchesExpenseFinanceOperations(date);

            var incomeDtos = incomes.Select(i => new IncomeFinanceOperationDto(i)).ToList();
            var expenseDtos = expenses.Select(e => new ExpenseFinanceOperationDto(e)).ToList();

            return DailyReport.Create(date, expenseDtos, incomeDtos);
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
