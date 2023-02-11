using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Database.Entities.Auth;
using SelfWebsiteApi.Models.Auth;
using SelfWebsiteApi.Services.Interfaces.Auth;
using System.Security.Claims;

namespace SelfWebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials login)
        {
            if (login is null)
            {
                return BadRequest();
            }

            var tokens = await _authService.AdminLogin(login);

            if (tokens is null)
            {
                return Unauthorized();
            }

            return Ok(tokens);
        }


        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh(TokenModel token)
        {
            if (token is null)
            {
                return BadRequest();
            }

            var tokens = await _authService.Refresh(token);

            if (tokens is null)
            {
                return Unauthorized();
            }

            return Ok(tokens);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("Revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            await _authService.Revoke(username);

            return NoContent();
        }
    }
}
