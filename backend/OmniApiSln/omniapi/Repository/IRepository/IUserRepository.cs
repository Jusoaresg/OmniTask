public interface IUserRepository : IRepository<User>
{
    public Task<User> GetByEmail(string email);
    public Task RemoveByEmail(string email);
}
