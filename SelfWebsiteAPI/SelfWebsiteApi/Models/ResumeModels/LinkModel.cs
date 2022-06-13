using SelfWebsiteApi.Enums.Resume;

namespace SelfWebsiteApi.Models.ResumeModels
{
    public class LinkModel
    {
        public int Id { get; set; }
        public LinkType? LinkType { get; set; }
        public string? LinkAddress { get; set; }
    }
}
