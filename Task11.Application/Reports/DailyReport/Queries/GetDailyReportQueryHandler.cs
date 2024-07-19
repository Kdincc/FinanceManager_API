using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var incomes = await _incomeOperationRepository.GetAllAsync(cancellationToken);
            var expenses = await _expenseOperationRepository.GetAllAsync(cancellationToken);

            DailyReport dailyReport = DailyReport.Create(request.Date, incomes, expenses);
        }
    }
}
