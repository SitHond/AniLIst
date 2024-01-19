using API.Models;

namespace API.interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> GetUserByEmailOrUsername(string email, string username);
        Task<User> GetUserByUsername(string username);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
    }
}
