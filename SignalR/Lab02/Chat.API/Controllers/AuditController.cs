using Chat.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuditController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(int page = 1, int pageSize = 50)
        {
            var logs = await _db.AuditLogs
                .OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new
                {
                    l.Id,
                    l.UserId,
                    l.UserName,
                    l.Action,
                    l.Details,
                    l.Timestamp
                })
                .ToListAsync();

            return Ok(logs);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserLogs(string userId)
        {
            var logs = await _db.AuditLogs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .Take(100)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
