using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedisCache.PixivLinksBot;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces;

namespace SelfWebsiteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PixivLinksController : ControllerBase
{
    private readonly IPixivLinksBotCacheService _pixivLinksBotCacheService;

    public PixivLinksController(IPixivLinksBotCacheService pixivLinksBotCacheService)
    {
        _pixivLinksBotCacheService = pixivLinksBotCacheService;
    }

    [HttpGet("GetCachedLinksByUsername")]
    public async Task<ActionResult<List<string>>> GetCachedLinksForUser()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok(await _pixivLinksBotCacheService.GetCachedLinksByUsername(User.Identity.Name));
        }

        return Ok();
    }

    [HttpPost("CachePixivLinkForUser"), Authorize]
    public async Task CachePixivLinkForUser([FromBody] string link)
    {
        if (User.Identity.IsAuthenticated)
        {
            await _pixivLinksBotCacheService.CacheRecentPixivLinks(User.Identity.Name, link);
        }
    }
}
