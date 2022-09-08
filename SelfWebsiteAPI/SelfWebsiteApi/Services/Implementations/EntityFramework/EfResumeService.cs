using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Database.Entities.ResumeEntities;
using SelfWebsiteApi.Models;
using SelfWebsiteApi.Models.ResumeModels;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.EntityFramework;

namespace SelfWebsiteApi.Services.Implementations.EntityFramework
{
    public class EfResumeService : IEfResumeService
    {
        private readonly SelfWebsiteContext _context;
        private readonly IMapper _mapper;

        private readonly IEfSectionService _sectionService;
        private readonly IEfLinkService _linkService;
        public EfResumeService(
            SelfWebsiteContext context,
            IMapperProvider mapperProvider,
            IEfLinkService linkService,
            IEfSectionService sectionService)
        {
            _context = context;
            _mapper = mapperProvider.GetMapper();

            _linkService = linkService;
            _sectionService = sectionService;
        }

        public async Task<ResumeModel?> GetMainResume()//add ability to select main resume on UI
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

        public async Task<ResumeModel?> CreateResume(ResumeModel resume)
        {
            var resumeEntity = _mapper.Map<ResumeModel, Resume>(resume);
            var entity = _context.Add(resumeEntity).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<Resume, ResumeModel>(entity);
        }

        public async Task<ResumeModel?> UpdateResume(ResumeModel resume)
        {
            await _sectionService.DeleteSectionsNotInResume(resume);
            await _linkService.DeleteLinksNotInResume(resume);

            var resumeEntity = _mapper.Map<ResumeModel, Resume>(resume);
            var entity = _context.Update(resumeEntity).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<Resume, ResumeModel>(entity);
        }

        public async Task<ResumeModel?> CreateOrUpdateResume(ResumeModel resume)
        {
            return resume.Id == 0 ? await CreateResume(resume) : await UpdateResume(resume);
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