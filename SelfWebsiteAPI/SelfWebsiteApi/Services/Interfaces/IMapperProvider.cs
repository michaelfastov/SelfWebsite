using AutoMapper;

namespace SelfWebsiteApi.Services.Interfaces
{
    public interface IMapperProvider
    {
        IMapper GetMapper();
    }
}
