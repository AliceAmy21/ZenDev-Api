namespace ZenDev.Api.ApiModels
{
    public class UserApiModel
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string StravaRefreshToken { get; set; } = string.Empty;
        public long Streak { get; set; }
        public string AvatarIconUrl { get; set; } = string.Empty;
        public DateTimeOffset LastActive { get; set; }
        public DateTimeOffset? LastSynced { get; set; } 
    }
}