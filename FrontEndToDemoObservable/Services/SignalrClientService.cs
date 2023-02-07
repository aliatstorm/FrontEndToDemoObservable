using Microsoft.AspNetCore.SignalR.Client;

namespace FrontEndToDemoObservable.Services
{
    public class SignalrClientService
    {
        private readonly HubConnection _signalRHubConnection;
        private readonly SessionApiService _sessionApiService;

        public SignalrClientService(HubConnection signalRHubConnection,
            SessionApiService sessionApiService)
        {
            _signalRHubConnection = signalRHubConnection;
            _sessionApiService = sessionApiService;
        }

        public async Task<bool> ConnectWithRetryAsync(CancellationToken token)
        {
            // Keep trying to until we can start or the token is canceled.
            while (true)
            {
                try
                {
                    await _signalRHubConnection.StartAsync(token);
                    return true;
                }
                catch when (token.IsCancellationRequested)
                {
                    Console.WriteLine($"ConnectWithRetryAsync: Cancellation Requested");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ConnectWithRetryAsync: {ex.Message}");
                    // Try again in a few seconds. This could be an incremental interval
                    await Task.Delay(5000);
                }
            }
        }

        public async Task StartUserSession()
        {
            await _signalRHubConnection.SendAsync("ConnectToSignalRHub", "testuser");
            SetHubConnection();
        }

        public async Task SetHubConnection()
        {
            _signalRHubConnection.On<string>("SignalRTestMessage", async (request) =>
            {
                Console.WriteLine($"Its a test message for atc id {request}");
                await _sessionApiService.GetSessions(request);
            });
        }

    }
}
