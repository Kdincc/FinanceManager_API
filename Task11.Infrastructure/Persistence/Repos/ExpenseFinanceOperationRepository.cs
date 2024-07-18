using Microsoft.EntityFrameworkCore;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;

namespace Task11.Infrastructure.Persistence.Repos
{
    public sealed class ExpenceFinanceOperationRepository(FinanceDbContext dbContext) : IRepository<ExpenseFinanceOperation, ExpenseFinanceOperationId>
    {
        private readonly FinanceDbContext _dbContext = dbContext;

        public async Task AddAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken)
        {
            await _dbContext.ExpenseFinanceOperations.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken)
        {
            _dbContext.ExpenseFinanceOperations.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IAsyncEnumerable<ExpenseFinanceOperation> GetAllAsAsyncEnumerable()
        {
            return _dbContext.ExpenseFinanceOperations.AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<ExpenseFinanceOperation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.ExpenseFinanceOperations.ToListAsync(cancellationToken);
        }

        public async Task<ExpenseFinanceOperation> GetByIdAsync(ExpenseFinanceOperationId id, CancellationToken cancellationToken)
        {
            return await _dbContext.ExpenseFinanceOperations.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(ExpenseFinanceOperation entity, CancellationToken cancellationToken)
        {
            _dbContext.ExpenseFinanceOperations.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
