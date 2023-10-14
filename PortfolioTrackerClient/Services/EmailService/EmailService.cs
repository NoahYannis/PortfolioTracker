using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _http;
        private string serverBaseDomain = "https://localhost:7207";


        public EmailService(HttpClient http)
        {
            _http = http;
        }

        public async Task SendEmail(string body, string recipientAddress)
        {
            var response = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/email?recipientAddress={recipientAddress}", body);
            //return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
