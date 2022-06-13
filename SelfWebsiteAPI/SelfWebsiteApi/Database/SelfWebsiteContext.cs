using Microsoft.EntityFrameworkCore;
using SelfWebsiteApi.Database.Entities.Auth;
using SelfWebsiteApi.Database.Entities.ResumeEntities;

namespace SelfWebsiteApi.Database
{
    public class SelfWebsiteContext : DbContext
    {
        public SelfWebsiteContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
