namespace ZenDev.Common.Helpers
{
    public class GroupQueryObject
    {
        public int? GroupExerciseTypeId { get; set; } = null;

        public string? SortBy { get; set; } = "";

        public bool? ShowMyGroups { get; set; } = null;

        public string? searchQuery { get; set; } = null;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 25;
    }
}
