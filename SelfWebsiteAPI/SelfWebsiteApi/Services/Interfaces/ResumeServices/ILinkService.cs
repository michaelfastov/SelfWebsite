using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Services.Interfaces.ResumeServices
{
    public interface ILinkService
    {
        Task DeleteLinksNotInResume(ResumeModel resume);
    }
}
