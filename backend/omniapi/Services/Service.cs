public class Service<TEntity, TDto> : IService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    IRepository<TEntity>_repository;
    public Service(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity> AddAsync(TDto dto)
    {
        var entity = Activator.CreateInstance<TEntity>();

        foreach(var propDto in typeof(TDto).GetProperties())
        {
            var propEntity = typeof(TEntity).GetProperty(propDto.Name);
            if(propEntity != null && propEntity.CanWrite)
            {
                propEntity.SetValue(entity, propDto.GetValue(dto));
            }
        }

        var created_entity = _repository.AddAsync(entity).Result;
        await _repository.SaveChangesAsync();

        return created_entity;
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IEnumerable<TEntity> entities = await _repository.GetAllAsync();
        return entities;
    }

    public Task RemoveAsync(Guid id)
    {
        _repository.RemoveAsync(id);
        _repository.SaveChangesAsync();
        return Task.CompletedTask;
    }
}
