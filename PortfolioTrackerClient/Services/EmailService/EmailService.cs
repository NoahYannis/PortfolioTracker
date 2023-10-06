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

        public async Task<bool> SendEmail(string body)
        {
            var response = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/email", body);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> SendPasswordResetEmail(string userEmail)
        {
            var response = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/email/reset-password", userEmail);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
