namespace ZenDev.Migrations
{
    public class AchievementConfig
    {
        public long AchievementId { get; set; }
        public string AchievementName { get; set; } = string.Empty;
        public string AchievementDescription { get; set; } = string.Empty;
        public string AchievementIcon { get; set; } = string.Empty;
    }
}