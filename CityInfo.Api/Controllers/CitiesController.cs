using CityInfo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly CityDataStore cityDataStore;

        public CitiesController(CityDataStore cityDataStore)
        {
            this.cityDataStore = cityDataStore ?? throw new ArgumentNullException(nameof(cityDataStore)); ;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var Cities = cityDataStore.Cities;
            if (Cities == null || Cities.Count() == 0)
            {
                return NoContent();
            }
            return Ok(cityDataStore.Cities); 
        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = cityDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}
