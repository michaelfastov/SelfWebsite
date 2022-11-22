using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace TelegramBot.PixivLinksBot
{
    public class PixivLinksBotService
    {
        private readonly IHubContext<PixivLinksHub> _pixivLinksHubContext;
        private readonly ILogger<PixivLinksBotService> _logger;

        public PixivLinksBotService(IHubContext<PixivLinksHub> pixivLinksHubContext,
            ILogger<PixivLinksBotService> logger)
        {
            _pixivLinksHubContext = pixivLinksHubContext;
            _logger = logger;
        }

        public async Task ProcessUpdate(Update update)
        {
            _logger.LogInformation($"Update received. ChannelPost: {update.ChannelPost}");
            if (update.ChannelPost != null)
            {
                var link = update.ChannelPost!.Text;
                await _pixivLinksHubContext.Clients.All.SendAsync("GetPixivLinks", link);
            }
        }
    }
}
