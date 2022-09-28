using Microsoft.Extensions.Options;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces.Elastic;
using System;
using System.Net;

namespace SelfWebsiteApi.Services.Implementations.Elastic
{
    public class ElasticResumeService : IElasticResumeService
    {
        private const string ResumeIndexName = "resume";
        private readonly ElasticSettings _elasticSettings;
        private readonly IElasticClientProvider _elasticClientProvider;

        public ElasticResumeService(
            IOptions<ElasticSettings> elasticSettings,
            IElasticClientProvider elasticClientProvider)
        {
            _elasticSettings = elasticSettings.Value;
            _elasticClientProvider = elasticClientProvider;
        }

        public Task<ResumeModel?> GetMainResume()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResumeModel>> SearchResume(string input)
        {
            var client = _elasticClientProvider.GetClient();

            if (client != null)
            {
                var searchResponse = client.Search<ResumeModel>(s => s
                    .Index(ResumeIndexName)
                    .Query(q => q
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field(f1 => f1.Name)
                                .Field(f2 => f2.Title)
                                .Field(f2 => f2.Description))
                    .Query(input))));

                return searchResponse.Documents.ToList();
            }

            return new List<ResumeModel>();
        }

        public Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume)
        {
            throw new NotImplementedException();
        }

        public async Task CreateResume(ResumeModel resume)
        {
            var client = _elasticClientProvider.GetClient();

            if (client != null)
            {
                var indexExists = client.Indices.Exists(ResumeIndexName);
                if (!indexExists.Exists)
                {
                    client.Indices.Create(ResumeIndexName, index => index.Map<ResumeModel>(x => x.AutoMap()));
                }

                await client.IndexAsync(resume, s => s.Index(ResumeIndexName));
            }
        }

        public Task DeleteResume(int resumeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResumeModel?> UpdateResume(ResumeModel resume)
        {
            throw new NotImplementedException();
        }
    }
}
