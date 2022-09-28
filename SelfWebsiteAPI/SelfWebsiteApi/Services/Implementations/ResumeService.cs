using Microsoft.Extensions.Options;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.Mongo;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.Elastic;
using SelfWebsiteApi.Services.Interfaces.EntityFramework;
using SelfWebsiteApi.Services.Interfaces.Mongo;

namespace SelfWebsiteApi.Services.Implementations
{
    public class ResumeService : IResumeService
    {
        private readonly IEfResumeService _efResumeService;
        private readonly IMongoResumeService _mongoResumeService;
        private readonly IElasticResumeService _elasticResumeService;
        private readonly EntityFrameworkSettings _entityFrameworkSettings;
        private readonly MongoDbSettings _mongoDbSettings;
        private readonly ElasticSettings _elasticSettings;

        public ResumeService(
            IEfResumeService efResumeService,
            IMongoResumeService mongoResumeService,
            IElasticResumeService elasticResumeService,
            IOptions<EntityFrameworkSettings> entityFrameworkSettings,
            IOptions<MongoDbSettings> mongoDbSettings,
            IOptions<ElasticSettings> elasticSettings)
        {
            _efResumeService = efResumeService;
            _mongoResumeService = mongoResumeService;
            _elasticResumeService = elasticResumeService;
            _entityFrameworkSettings = entityFrameworkSettings.Value;
            _mongoDbSettings = mongoDbSettings.Value;
            _elasticSettings = elasticSettings.Value;
        }

        public async Task<List<ResumeModel>> SearchResume(string input)
        {
            if (_elasticSettings.IsActive)
            {
                return await _elasticResumeService.SearchResume(input);
            }

            return new List<ResumeModel>();
        }

        public async Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume)
        {
            ResumeModel? model = null;

            if (_mongoDbSettings.IsActive)
            {
                model = await _mongoResumeService.CreateOrUpdateResume(resume);
            }

            if (_entityFrameworkSettings.IsActive)
            {
                model = await _efResumeService.CreateOrUpdateResume(resume);
            }

            return model;
        }

        public async Task<ResumeModel?> CreateResume(ResumeModel resume)
        {
            ResumeModel? model = null;

            if (_mongoDbSettings.IsActive)
            {
                model = await _mongoResumeService.CreateResume(resume);
            }

            if (_entityFrameworkSettings.IsActive)
            {
                model = await _efResumeService.CreateResume(resume);

                //Elastic works with EF only because ids in EF and Mongo are different
                if (_entityFrameworkSettings.IsActive && model != null)
                {
                    await _elasticResumeService.CreateResume(model);
                }
            }

            return model;
        }

        public async Task<ResumeModel?> GetMainResume()
        {
            ResumeModel? model = null;

            if (_mongoDbSettings.IsActive)
            {
                model = await _mongoResumeService.GetMainResume();
            }

            if (_entityFrameworkSettings.IsActive)
            {
                model = await _efResumeService.GetMainResume();
            }

            return model;
        }

        public async Task<ResumeModel?> UpdateResume(ResumeModel resume)
        {
            ResumeModel? model = null;

            if (_mongoDbSettings.IsActive)
            {
                model = await _mongoResumeService.UpdateResume(resume);
            }

            if (_entityFrameworkSettings.IsActive)
            {
                model = await _efResumeService.UpdateResume(resume);
            }

            return model;
        }

        public async Task DeleteResume(int resumeId)
        {
            if (_mongoDbSettings.IsActive)
            {
                await _mongoResumeService.DeleteResume(resumeId);
            }

            if (_entityFrameworkSettings.IsActive)
            {
                await _efResumeService.DeleteResume(resumeId);
            }
        }
    }
}
