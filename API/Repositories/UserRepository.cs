using API.Controllers;
using API.interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
