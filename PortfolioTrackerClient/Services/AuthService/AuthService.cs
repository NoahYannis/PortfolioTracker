using Microsoft.AspNetCore.Components.Authorization;
using PortfolioTrackerShared.Models;
using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;
using System.Net.Http;
using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private string serverBaseDomain = "https://localhost:7207";

        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<User>> GetUserFromDb(string email)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<User>>($"{serverBaseDomain}/api/auth/user");
            return response;
        }
    }
}
