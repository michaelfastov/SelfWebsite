using SelfWebsiteApi.Database;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces.EntityFramework;

namespace SelfWebsiteApi.Services.Implementations.EntityFramework
{
    public class EfLinkService : IEfLinkService
    {
        private readonly SelfWebsiteContext _context;

        public EfLinkService(
            SelfWebsiteContext context)
        {
            _context = context;
        }

        public async Task DeleteLinksNotInResume(ResumeModel resume)
        {
            if (resume != null)
            {
                if (resume.Links == null || !resume.Links.Any())
                {
                    _context.RemoveRange(_context.Links);
                }
                else
                {
                    _context.RemoveRange(_context.Links
                        .Where(x => !resume.Links.Select(s => s.Id)
                        .Contains(x.Id)));
                }

                _context.SaveChanges();
            }
        }
    }
}
