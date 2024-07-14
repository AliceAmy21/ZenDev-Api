namespace ZenDev.SignalRHost.Configuration
{
    public class LogLevelConfiguration
    {
        public LogLevel Default { get; set; } = LogLevel.Information;
        public LogLevel MicrosoftAspNetCore { get; set; } = LogLevel.Information;
        public LogLevel MicrosoftAspNetCoreSignalR { get; set; } = LogLevel.Information;
        public LogLevel MicrosoftAspNetCoreHttpConnections { get; set; } = LogLevel.Information;
    }
}