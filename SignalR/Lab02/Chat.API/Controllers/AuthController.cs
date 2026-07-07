using Chat.API.DTOs;
using Chat.API.Models;
using Chat.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly AuditService _audit;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService,
            AuditService audit)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _audit = audit;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Email already registered." });

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                DisplayName = request.DisplayName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "Registration failed.", errors });
            }

            await _audit.LogAsync(user.Id, user.UserName ?? "", "Register", $"Email: {user.Email}");

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                UserName = user.UserName ?? "",
                DisplayName = user.DisplayName,
                Email = user.Email ?? ""
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrUsername)
                    ?? await _userManager.FindByNameAsync(request.EmailOrUsername);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid credentials." });

            await _audit.LogAsync(user.Id, user.UserName ?? "", "Login", "");

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                UserName = user.UserName ?? "",
                DisplayName = user.DisplayName,
                Email = user.Email ?? ""
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "";

            await _audit.LogAsync(userId, userName, "Logout", "");
            return Ok(new { message = "Logged out successfully." });
        }
    }
}
