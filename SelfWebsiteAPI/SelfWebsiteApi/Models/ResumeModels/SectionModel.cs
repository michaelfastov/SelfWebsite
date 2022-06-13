using SelfWebsiteApi.Enums.Resume;

namespace SelfWebsiteApi.Models.ResumeModels
{
    public class SectionModel
    {
        public int Id { get; set; }
        public SectionType SectionType { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Content { get; set; }
        public string? JobTitle { get; set; }
        public string? Company { get; set; }
        public string? Separator { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
