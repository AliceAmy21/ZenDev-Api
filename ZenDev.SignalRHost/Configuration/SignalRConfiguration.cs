namespace ZenDev.SignalRHost.Configuration
{
    public class SignalRConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string AllowedOrigins { get; set; }
        public Logging Logging { get; set; }
        public bool EnableSSL { get; set; }
    }
}