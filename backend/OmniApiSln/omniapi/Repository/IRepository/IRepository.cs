public interface IRepository<T> where T : class
{
    Task<T> GetAsync(Guid id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> AddAsync(T entity);

    Task RemoveAsync(Guid id);

    Task SaveChangesAsync();
}
