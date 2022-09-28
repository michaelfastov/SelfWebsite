using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.Elastic
{
    public interface IElasticResumeService
    {
        Task<ResumeModel?> GetMainResume();
        Task<List<ResumeModel>> SearchResume(string input);
        Task CreateResume(ResumeModel resume);
        Task<ResumeModel?> UpdateResume(ResumeModel resume);
        Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume);
        Task DeleteResume(int resumeId);
    }
}
