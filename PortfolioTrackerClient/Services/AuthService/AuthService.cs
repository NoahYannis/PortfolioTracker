using PortfolioTrackerShared.Models.UserModels;
using PortfolioTrackerShared.Other;

namespace PortfolioTrackerClient.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> Login(UserLogin request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<int>> Register(UserRegister request)
        {
            throw new NotImplementedException();
        }

    }
}
