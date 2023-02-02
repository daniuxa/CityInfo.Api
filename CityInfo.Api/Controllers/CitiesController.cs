using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository cityInfoRepository;
        private readonly IMapper mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            this.cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInerestDto>>> GetCities()
        {
            var Cities = await cityInfoRepository.GetCitiesAsync();
            if (Cities == null || Cities.Count() == 0)
            {
                return NoContent();
            }
           
            return Ok(mapper.Map<IEnumerable<CityWithoutPointsOfInerestDto>>(Cities)); 
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            
            if (includePointsOfInterest)
            {
                return Ok(mapper.Map<CityDto>(city));
            }

            return Ok(mapper.Map<CityWithoutPointsOfInerestDto>(city));
        }
    }
}
