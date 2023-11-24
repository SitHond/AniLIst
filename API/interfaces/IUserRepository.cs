using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> GetUserByEmailOrUsername(string email, string username);
        Task<User> GetUserByUsername(string username);
    }
}
