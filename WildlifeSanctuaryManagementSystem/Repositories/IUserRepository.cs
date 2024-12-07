using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IUserRepository
    {
       public Task<IEnumerable<User>> GetAllUsers();
      public Task<User>GetUserById(int id);

        public Task<User> GetUserByEmail(string email);
        public Task AddUser(User user);
      public  Task UpdateUser(User user);
       public Task DeleteUser(int id);
        public Task<IEnumerable<Object>> GetUsersByRole(string role);
    }
}
