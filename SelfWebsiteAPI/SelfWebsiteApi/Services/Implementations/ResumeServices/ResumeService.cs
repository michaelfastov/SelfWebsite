using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Database.Entities.ResumeEntities;
using SelfWebsiteApi.Models;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.ResumeServices;

namespace SelfWebsiteApi.Services.Implementations.ResumeServices
{
    public class ResumeService : IResumeService
    {
        private readonly SelfWebsiteContext _context;
        private readonly IMapper _mapper;

        private readonly ISectionService _sectionService;
        private readonly ILinkService _linkService;
        public ResumeService(
            SelfWebsiteContext context,
            IMapperProvider mapperProvider,
            ILinkService linkService,
            ISectionService sectionService)
        {
            _context = context;
            _mapper = mapperProvider.GetMapper();

            _linkService = linkService;
            _sectionService = sectionService;
        }

        public async Task<ResumeModel> GetMainResume()//add ability to select main resume on UI
        {
            var entity = await _context.Resumes
                .Include(x => x.Sections)
                .Include(x => x.Links)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                return _mapper.Map<Resume, ResumeModel>(entity);
            }

            return null;
        }

        public async Task<ResumeModel> CreateResume(ResumeModel resume)
        {
            var resumeEntity = _mapper.Map<ResumeModel, Resume>(resume);
            var entity = _context.Add(resumeEntity).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<Resume, ResumeModel>(entity);
        }

        public async Task<ResumeModel> UpdateResume(ResumeModel resume)
        {
            await _sectionService.DeleteSectionsNotInResume(resume);
            await _linkService.DeleteLinksNotInResume(resume);

            var resumeEntity = _mapper.Map<ResumeModel, Resume>(resume);
            var entity = _context.Update(resumeEntity).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<Resume, ResumeModel>(entity);
        }

        public async Task<ResumeModel> CreateOrUpdateResume(ResumeModel resume)
        {
            ResumeModel result;
            if (resume.Id == 0)
            {
                result = await CreateResume(resume);
            }
            else
            {
                result = await UpdateResume(resume);
            }

            return result;
        }

        public async Task DeleteResume(int resumeId)
        {
            var resume = _context.Resumes.FirstOrDefault(x => x.Id == resumeId);

            if (resume != null)
            {
                _context.Remove(resume);
                await _context.SaveChangesAsync();
            }
        }
    }
}