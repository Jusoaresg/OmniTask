using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    readonly ApplicationDbContext _context;
    internal DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula.");
        }

        try
        {
            await _dbSet.AddAsync(entity);
            return entity;
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

    public async Task<T> GetAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
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

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        IEnumerable<T> entities = await _dbSet.ToListAsync();
        return entities;
    }

    public async Task RemoveAsync(Guid id)
    {
        var user = await _dbSet.FindAsync(id);
        if (user is null)
        {
            throw new KeyNotFoundException("Usuario não encontrado");
        }
        _dbSet.Remove(user);

        return;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
