namespace Chat.Desktop.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string CreatedByUserName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int MemberCount { get; set; }
        public bool IsMember { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsOnline { get; set; }
    }
}
