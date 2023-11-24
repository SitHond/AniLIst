using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace site.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            // Проверка наличия пользователя с таким же email или username
            var existingUser = await _userRepository.GetUserByEmailOrUsername(email, username);
            if (existingUser != null)
            {
                return StatusCode(400, new { Error = "Пользователь с таким email или username уже существует" }); // Код 400: Bad Request
            }

            // Хеширование пароля
            var hashedPassword = HashPassword(password);

            // Создание нового пользователя
            var user = new User
            {
                username = username,
                email = email,
                password = hashedPassword
            };

            // Сохранение пользователя в базу данных
            await _userRepository.CreateUser(user);

            return StatusCode(201, new { Message = "Успешная регистрация!" }); // Код 201: Created
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }      
    }
}
