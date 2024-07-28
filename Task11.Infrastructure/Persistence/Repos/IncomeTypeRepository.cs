using Microsoft.EntityFrameworkCore;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Infrastructure.Persistence.Repos
{
    public sealed class IncomeTypeRepository(FinanceDbContext dbContext) : IIncomeTypeRepository
    {
        private readonly FinanceDbContext _dbContext = dbContext;

        public async Task AddAsync(Domain.IncomeType.IncomeType entity, CancellationToken cancellationToken)
        {
            await _dbContext.IncomeTypes.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Domain.IncomeType.IncomeType entity, CancellationToken cancellationToken)
        {
            _dbContext.IncomeTypes.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IAsyncEnumerable<Domain.IncomeType.IncomeType> GetAllAsAsyncEnumerable()
        {
            return _dbContext.IncomeTypes.AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<Domain.IncomeType.IncomeType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.IncomeTypes.ToListAsync(cancellationToken);
        }

        public async Task<Domain.IncomeType.IncomeType> GetByIdAsync(IncomeTypeId id, CancellationToken cancellationToken)
        {
            Domain.IncomeType.IncomeType incomeType = await _dbContext.IncomeTypes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            return incomeType;
        }

        public async Task UpdateAsync(Domain.IncomeType.IncomeType entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
