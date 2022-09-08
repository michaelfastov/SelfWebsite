using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.EntityFramework
{
    public interface IEfSectionService
    {
        Task DeleteSectionsNotInResume(ResumeModel resume);
    }
}
