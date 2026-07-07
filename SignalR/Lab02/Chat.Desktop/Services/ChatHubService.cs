using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Desktop.Services
{
    public class RoomMessageDto
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string SenderId { get; set; } = "";
        public string SenderName { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime SentAt { get; set; }
    }

    public class PrivateMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = "";
        public string SenderName { get; set; } = "";
        public string ReceiverId { get; set; } = "";
        public string ReceiverName { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime SentAt { get; set; }
    }

    public class AuditLogDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Action { get; set; } = "";
        public string Details { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }

    public class ChatHubService
    {
        private HubConnection? _connection;
        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public event Action<string, string>? UserConnected;
        public event Action<string, string>? UserDisconnected;
        public event Action<RoomMessageDto>? RoomMessageReceived;
        public event Action<PrivateMessageDto>? PrivateMessageReceived;
        public event Action? RoomsUpdated;
        public event Action<AuditLogDto>? AuditUpdated;

        public async Task StartAsync(string token)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7000/hubs/chat", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                    options.HttpMessageHandlerFactory = _ => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                    };
                })
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("UserConnected", (userId, userName) =>
                UserConnected?.Invoke(userId, userName));

            _connection.On<string, string>("UserDisconnected", (userId, userName) =>
                UserDisconnected?.Invoke(userId, userName));

            _connection.On<RoomMessageDto>("ReceiveRoomMessage", (msg) =>
                RoomMessageReceived?.Invoke(msg));

            _connection.On<PrivateMessageDto>("ReceivePrivateMessage", (msg) =>
                PrivateMessageReceived?.Invoke(msg));

            _connection.On("RoomsUpdated", () =>
                RoomsUpdated?.Invoke());

            _connection.On<AuditLogDto>("AuditUpdated", (log) =>
                AuditUpdated?.Invoke(log));

            await _connection.StartAsync();
        }

        public async Task JoinRoomAsync(int roomId)
        {
            if (_connection != null)
                await _connection.InvokeAsync("JoinRoom", roomId);
        }

        public async Task LeaveRoomAsync(int roomId)
        {
            if (_connection != null)
                await _connection.InvokeAsync("LeaveRoom", roomId);
        }

        public async Task SendRoomMessageAsync(int roomId, string content)
        {
            if (_connection != null)
                await _connection.InvokeAsync("SendRoomMessage", roomId, content);
        }

        public async Task SendPrivateMessageAsync(string receiverId, string content)
        {
            if (_connection != null)
                await _connection.InvokeAsync("SendPrivateMessage", receiverId, content);
        }

        public async Task StopAsync()
        {
            if (_connection != null)
                await _connection.StopAsync();
        }
    }
}
