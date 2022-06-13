using Microsoft.EntityFrameworkCore;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Enums.Auth;
using SelfWebsiteApi.Models.Auth;
using SelfWebsiteApi.Services.Interfaces.Auth;
using System.Security.Claims;

namespace SelfWebsiteApi.Services.Implementations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SelfWebsiteContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthService(
            SelfWebsiteContext context,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<TokenModel> AdminLogin(LoginCredentials login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (u.UserName == login.UserName) &&
                (u.Password == login.Password));

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Role, ClaimRole.Admin.ToString())
            };

            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime =
                DateTime.Now.AddDays(
                    _configuration.GetValue<int>("Settings:Authorization:RefreshTokenExpireTimeDays"));

            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenModel> Refresh(TokenModel token)
        {
            var claims = _tokenService.GetClaimsFromToken(token.AccessToken);
            var username = claims.Identity.Name;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user is null ||
                user.RefreshToken != token.RefreshToken ||
                user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return null;
            }

            var newAccessToken = _tokenService.GenerateAccessToken(claims.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task Revoke(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return;
            }

            user.RefreshToken = null;
            _context.SaveChanges();
        }
    }
}
