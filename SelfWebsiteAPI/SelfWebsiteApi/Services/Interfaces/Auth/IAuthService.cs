using SelfWebsiteApi.Models.Auth;

namespace SelfWebsiteApi.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<TokenModel> AdminLogin(LoginCredentials login);
        Task<TokenModel> Refresh(TokenModel token);
        Task Revoke(string username);
    }
}
