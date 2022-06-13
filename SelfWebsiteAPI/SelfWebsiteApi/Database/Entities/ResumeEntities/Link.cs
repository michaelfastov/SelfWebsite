using SelfWebsiteApi.Enums.Resume;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWebsiteApi.Database.Entities.ResumeEntities
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public LinkType? LinkType { get; set; }
        public string? LinkAddress { get; set; }
        public Resume Resume { get; set; }
    }
}
