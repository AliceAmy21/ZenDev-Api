namespace ZenDev.SignalRHost.Hubs
{
    public interface IInvitationClient
    {
        public Task ConnectionSuccessful(string message);
        public Task InvitationSentSuccessful(string message);
        public Task InvitationSentUnsuccessful(string message);
    }
}
