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
using Microsoft.Extensions.Logging;

namespace TelegramBot.PixivLinksBot
{
    public class PixivLinksBotWebhookService : IHostedService
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PixivLinksBotWebhookService> _logger;

        public PixivLinksBotWebhookService(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ILogger<PixivLinksBotWebhookService> logger)
        {
            _services = serviceProvider;
            _configuration = configuration;
            _logger = logger;
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

            _logger.LogInformation($"Webhook set. Url: {webhookAddress}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
