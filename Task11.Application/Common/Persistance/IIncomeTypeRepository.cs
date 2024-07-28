using System.Security.Cryptography;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IIncomeTypeRepository
    {
        public IAsyncEnumerable<IncomeType> GetAllAsAsyncEnumerable();

        public Task<IncomeType> GetByIdAsync(IncomeTypeId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<IncomeType>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(IncomeType entity, CancellationToken cancellationToken);

        public Task UpdateAsync(IncomeType entity, CancellationToken cancellationToken);

        public Task DeleteAsync(IncomeType entity, CancellationToken cancellationToken);
    }
}
