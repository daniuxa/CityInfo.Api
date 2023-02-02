using CityInfo.Api.Models;

namespace CityInfo.Api
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CityDataStore Current { get; } = new CityDataStore();
        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Kyiv",
                    Description = "City where I was born",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new() {Id = 1, Name = "Golden Gate"},
                        new() {Id = 2, Name = "Dnipro river"}
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Lviv",
                    Description = "One of the mos beautiful cities",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new() {Id = 3, Name = "Town hall"},
                        new() {Id = 4, Name = "Park"}
                    }
                }
            };
        }
    }
}
