using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace TelegramBot.PixivLinksBot
{
    public class PixivLinksBotWebhookService : IHostedService
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;

        public PixivLinksBotWebhookService(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _services = serviceProvider;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            //substitute with ngrok command for local debug: ngrok http 8443 --host-header="localhost:8443"
            var serverLink = _configuration.GetSection("Settings")["ServerLink"];
            var botToken = _configuration.GetSection("TelegramBots")["PixivLinksBotToken"];
            var webhookAddress = @$"{serverLink}/{botToken}";

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
