using Microsoft.AspNetCore.Identity;

namespace Chat.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsOnline { get; set; } = false;

        public ICollection<RoomMember> RoomMemberships { get; set; } = new List<RoomMember>();
        public ICollection<PublicMessage> PublicMessages { get; set; } = new List<PublicMessage>();
        public ICollection<PrivateMessage> SentMessages { get; set; } = new List<PrivateMessage>();
        public ICollection<PrivateMessage> ReceivedMessages { get; set; } = new List<PrivateMessage>();
    }
}
