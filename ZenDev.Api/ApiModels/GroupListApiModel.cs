namespace ZenDev.Api.ApiModels
{
    public class GroupListApiModel
    {
        public long GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public string GroupIconUrl { get; set; } = string.Empty;

        public long MemberCount { get; set; }

        public ExerciseTypeApiModel? ExerciseType { get; set; }
    }
}
