using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IExpenseTypeRepository
    {
        public IAsyncEnumerable<ExpenseType> GetAllAsAsyncEnumerable();

        public Task<ExpenseType> GetByIdAsync(ExpenseTypeId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<ExpenseType>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(ExpenseType entity, CancellationToken cancellationToken);

        public Task UpdateAsync(ExpenseType entity, CancellationToken cancellationToken);

        public Task DeleteAsync(ExpenseType entity, CancellationToken cancellationToken);
    }
}
