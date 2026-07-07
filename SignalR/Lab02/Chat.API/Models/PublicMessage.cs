namespace Chat.API.Models
{
    // A message sent in a public chat room
    public class PublicMessage
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ChatRoom? ChatRoom { get; set; }
        public ApplicationUser? Sender { get; set; }
    }
}
