using Microsoft.EntityFrameworkCore;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Infrastructure.Persistence.Repos
{
    public sealed class ExpenseTypeRepository(FinanceDbContext dbContext) : IExpenseTypeRepository
    {
        private readonly FinanceDbContext _dbContext = dbContext;

        public async Task AddAsync(ExpenseType entity, CancellationToken cancellationToken)
        {
            await _dbContext.ExpenseTypes.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ExpenseType entity, CancellationToken cancellationToken)
        {
            _dbContext.ExpenseTypes.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<ExpenseType> GetAllAsAsyncEnumerable()
        {
            return _dbContext.ExpenseTypes.AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<ExpenseType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.ExpenseTypes.ToListAsync(cancellationToken);
        }

        public async Task<ExpenseType> GetByIdAsync(ExpenseTypeId id, CancellationToken cancellationToken)
        {
            ExpenseType expenseType = await _dbContext.ExpenseTypes.FirstOrDefaultAsync(e => e.Id == id);

            return expenseType;
        }

        public async Task UpdateAsync(ExpenseType entity, CancellationToken cancellationToken)
        {
            _dbContext.ExpenseTypes.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
