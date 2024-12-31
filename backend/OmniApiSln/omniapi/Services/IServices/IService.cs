public interface IService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    public Task<TEntity> AddAsync(TDto dto);
    public Task<TEntity> GetAsync(Guid id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task RemoveAsync(Guid id);
}
