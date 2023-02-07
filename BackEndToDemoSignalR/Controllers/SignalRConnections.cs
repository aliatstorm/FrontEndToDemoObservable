using BackEndToDemoSignalR.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndToDemoSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRConnections : ControllerBase
    {
        private readonly SignalRConnectionMapping<string> _signalRConnections;
        private readonly SignalRHub _signalrHub;

        public SignalRConnections(SignalRConnectionMapping<string> signalRConnections,
            SignalRHub signalrHub)
        {
            _signalRConnections = signalRConnections;
            _signalrHub = signalrHub;
        }

        [HttpGet]
        public Dictionary<string, HashSet<string>> GetAllConnections()
        {
            return _signalRConnections.GetConnections();
        }

        [HttpPost]
        public async Task SendTestMessageToUser(string atcId, string userId, CancellationToken cancellationToken)
        {
            await _signalrHub.SignalRTestMessage(atcId, userId, cancellationToken);
        }
    }
}
