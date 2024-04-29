namespace ZenDev.BusinessLogic.Models
{
    public class ResultModel
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
