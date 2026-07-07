using Chat.API.Data;
using Chat.API.DTOs;
using Chat.API.Hubs;
using Chat.API.Models;
using Chat.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Chat.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly AuditService _audit;
        private readonly IHubContext<ChatHub> _hub;

        public RoomsController(AppDbContext db, AuditService audit, IHubContext<ChatHub> hub)
        {
            _db = db;
            _audit = audit;
            _hub = hub;
        }

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        private string CurrentUserName => User.FindFirstValue(ClaimTypes.Name) ?? "";

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _db.ChatRooms
                .Include(r => r.CreatedBy)
                .Include(r => r.Members)
                .ToListAsync();

            var response = rooms.Select(r => new RoomResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                CreatedByUserName = r.CreatedBy?.UserName ?? "",
                CreatedAt = r.CreatedAt,
                MemberCount = r.Members.Count,
                IsMember = r.Members.Any(m => m.UserId == CurrentUserId)
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _db.ChatRooms
                .Include(r => r.CreatedBy)
                .Include(r => r.Members)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null) return NotFound();

            return Ok(new RoomResponse
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                CreatedByUserName = room.CreatedBy?.UserName ?? "",
                CreatedAt = room.CreatedAt,
                MemberCount = room.Members.Count,
                IsMember = room.Members.Any(m => m.UserId == CurrentUserId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request)
        {
            var room = new ChatRoom
            {
                Name = request.Name,
                Description = request.Description,
                CreatedByUserId = CurrentUserId
            };

            _db.ChatRooms.Add(room);
            await _db.SaveChangesAsync();

            _db.RoomMembers.Add(new RoomMember { ChatRoomId = room.Id, UserId = CurrentUserId });
            await _db.SaveChangesAsync();

            await _audit.LogAsync(CurrentUserId, CurrentUserName, "CreateRoom", $"Room: {room.Name}");

            await _hub.Clients.All.SendAsync("RoomsUpdated");

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, new { room.Id, room.Name });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _db.ChatRooms.FindAsync(id);
            if (room == null) return NotFound();

            if (room.CreatedByUserId != CurrentUserId)
                return Forbid();

            _db.ChatRooms.Remove(room);
            await _db.SaveChangesAsync();

            await _audit.LogAsync(CurrentUserId, CurrentUserName, "DeleteRoom", $"Room: {room.Name}");

            await _hub.Clients.All.SendAsync("RoomsUpdated");

            return NoContent();
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var room = await _db.ChatRooms.FindAsync(id);
            if (room == null) return NotFound();

            var alreadyMember = await _db.RoomMembers
                .AnyAsync(m => m.ChatRoomId == id && m.UserId == CurrentUserId);

            if (alreadyMember)
                return BadRequest(new { message = "Already a member." });

            _db.RoomMembers.Add(new RoomMember { ChatRoomId = id, UserId = CurrentUserId });
            await _db.SaveChangesAsync();

            await _audit.LogAsync(CurrentUserId, CurrentUserName, "JoinRoom", $"Room: {room.Name}");

            await _hub.Clients.All.SendAsync("RoomsUpdated");

            return Ok(new { message = "Joined successfully." });
        }

        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveRoom(int id)
        {
            var membership = await _db.RoomMembers
                .FirstOrDefaultAsync(m => m.ChatRoomId == id && m.UserId == CurrentUserId);

            if (membership == null)
                return BadRequest(new { message = "Not a member of this room." });

            var room = await _db.ChatRooms.FindAsync(id);

            _db.RoomMembers.Remove(membership);
            await _db.SaveChangesAsync();

            await _audit.LogAsync(CurrentUserId, CurrentUserName, "LeaveRoom", $"Room: {room?.Name}");

            await _hub.Clients.All.SendAsync("RoomsUpdated");

            return Ok(new { message = "Left successfully." });
        }
    }
}
