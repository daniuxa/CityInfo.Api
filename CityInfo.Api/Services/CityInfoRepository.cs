using CityInfo.Api.DbContexts;
using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext cityInfoContext;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            this.cityInfoContext = cityInfoContext ?? throw new ArgumentNullException(nameof(cityInfoContext));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await cityInfoContext.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, 
            string? searchQuery, int pageNumber, int pageSize)
        {

            var collection = cityInfoContext.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery!) || (a.Description != null && a.Description.Contains(searchQuery!)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.
                OrderBy(c => c.Name).
                Skip(pageSize * (pageNumber - 1)).
                Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);

           
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await cityInfoContext.Cities.Include(x => x.PointsOfInterests).FirstOrDefaultAsync(c => c.Id == cityId);
            }

            return await cityInfoContext.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
            
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await cityInfoContext.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await cityInfoContext.PointOfInterests.FirstOrDefaultAsync(p => p.Id == pointOfInterestId && p.CityId == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsForCityAsync(int cityId)
        {
            return await cityInfoContext.PointOfInterests.Where(c => c.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterests.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await cityInfoContext.SaveChangesAsync() >= 0);
        }

        public void DeletePointOfInterestForCityAsync(PointOfInterest pointOfInterest)
        {
            cityInfoContext.PointOfInterests.Remove(pointOfInterest);
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await cityInfoContext.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }
    }
}
