namespace Chat.API.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
    }
}
