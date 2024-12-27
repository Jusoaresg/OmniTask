public class UserService : Service<User, UserDto>, IUserService
{
    IUserRepository _userRepository;
    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        await _userRepository.SaveChangesAsync();
        return user;
    }

    public Task RemoveByEmail(string email)
    {
        _userRepository.RemoveByEmail(email);
        _userRepository.SaveChangesAsync();
        return Task.CompletedTask;
    }
}
