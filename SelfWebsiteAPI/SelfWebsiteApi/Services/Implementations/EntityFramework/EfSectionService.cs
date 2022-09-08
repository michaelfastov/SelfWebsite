using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces.EntityFramework;

namespace SelfWebsiteApi.Services.Implementations.EntityFramework
{
    public class EfSectionService : IEfSectionService
    {
        private readonly SelfWebsiteContext _context;

        public EfSectionService(
            SelfWebsiteContext context)
        {
            _context = context;
        }

        public async Task DeleteSectionsNotInResume(ResumeModel resume)
        {
            if (resume != null)
            {
                if (resume.Sections == null || !resume.Sections.Any())
                {
                    _context.RemoveRange(_context.Sections);
                }
                else
                {
                    _context.RemoveRange(_context.Sections
                        .Where(x => !resume.Sections.Select(s => s.Id)
                        .Contains(x.Id)));
                }

                _context.SaveChanges();
            }
        }
    }
}
