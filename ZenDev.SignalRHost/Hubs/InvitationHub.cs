using Microsoft.AspNetCore.SignalR;

namespace ZenDev.SignalRHost.Hubs
{
    public sealed class InvitationHub : Hub<IInvitationClient>
    {
        public const string HUB_IDENTIFIER = "invitation-hub";

        public static readonly Dictionary<string, string> _invitationConnections = [];

         public override Task OnConnectedAsync()
        {
            Clients.Caller.ConnectionSuccessful(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}