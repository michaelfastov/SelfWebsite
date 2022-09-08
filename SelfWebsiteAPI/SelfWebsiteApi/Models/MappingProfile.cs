using AutoMapper;
using SelfWebsiteApi.Database.Entities.ResumeEntities;
using SelfWebsiteApi.Models.Mongo;
using SelfWebsiteApi.Models.ResumeModels;

namespace SelfWebsiteApi.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resume, ResumeModel>();
            CreateMap<Section, SectionModel>();
            CreateMap<Link, LinkModel>();

            CreateMap<ResumeModel, Resume>();
            CreateMap<SectionModel, Section>();
            CreateMap<LinkModel, Link>();

            CreateMap<ResumeMongo, ResumeModel>();
            CreateMap<ResumeModel, ResumeMongo>();
        }
    }
}