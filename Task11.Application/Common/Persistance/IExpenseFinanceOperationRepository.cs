using System.Security.Cryptography;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IExpenseFinanceOperationRepository
    {
        public IAsyncEnumerable<ExpenseFinanceOperation> GetAllAsAsyncEnumerable();

        public Task<ExpenseFinanceOperation> GetByIdAsync(ExpenseFinanceOperationId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<ExpenseFinanceOperation>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken);

        public Task UpdateAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken);

        public Task DeleteAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken);
    }
}
