using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGeneration _jwtTokenGeneration;

        public UserService(IUserRepository userRepository, JwtTokenGeneration jwtTokenGeneration)
        {
            _userRepository = userRepository;
            _jwtTokenGeneration = jwtTokenGeneration;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task UpdateUser(User user)
        {

            var existingUser = await _userRepository.GetUserById(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.UpdateUser(user);
        }

        // Delete User
        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }


            await _userRepository.DeleteUser(id);
        }

        // Registers a new user
        public async Task<bool> RegisterUser(User user)
        {

            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            // Hash the user's password before saving
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Add the user to the database
            await _userRepository.AddUser(user);
            return true;
        }

        // Authenticates a user and returns a JWT token
        public async Task<string> AuthenticateUser(string email, string password)
        {
            // Retrieve the user by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Generate a JWT token
            var token = _jwtTokenGeneration.GenerateToken(user.UserId, user.Username, user.Email, user.Role);
            return token;
        }

        // Verifies a plaintext password against a hashed password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<IEnumerable<Object>> GetUsersByRole(string role)
        {
            return await _userRepository.GetUsersByRole(role);
        }
    }  
    }

