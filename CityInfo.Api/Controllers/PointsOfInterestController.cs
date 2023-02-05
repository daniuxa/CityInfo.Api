using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/[controller]")]
    [Authorize(Policy = "MustBeFromKyiv")]
    [ApiVersion("2.0")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;
        private readonly ICityInfoRepository cityInfoRepository;
        private readonly IMapper mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            this.cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
           /* var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            if (!await cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            {
                return Forbid();
            }*/

            if (!await cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with Id {cityId} not found");
                return NotFound();
            }

            var pointsOfInterestForCity = await cityInfoRepository.GetPointsOfInterestsForCityAsync(cityId);

            return Ok(mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }
        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            if (await cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with Id {cityId} not found");
                return NotFound();
            }

            var pointOfInterest = await cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestCreation pointOfInterest)
        {
            if (!await cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn = mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new {cityId = cityId, pointOfInterestId = createdPointOfInterestToReturn.Id}, createdPointOfInterestToReturn);
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestforUpdateDTO pointOfInterest)
        {
            if (!await cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            mapper.Map(pointOfInterest, pointOfInterestEntity);

            await cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestforUpdateDTO> patchDocument)
        {
            if (!await cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await cityInfoRepository.
                GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = mapper.Map<PointOfInterestforUpdateDTO>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await cityInfoRepository.
                GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            cityInfoRepository.DeletePointOfInterestForCityAsync(pointOfInterestEntity);

            await cityInfoRepository.SaveChangesAsync();
            _localMailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id}");
            return NoContent();
        }
    }
}
