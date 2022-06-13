using SelfWebsiteApi.Enums.Resume;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWebsiteApi.Database.Entities.ResumeEntities
{
    public class Resume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}
