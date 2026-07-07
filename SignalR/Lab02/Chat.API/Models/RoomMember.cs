namespace Chat.API.Models
{
    // Tracks which users are members of which rooms
    public class RoomMember
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ChatRoom? ChatRoom { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
