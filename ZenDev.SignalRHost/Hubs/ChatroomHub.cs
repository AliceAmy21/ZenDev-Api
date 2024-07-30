using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using ZenDev.Api.ApiModels;
using ZenDev.SignalRHost.Models;


namespace ZenDev.SignalRHost.Hubs
{
    public class ChatroomHub : Hub<IExampleClient>
    {
        public const string HUB_IDENTIFIER = "chatroom-hub";
        

        public static readonly Dictionary<string, long> _chatConnections = new Dictionary<string, long>();
        public static readonly Dictionary<long, string> _chatUsers = new Dictionary<long, string>();
        
        public async Task JoinChat(long userId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            { 
                await Clients.Caller.JoinChatUnsuccessful("Invalid username");
                return;
            }

            var connectionExists = _chatConnections.ContainsValue(userId);
            
           
            _chatConnections.Add(Context.ConnectionId, userId);
            _chatUsers.Add(userId, userName);
            
            await Clients.Caller.JoinChatSuccessful(Context.ConnectionId);
      
           
        }

        

        public async Task SendMessage(MessageApiModel message)
        {
            var userId = _chatConnections.GetValueOrDefault(Context.ConnectionId);
            var userName = _chatUsers.GetValueOrDefault(userId);
            var connectionExists = _chatConnections.ContainsKey(Context.ConnectionId);
            if (!connectionExists)
            {
              
                _chatConnections.Add(Context.ConnectionId, userId);
                _chatUsers.Add(userId, userName);
                await Clients.Caller.SendMessageSuccessful("Added Connection");
               
            }

            await Clients.Caller.SendMessageSuccessful(Context.ConnectionId);

            
            await Clients.All.NewMessage(message);

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
