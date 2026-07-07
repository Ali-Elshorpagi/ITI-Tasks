namespace Chat.API.Models
{
    // Represents a public chat room that users can join
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser? CreatedBy { get; set; }
        public ICollection<RoomMember> Members { get; set; } = new List<RoomMember>();
        public ICollection<PublicMessage> Messages { get; set; } = new List<PublicMessage>();
    }
}
