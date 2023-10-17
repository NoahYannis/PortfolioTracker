namespace PortfolioTrackerClient.Services.AuthService
{
    /// <summary>
    /// User authentication and authorization
    /// </summary>
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<User>> GetUserFromDbByEmail(string email);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
        Task<User> GetPortfolioOwner();
    }
}
