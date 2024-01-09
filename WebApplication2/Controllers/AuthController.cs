using API.Controllers;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using System.Configuration;

namespace site.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, AppDbContext context, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetUserByEmailOrUsername(model.Email, model.Username);
            if (existingUser != null)
            {
                return StatusCode(400, new { Error = "User with this email or username already exists" });
            }

            var hashedPassword = HashPassword(model.Password);

            var user = new User
            {
                username = model.Username,
                email = model.Email,
                password = hashedPassword
            };

            await _userRepository.CreateUser(user);

            return StatusCode(201, new { Message = "Registration successful!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (model == null || string.IsNullOrEmpty(model.Password))
                {
                    // Логирование
                    Console.WriteLine("Invalid input data");
                    return BadRequest("Invalid input data");
                }

                var isValidUser = await _userRepository.ValidateUserCredentialsAsync(model.Username, model.Password);
                if (isValidUser)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var secretKey = _configuration["Jwt:SecretKey"];
                    if (secretKey != null)
                    {
                        var key = Encoding.ASCII.GetBytes(secretKey);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
        new Claim(ClaimTypes.Name, model.Username)
                            }),
                            NotBefore = DateTime.UtcNow, // Устанавливаем время начала действия
                            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])), // Устанавливаем срок действия
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
                        var accessTokenString = tokenHandler.WriteToken(accessToken);

                        var refreshTokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                        new Claim(ClaimTypes.Name, model.Username)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(60 * 24 * 7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);
                        var refreshTokenString = tokenHandler.WriteToken(refreshToken);

                        var user = await _userRepository.GetUserByUsername(model.Username);

                        var newAccessToken = new AccessToken
                        {
                            Token = accessTokenString,
                            Expires = DateTime.UtcNow.AddMinutes(15),
                            UserId = user.Id
                        };

                        var newRefreshToken = new RefreshToken
                        {
                            Token = refreshTokenString,
                            Expires = DateTime.UtcNow.AddMinutes(60 * 24 * 7),
                            UserId = user.Id
                        };

                        _context.AccessTokens.Add(newAccessToken);
                        _context.RefreshTokens.Add(newRefreshToken);
                        await _context.SaveChangesAsync();

                        return Ok(new { AccessToken = accessTokenString, RefreshToken = refreshTokenString });
                    }
                    else
                    {
                        // Обработка случая, когда значение secretKey равно null
                        Console.WriteLine("Jwt:SecretKey is null");
                    }

                }

                return Unauthorized("Incorrect username or password!");
            }
            catch (Exception ex)
            {
                // Логирование: выведите в журнал информацию об исключении
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpGet("check-authentication")]
        public IActionResult CheckAuthentication()
        {
            var username = HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(username))
            {
                return Ok(new { IsAuthenticated = true, Username = username });
            }

            return Ok(new { IsAuthenticated = false });
        }
    }
}
