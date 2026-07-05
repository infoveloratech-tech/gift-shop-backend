using gift_shop.Models;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gift_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // =========================
        // GET: api/users
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // =========================
        // GET: api/users/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // =========================
        // POST: api/users
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(user);

            return Ok(new
            {
                message = "User created successfully",
                data = createdUser
            });
        }

        // =========================
        // PUT: api/users/{id}
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest(new { message = "User ID mismatch" });

            var updated = await _userService.UpdateUserAsync(user);

            if (!updated)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User updated successfully" });
        }

        // =========================
        // DELETE: api/users/{id}
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
    
}
