using AutoMapper;
using SelfWebsiteApi.Models;
using SelfWebsiteApi.Services.Interfaces;

namespace SelfWebsiteApi.Services.Implementations
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public MapperProvider()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IMapper GetMapper()
        {
            if(_mapper is null)
            {
                throw new InvalidOperationException();
            }

            return _mapper;
        }
    }
}
