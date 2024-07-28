using Microsoft.EntityFrameworkCore;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Infrastructure.Persistence.Repos
{
    public sealed class IncomeFinanceOperationRepository(FinanceDbContext dbContext) : IIncomeFinanceOperationRepository
    {
        private readonly FinanceDbContext _dbContext = dbContext;

        public async Task AddAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken)
        {
            await _dbContext.IncomeFinanceOperations.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken)
        {
            _dbContext.IncomeFinanceOperations.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IAsyncEnumerable<IncomeFinanceOperation> GetAllAsAsyncEnumerable()
        {
            return _dbContext.IncomeFinanceOperations.AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<IncomeFinanceOperation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.IncomeFinanceOperations.ToListAsync(cancellationToken);
        }

        public Task<IncomeFinanceOperation> GetByIdAsync(IncomeFinanceOperationId id, CancellationToken cancellationToken)
        {
            return _dbContext.IncomeFinanceOperations.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(IncomeFinanceOperation entity, CancellationToken cancellationToken)
        {
            _dbContext.IncomeFinanceOperations.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
