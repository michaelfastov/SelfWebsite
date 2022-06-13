using SelfWebsiteApi.Enums.Resume;

namespace SelfWebsiteApi.Models.ResumeModels
{
    public class ResumeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<SectionModel>? Sections { get; set; }
        public List<LinkModel>? Links { get; set; }
    }
}
