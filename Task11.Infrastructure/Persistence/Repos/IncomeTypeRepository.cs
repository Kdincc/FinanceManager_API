using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Infrastructure.Persistence.Repos
{
    public sealed class IncomeTypeRepository(FinanceDbContext dbContext) : IRepository<IncomeType, IncomeTypeId>
    {
        private readonly FinanceDbContext _dbContext = dbContext;

        public async Task AddAsync(IncomeType entity, CancellationToken cancellationToken)
        {
            await _dbContext.IncomeTypes.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(IncomeType entity, CancellationToken cancellationToken)
        {
            _dbContext.IncomeTypes.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IAsyncEnumerable<IncomeType> GetAllAsAsyncEnumerable()
        {
            return _dbContext.IncomeTypes.AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<IncomeType>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.IncomeTypes.ToListAsync(cancellationToken);
        }

        public async Task<IncomeType> GetByIdAsync(IncomeTypeId id, CancellationToken cancellationToken)
        {
            IncomeType incomeType = await _dbContext.IncomeTypes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

            return incomeType;
        }

        public async Task UpdateAsync(IncomeType entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
