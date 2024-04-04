using System.Net.Http.Json;

namespace PortfolioTrackerClient.Services.AuthService;

public class AuthService(HttpClient http, AuthenticationStateProvider authenticationStateProvider) : IAuthService
{
    private readonly string serverBaseDomain = "https://localhost:7207";

    private readonly HttpClient _http = http;
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    public User PortfolioOwner { get; set; } = new();


    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
        var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new();
    }

    public async Task<ServiceResponse<string>> Login(UserLogin request)
    {
        var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>() ?? new();
    }

    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
    {
        var result = await _http.PostAsJsonAsync($"{serverBaseDomain}/api/auth/change-password", request.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>() ?? new();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState?.User?.Identity?.IsAuthenticated ?? false;
    }


    public async Task<ServiceResponse<User>> GetUserFromDbByEmail(string email)
    {
        var response = await _http.GetFromJsonAsync<ServiceResponse<User>>($"{serverBaseDomain}/api/auth/user/{email}") ?? new();
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
