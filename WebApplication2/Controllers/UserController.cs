using API.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        try
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            // Логирование ошибок
            return StatusCode(500, new { Message = "Internal Server Error" });
        }
    }
}
