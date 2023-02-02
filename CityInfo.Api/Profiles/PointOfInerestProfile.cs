using AutoMapper;

namespace CityInfo.Api.Profiles
{
    public class PointOfInerestProfile : Profile
    {
        public PointOfInerestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestCreation, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestforUpdateDTO, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestforUpdateDTO>();
        }
    }
}
