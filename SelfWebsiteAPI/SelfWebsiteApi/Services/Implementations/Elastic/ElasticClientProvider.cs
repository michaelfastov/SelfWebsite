using Microsoft.Extensions.Options;
using Nest;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Services.Interfaces.Elastic;

namespace SelfWebsiteApi.Services.Implementations.Elastic
{
    public class ElasticClientProvider : IElasticClientProvider
    {
        private readonly ElasticSettings _elasticSettings;

        public ElasticClientProvider(IOptions<ElasticSettings> elasticSettings)
        {
            _elasticSettings = elasticSettings.Value;
        }

        public ElasticClient? GetClient()
        {
            if (_elasticSettings.IsActive)
            {
                var settings = new ConnectionSettings(new Uri(_elasticSettings.Url))
                    .BasicAuthentication(_elasticSettings.Username, _elasticSettings.Password)
                    .PrettyJson();
                
                return new ElasticClient(settings);
            }

            return null;
        }
    }
}
