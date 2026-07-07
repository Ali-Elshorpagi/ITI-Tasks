namespace Chat.API.Models
{
    // A direct message between two users
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        // Navigation properties
        public ApplicationUser? Sender { get; set; }
        public ApplicationUser? Receiver { get; set; }
    }
}
