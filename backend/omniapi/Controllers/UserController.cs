using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDbContext _context;

    private readonly IUserRepository _userRepository;

    public UserController(ApplicationDbContext dbContext, ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _context = dbContext;
        _userRepository = userRepository;

    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        return Ok();
    }


    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto user)
    {
        return Ok();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        return Ok();
    }
}
