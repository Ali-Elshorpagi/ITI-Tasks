using Chat.API.Data;
using Chat.API.Hubs;
using Chat.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Services
{
    public class AuditService
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<ChatHub> _hub;

        public AuditService(AppDbContext db, IHubContext<ChatHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        public async Task LogAsync(string userId, string userName, string action, string details = "")
        {
            var log = new AuditLog
            {
                UserId = userId,
                UserName = userName,
                Action = action,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            _db.AuditLogs.Add(log);
            await _db.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("AuditUpdated", new
            {
                id = log.Id,
                userId = log.UserId,
                userName = log.UserName,
                action = log.Action,
                details = log.Details,
                timestamp = log.Timestamp
            });
        }
    }
}
