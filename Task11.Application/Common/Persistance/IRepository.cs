using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Application.Common.Persistance
{
    public interface IRepository<T, TId> where T : notnull where TId : notnull
    {
        public Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(T entity, CancellationToken cancellationToken);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken);

        public Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
