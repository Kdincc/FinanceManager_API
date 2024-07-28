using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IIncomeFinanceOperationRepository : IRepository<IncomeFinanceOperation, IncomeFinanceOperationId>
    {
    }
}
