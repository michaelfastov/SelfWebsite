using Microsoft.IdentityModel.Tokens;
using SelfWebsiteApi.Services.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SelfWebsiteApi.Services.Implementations.Auth
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = GetSecretKey();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Settings:ServerLink"),
                audience: _configuration.GetValue<string>("Settings:ServerLink"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    _configuration.GetValue<int>("Settings:Authorization:TokenExpireTimeMinutes")),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetClaimsFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecretKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            //?
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private SecurityKey GetSecretKey()
        {
            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration.GetValue<string>("Settings:Authorization:SymmetricSecurityKey")));
        }
    }
}
