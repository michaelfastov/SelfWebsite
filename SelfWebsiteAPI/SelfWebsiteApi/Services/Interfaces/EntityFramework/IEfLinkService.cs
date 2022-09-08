using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.EntityFramework
{
    public interface IEfLinkService
    {
        Task DeleteLinksNotInResume(ResumeModel resume);
    }
}
