using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.PixivLinksBot;

namespace SelfWebsiteApi.Controllers;

public class PixivLinksBotController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices] PixivLinksBotService pixivLinksBotService,
        [FromBody] Update update)
    {
        await pixivLinksBotService.ProcessUpdate(update);

        return Ok();
    }
}
