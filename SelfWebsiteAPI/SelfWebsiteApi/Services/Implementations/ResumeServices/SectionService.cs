using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces.ResumeServices;

namespace SelfWebsiteApi.Services.Implementations.ResumeServices
{
    public class SectionService : ISectionService
    {
        private readonly SelfWebsiteContext _context;

        public SectionService(
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
