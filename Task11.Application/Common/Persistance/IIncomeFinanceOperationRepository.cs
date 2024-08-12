using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IIncomeFinanceOperationRepository
    {
        public IAsyncEnumerable<IncomeFinanceOperation> GetAllAsAsyncEnumerable();

        public Task<IncomeFinanceOperation> GetByIdAsync(IncomeFinanceOperationId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<IncomeFinanceOperation>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken);

        public Task UpdateAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken);

        public Task DeleteAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken);
    }
}
