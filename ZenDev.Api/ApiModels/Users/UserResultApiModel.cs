namespace ZenDev.Api.ApiModels
{
    public class UserResultApiModel
    {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}