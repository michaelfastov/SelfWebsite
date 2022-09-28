using Nest;

namespace SelfWebsiteApi.Services.Interfaces.Elastic
{
    public interface IElasticClientProvider
    {
        ElasticClient? GetClient();
    }
}
