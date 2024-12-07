using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //get all users
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        //Register User
        //[Authorize(Roles = "Manager,Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                await _userService.RegisterUser(user);
                return Ok(new { message = "User Registered Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                await _userService.UpdateUser(user);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found.");
            }
        }


        //Login User
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.AuthenticateUser(loginRequest.Email, loginRequest.Password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Role cannot be empty.");
            }

            var users = await _userService.GetUsersByRole(role);

            if (users == null || !users.Any())
            {
                return NotFound($"No users found with the role '{role}'.");
            }

            return Ok(users);
        }

    }
}
