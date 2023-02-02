using AutoMapper;

namespace CityInfo.Api.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInerestDto>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
