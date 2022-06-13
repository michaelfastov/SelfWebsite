using System.Security.Claims;

namespace SelfWebsiteApi.Services.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsFromToken(string token);
    }
}
