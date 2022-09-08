using SelfWebsiteApi.Models.Mongo;
using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.Mongo
{
    public interface IMongoResumeService
    {
        Task<ResumeModel?> GetMainResume();
        Task<ResumeModel?> CreateResume(ResumeModel resume);
        Task<ResumeModel?> UpdateResume(ResumeModel resume);
        Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume);
        Task DeleteResume(int resumeId);
    }
}
