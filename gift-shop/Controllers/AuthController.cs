using gift_shop.DTOs.Auth;
using gift_shop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gift_shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid email or password."
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Register
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Google Login
        /// </summary>
        [AllowAnonymous]
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var result = await _authService.GoogleLoginAsync(request);

            if (result == null)
                return Unauthorized(new
                {
                    success = false,
                    message = "Google authentication failed."
                });

            return Ok(result);
        }

        /// <summary>
        /// Current User Profile
        /// </summary>
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var profile = await _authService.GetProfileAsync(int.Parse(userIdClaim.Value));

            return Ok(profile);
        }

        /// <summary>
        /// Change Password
        /// </summary>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _authService.ChangePasswordAsync(userId, request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // JWT is stateless. Client should discard the token.
            return Ok(new
            {
                success = true,
                message = "Logged out successfully."
            });
        }
    }
}
