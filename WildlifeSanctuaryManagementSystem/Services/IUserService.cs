using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(User user);
        Task<string> AuthenticateUser(string email, string password);

        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(int id);

        

        public Task UpdateUser(User user);
        public Task DeleteUser(int id);

        Task<IEnumerable<Object>> GetUsersByRole(string role);
    }
}
