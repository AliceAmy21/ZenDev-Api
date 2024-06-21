namespace ZenDev.Api.ApiModels
{
    public class GroupInvitationApiModel
    {  
        public long GroupInvitationId { get; set; }

        public long GroupId { get; set; }

        public long InvitedUserId { get; set; }

        public long InviteSenderId { get; set; }
    }
}