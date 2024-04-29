using Microsoft.AspNetCore.SignalR;

namespace ZenDev.SignalRHost.Hubs
{
    public sealed class ExampleHub : Hub<IExampleClient>
    {
        public const string HUB_IDENTIFIER = "example-hub";

        public static readonly Dictionary<string, string> _chatConnections = new Dictionary<string, string>();

        public async Task JoinChat(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                await Clients.Caller.JoinChatUnsuccessful("Invalid username");
                return;
            }

            var connectionExists = _chatConnections.ContainsKey(Context.ConnectionId);
            var test = Context.UserIdentifier;
            if (!connectionExists)
            {
                _chatConnections.Add(Context.ConnectionId, username);
            }

            await Clients.Caller.JoinChatSuccessful(Context.ConnectionId);
        }

        public async Task SendMessage(string message)
        {
            var connectionExists = _chatConnections.ContainsKey(Context.ConnectionId);
            if (!connectionExists)
            {
                await Clients.Caller.SendMessageUnsuccessful("You have not joined the chat");
                return;
            }

            await Clients.Caller.SendMessageSuccessful(Context.ConnectionId);

            var username = _chatConnections.GetValueOrDefault(Context.ConnectionId);

            var broadcastResponse = BuildResponse(username ?? string.Empty, message);
            await Clients.All.NewMessage(broadcastResponse);
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.ConnectionSuccessful(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        private string BuildResponse(params string[] parameters)
        {
            if (parameters.Count() == 0) return string.Empty;

            var response = parameters.First();

            for (var i = 1; i < parameters.Count(); i++)
            {
                response = $"{response}:{parameters[i]}";
            }

            return response;
        }
    }
}
