using Chat.API.Data;
using Chat.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    UserName = u.UserName ?? "",
                    DisplayName = u.DisplayName,
                    Email = u.Email ?? "",
                    IsOnline = u.IsOnline
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("online")]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var users = await _db.Users
                .Where(u => u.IsOnline)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    UserName = u.UserName ?? "",
                    DisplayName = u.DisplayName,
                    Email = u.Email ?? "",
                    IsOnline = true
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            return Ok(new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName ?? "",
                DisplayName = user.DisplayName,
                Email = user.Email ?? "",
                IsOnline = user.IsOnline
            });
        }
    }
}
