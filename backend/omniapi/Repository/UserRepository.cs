public class UserRepository : Repository<User>, IUserRepository
{
    ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _dbSet.FindAsync(email);
        if (user is null)
        {
            throw new KeyNotFoundException("Usuario não encontrado");
        }
        return user;
    }

    public Task RemoveByEmail(string email)
    {
        var user = _dbSet.FirstOrDefault(e => e.Email == email);
        if (user is null)
        {
            throw new KeyNotFoundException("Usuario não encontrado");
        }
        _dbSet.Remove(user);
        return Task.CompletedTask;
    }
}
