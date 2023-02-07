using FrontEndToDemoObservable.ViewModels;
using System.Net.Http.Json;

namespace FrontEndToDemoObservable.Services
{
    public class SessionApiService
    {
        private readonly HttpClient _apiClient;
        private readonly StateContainer _stateContainer;

        public SessionApiService(IHttpClientFactory httpClientFactory,
            StateContainer stateContainer)
        {
            _apiClient = httpClientFactory.CreateClient("baseAPIClient");
            _stateContainer = stateContainer;
        }

        public async Task GetSessions(string atcId)
        {
            var url = $"SessionManager/atc/{atcId}";
            var atcExamSessions = await _apiClient.GetFromJsonAsync<AtcExamSessions>(url);
            if (atcExamSessions != null)
            {
                _stateContainer.AtcExamSessions = atcExamSessions;
            }
            else
            {
                throw new Exception($"No data found for ATC: {atcId}");
            }
        }
    }
}
