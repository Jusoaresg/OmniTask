public interface IRepository<T> where T : class
{
    Task<T> Get(Guid id);

    Task<IEnumerable<T>> GetAll();

    void Add(T entity);

    void Remove(Guid id);
}
