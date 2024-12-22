public interface IUserRepository : IRepository<User>
{
    public void Add(User entity);
}
