public interface IUserService : IService<User, UserDto>
{
    public Task<User> GetByEmail(string email);
    public Task RemoveByEmail(string email);
}
