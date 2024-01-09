using API.Controllers;
using API.interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
                return false;

            // Ваша логика проверки пароля - сравнение хэшированного пароля пользователя с предоставленным паролем
            // Например, сравнение хэшей паролей

            return user.password == HashPassword(password); // Ваша логика хэширования пароля
        }
        private string HashPassword(string password)
        {
            // Логика хэширования пароля
            // Это просто пример, в реальной системе вам нужно использовать хэширование с солью и возможно более безопасные методы
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<User> GetUserByEmailOrUsername(string email, string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.email == email || u.username == username);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.username == username);
        }
    }
}
