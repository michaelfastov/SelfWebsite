using Microsoft.AspNetCore.SignalR;
using Telegram.Bot.Types;

namespace TelegramBot.PixivLinksBot
{
    public class PixivLinksBotService
    {
        private readonly IHubContext<PixivLinksHub> _pixivLinksHubContext;

        public PixivLinksBotService(IHubContext<PixivLinksHub> pixivLinksHubContext)
        {
            _pixivLinksHubContext = pixivLinksHubContext;
        }

        public async Task ProcessUpdate(Update update)
        {
            if (update.ChannelPost != null)
            {
                var link = update.ChannelPost!.Text;
                await _pixivLinksHubContext.Clients.All.SendAsync("GetPixivLinks", link);
            }
        }
    }
}
