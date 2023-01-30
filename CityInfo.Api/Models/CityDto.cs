namespace CityInfo.Api.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public int NumberOfPointsOfInterests 
        { 
            get
            {
                return PointOfInterests.Count;
            }
                
        }
        public ICollection<PointOfInterestDto> PointOfInterests { get; set; } = new List<PointOfInterestDto>();
    }
}
