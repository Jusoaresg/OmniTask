using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDbContext _context;

    private readonly IUserService _userService;

    public UserController(ApplicationDbContext dbContext, ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _context = dbContext;
        _userService = userService;

    }

    [HttpGet("id/{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userService.GetAsync(userId);
        return Ok(user);
    }


    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetByEmail(email);
        return Ok(user);
    }


    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {

        var users = await _userService.GetAllAsync();
        return Ok(users);
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto user)
    {
        User _user = await _userService.AddAsync(user);
        return Ok(_user);
    }

    [HttpDelete("id/{userId}")]
    public async Task<IActionResult> RemoveUserById(Guid userId)
    {
        await _userService.RemoveAsync(userId);
        return Ok("Usuario deletado com sucesso");
    }

    [HttpDelete("email/{email}")]
    public async Task<IActionResult> RemoveUserByEmail(string email)
    {
        await _userService.RemoveByEmail(email);
        return Ok("Usuario deletado com sucesso");
    }
}
