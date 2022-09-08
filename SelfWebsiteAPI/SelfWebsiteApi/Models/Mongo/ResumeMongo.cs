using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Models.Mongo
{
    public class ResumeMongo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SectionModel>? Sections { get; set; }
        public List<LinkModel>? Links { get; set; }
    }
}
