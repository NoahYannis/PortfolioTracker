﻿using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.AuthService;

public class AuthService : IAuthService
{
    private string serverBaseDomain = "https://localhost:7207";

    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public User PortfolioOwner { get; set; } = new();

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

    public async Task<ServiceResponse<User>> GetUserFromDbByEmail(string email)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<User>>($"{serverBaseDomain}/api/auth/user/{email}");
        return response;
    }


    public async Task<User> GetPortfolioOwner()
    {
        AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (await IsUserAuthenticated())
        {
            var userServiceResponse = await GetUserFromDbByEmail(authState.User.Claims.ElementAt(2).Value);
            PortfolioOwner = userServiceResponse?.Data ?? new();
        }

        return PortfolioOwner;
    }

}
