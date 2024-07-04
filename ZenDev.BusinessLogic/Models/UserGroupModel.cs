namespace ZenDev.BusinessLogic.Models
{
    public class UserGroupResultModel
    {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public long GroupId {get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}