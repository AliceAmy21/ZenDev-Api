using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace ZenDev.SignalRHost.Hubs
{
    public class ChatroomHub : Hub<IChatroomClient>
    {
        public const string HUB_IDENTIFIER = "chatroom-hub";
        
        public class GroupConnection
        {
            public long ConnectionId {  get; set; }
            public long GroupId {  get; set; }
        }

        public async Task AddToGroup(long groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        public async Task RemoveFromGroup(long groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        public async Task SendMessage(long userId, string messageContent, long groupId, DateTime timeSent)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", userId, messageContent,);
        }

        //public async Task AddReaction(long userId, long messageId, long reactionId, long groupId)
        //{
        //    await Clients.Group(groupId.ToString()).SendAsync("ReceiveReaction", userId, messageId, reactionId);
        //}

        //public async Task RemoveReaction(long userId, long messageId, long reactionId, long groupId)
        //{ 
        //    await Clients.Group(groupId.ToString()).SendAsync("RemoveReaction", userId, messageId, reactionId);
        //}
    }
}
