namespace ZenDev.Api.ApiModels
{
    public class UserInviteApiModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public string AvatarIconUrl { get; set; }
    }
}