namespace ZenDev.Api.ApiModels
{
    public class UserGroupResultApiModel
    {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}