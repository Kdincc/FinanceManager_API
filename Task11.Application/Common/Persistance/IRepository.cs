namespace Task11.Application.Common.Persistance
{
    public interface IRepository<T, TId> where T : notnull where TId : notnull
    {
        public IAsyncEnumerable<T> GetAllAsAsyncEnumerable();

        public Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);

        public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

        public Task AddAsync(T entity, CancellationToken cancellationToken);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken);

        public Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
