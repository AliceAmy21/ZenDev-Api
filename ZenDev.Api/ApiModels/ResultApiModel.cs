namespace ZenDev.Api.ApiModels
{
    public class ResultApiModel
    {
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
