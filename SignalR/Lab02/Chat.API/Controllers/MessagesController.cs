using Chat.API.Data;
using Chat.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Chat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MessagesController(AppDbContext db)
        {
            _db = db;
        }

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetRoomMessages(int roomId, int page = 1, int pageSize = 50)
        {
            var messages = await _db.PublicMessages
                .Where(m => m.ChatRoomId == roomId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new PublicMessageResponse
                {
                    Id = m.Id,
                    ChatRoomId = m.ChatRoomId,
                    SenderId = m.SenderId,
                    SenderName = m.Sender!.UserName ?? "",
                    Content = m.Content,
                    SentAt = m.SentAt
                })
                .ToListAsync();

            return Ok(messages.OrderBy(m => m.SentAt));
        }

        [HttpGet("private/{userId}")]
        public async Task<IActionResult> GetPrivateMessages(string userId, int page = 1, int pageSize = 50)
        {
            var messages = await _db.PrivateMessages
                .Where(m =>
                    (m.SenderId == CurrentUserId && m.ReceiverId == userId) ||
                    (m.SenderId == userId && m.ReceiverId == CurrentUserId))
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new PrivateMessageResponse
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    SenderName = m.Sender!.UserName ?? "",
                    ReceiverId = m.ReceiverId,
                    ReceiverName = m.Receiver!.UserName ?? "",
                    Content = m.Content,
                    SentAt = m.SentAt,
                    IsRead = m.IsRead
                })
                .ToListAsync();

            return Ok(messages.OrderBy(m => m.SentAt));
        }
    }
}
