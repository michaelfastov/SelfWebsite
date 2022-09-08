using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.Mongo;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.Mongo;

namespace SelfWebsiteApi.Services.Implementations.Mongo
{
    public class MongoResumeService : IMongoResumeService
    {
        private readonly IMongoCollection<ResumeMongo> _resumeCollection;
        private readonly IMapper _mapper;

        public MongoResumeService(
            IMongoCollectionProvider mongoCollectionProvider,
            IOptions<MongoDbSettings> mongoDbSettings,
            IMapperProvider mapperProvider)
        {
            _resumeCollection = mongoCollectionProvider
                .GetCollection<ResumeMongo>(mongoDbSettings.Value.ResumesCollectionName);
            _mapper = mapperProvider.GetMapper();
        }

        public async Task<ResumeModel?> GetMainResume()
        {
            var resume = await _resumeCollection.Find(_ => true).FirstOrDefaultAsync();

            if (resume != null)
            {
                return _mapper.Map<ResumeMongo, ResumeModel>(resume);
            }

            return null;
        }

        public async Task<ResumeModel> GetResume(int id)
        {
            var resume = await _resumeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (resume != null)
            {
                return _mapper.Map<ResumeMongo, ResumeModel>(resume);
            }

            return null;
        }

        public async Task<ResumeModel> CreateResume(ResumeModel resume)
        {
            var resumeMongo = _mapper.Map<ResumeModel, ResumeMongo>(resume);
            resumeMongo.CreatedAt = DateTime.Now;
            await _resumeCollection.InsertOneAsync(resumeMongo);

            return _mapper.Map<ResumeMongo, ResumeModel>(await GetResumeWithNewestDate());
        }

        public async Task<ResumeModel> CreateOrUpdateResume(ResumeModel resume)
        {
            return resume.Id == 0 ? await CreateResume(resume) : await UpdateResume(resume);
        }

        public async Task<ResumeModel> UpdateResume(ResumeModel resume)
        {
            var resumeMongo = _mapper.Map<ResumeModel, ResumeMongo>(resume);

            await _resumeCollection.ReplaceOneAsync(x => x.Id == resumeMongo.Id, resumeMongo);
            return _mapper.Map<ResumeMongo, ResumeModel>(await GetResumeWithNewestDate());
        }

        public async Task DeleteResume(int resumeId)
        {
            await _resumeCollection.DeleteOneAsync(x => x.Id == resumeId);
        }

        private async Task<ResumeMongo> GetResumeWithNewestDate()
        {
            var sortDef = Builders<ResumeMongo>.Sort.Descending(a => a.CreatedAt);
            return await _resumeCollection.Find(_ => true).Sort(sortDef).FirstOrDefaultAsync();
        }
    }
}
