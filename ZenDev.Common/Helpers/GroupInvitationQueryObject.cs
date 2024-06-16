namespace ZenDev.Common.Helpers
{
    public class GroupInvitationQueryObject
    {
        public string? SearchQuery { get; set; } = null;
        public long? UserToExclude { get; set; } = -1;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
