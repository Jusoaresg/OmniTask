using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
        }

        try
        {
            _context.Add(entity);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Erro ao adicionar entidado ao banco de dados.", e);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro.", e);
        }
    }

    public async Task<T> Get(Guid id)
    {
        try
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
            }
            return entity;
        }
        catch (ArgumentNullException ex)
        {
            throw new KeyNotFoundException($"Não foi encontrada uma entidade com o id: {id}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Um erro ocorreu ao buscar a entidade com o id: {id}", ex);
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IEnumerable<T> users = await _context.Set<T>().ToListAsync();
        return users;
        throw new NotImplementedException();
    }
    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}
