using Chat.API.Data;
using Chat.API.Models;
using Chat.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Chat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly AppDbContext _db;
        private readonly AuditService _audit;

        private static readonly ConcurrentDictionary<string, string> _connections = new();

        public ChatHub(AppDbContext db, AuditService audit)
        {
            _db = db;
            _audit = audit;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                _connections.TryAdd(Context.ConnectionId, userId);

                var user = await _db.Users.FindAsync(userId);
                if (user != null)
                {
                    user.IsOnline = true;
                    await _db.SaveChangesAsync();
                }

                await Clients.All.SendAsync("UserConnected", userId, userName);
                await _audit.LogAsync(userId, userName, "UserConnected", $"ConnectionId: {Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _connections.TryRemove(Context.ConnectionId, out var userId);

            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                bool hasOtherConnections = _connections.Values.Contains(userId);
                if (!hasOtherConnections)
                {
                    var user = await _db.Users.FindAsync(userId);
                    if (user != null)
                    {
                        user.IsOnline = false;
                        await _db.SaveChangesAsync();
                    }

                    await Clients.All.SendAsync("UserDisconnected", userId, userName);
                    await _audit.LogAsync(userId, userName, "UserDisconnected", "");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(int roomId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            var isMember = await _db.RoomMembers
                .AnyAsync(rm => rm.ChatRoomId == roomId && rm.UserId == userId);

            if (!isMember)
            {
                _db.RoomMembers.Add(new RoomMember
                {
                    ChatRoomId = roomId,
                    UserId = userId
                });
                await _db.SaveChangesAsync();
                await _audit.LogAsync(userId, userName, "JoinRoom", $"RoomId: {roomId}");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"room-{roomId}");
            await Clients.Group($"room-{roomId}").SendAsync("UserJoinedRoom", userId, userName, roomId);

            await Clients.All.SendAsync("RoomsUpdated");
        }

        public async Task LeaveRoom(int roomId)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            var membership = await _db.RoomMembers
                .FirstOrDefaultAsync(rm => rm.ChatRoomId == roomId && rm.UserId == userId);

            if (membership != null)
            {
                _db.RoomMembers.Remove(membership);
                await _db.SaveChangesAsync();
                await _audit.LogAsync(userId, userName, "LeaveRoom", $"RoomId: {roomId}");
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room-{roomId}");
            await Clients.Group($"room-{roomId}").SendAsync("UserLeftRoom", userId, userName, roomId);

            await Clients.All.SendAsync("RoomsUpdated");
        }

        public async Task SendRoomMessage(int roomId, string content)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            if (string.IsNullOrWhiteSpace(content)) return;

            var message = new PublicMessage
            {
                ChatRoomId = roomId,
                SenderId = userId,
                Content = content,
                SentAt = DateTime.UtcNow
            };
            _db.PublicMessages.Add(message);
            await _db.SaveChangesAsync();

            await _audit.LogAsync(userId, userName, "PublicMessage", $"RoomId: {roomId}");

            await Clients.Group($"room-{roomId}").SendAsync("ReceiveRoomMessage", new
            {
                id = message.Id,
                chatRoomId = roomId,
                senderId = userId,
                senderName = userName,
                content = content,
                sentAt = message.SentAt
            });
        }

        public async Task SendPrivateMessage(string receiverId, string content)
        {
            var senderId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var senderName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

            if (string.IsNullOrWhiteSpace(content)) return;

            var receiver = await _db.Users.FindAsync(receiverId);
            if (receiver == null) return;

            var message = new PrivateMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow
            };
            _db.PrivateMessages.Add(message);
            await _db.SaveChangesAsync();

            await _audit.LogAsync(senderId, senderName, "PrivateMessage", $"To: {receiver.UserName}");

            var payload = new
            {
                id = message.Id,
                senderId = senderId,
                senderName = senderName,
                receiverId = receiverId,
                receiverName = receiver.UserName,
                content = content,
                sentAt = message.SentAt
            };

            var receiverConnections = _connections
                .Where(c => c.Value == receiverId)
                .Select(c => c.Key)
                .ToList();

            foreach (var connId in receiverConnections)
            {
                await Clients.Client(connId).SendAsync("ReceivePrivateMessage", payload);
            }

            await Clients.Caller.SendAsync("ReceivePrivateMessage", payload);
        }

        public async Task NotifyUserOnline()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            await Clients.All.SendAsync("UserConnected", userId, userName);
        }

        public async Task NotifyUserOffline()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var userName = Context.User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
            await Clients.All.SendAsync("UserDisconnected", userId, userName);
        }
    }
}
