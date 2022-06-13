using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.ResumeServices
{
    public interface ISectionService
    {
        Task DeleteSectionsNotInResume(ResumeModel resume);
    }
}
