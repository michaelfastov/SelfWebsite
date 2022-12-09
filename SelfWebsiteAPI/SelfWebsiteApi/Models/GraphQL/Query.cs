using SelfWebsiteApi.Database;
using SelfWebsiteApi.Database.Entities.ResumeEntities;

namespace SelfWebsiteApi.Models.GraphQL;

public class Query
{
    private readonly SelfWebsiteContext _context;

    public Query(SelfWebsiteContext context)
    {
        _context = context;
    }

    public IQueryable<Resume> Resumes => _context.Resumes;
}
