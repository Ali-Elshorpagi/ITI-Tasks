namespace Chat.API.Models
{
    // Records important user actions for audit purposes
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;  // e.g. "Login", "JoinRoom"
        public string Details { get; set; } = string.Empty; // extra context
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
