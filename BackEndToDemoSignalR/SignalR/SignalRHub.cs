using Microsoft.AspNetCore.SignalR;

namespace BackEndToDemoSignalR.SignalR
{
    public class SignalRHub : Hub
    {
        private readonly SignalRConnectionMapping<string> _signalRConnections;

        public SignalRHub(SignalRConnectionMapping<string> signalRConnections)
        {
            _signalRConnections = signalRConnections;
        }

        public async Task ConnectToSignalRHub(string userId)
        {
            this._signalRConnections.Add(userId, Context.ConnectionId);
            await Clients.Caller.SendAsync("Connected");
        }

        public async Task SignalRTestMessage(string atcId, string userId, CancellationToken token)
        {
            var invigilatorConnections = this._signalRConnections.GetConnections(userId);
            if (invigilatorConnections.Any())
            {
                await Clients.Clients(invigilatorConnections).SendAsync("SignalRTestMessage", atcId, token);
            }
        }
    }
}
