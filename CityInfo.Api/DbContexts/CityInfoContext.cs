using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set;} = null!;
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Kyiv") {
                    Id = 1,
                    Description = "City where I was born"
                },
                new City("Lviv")
                {
                    Id = 2,
                    Description = "One of the mos beautiful cities"
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Golden Gate")
                {
                    Id=1,
                    CityId = 1,
                    Description = "Gate"
                },
                new PointOfInterest("Dnipro river")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "River"
                },
                new PointOfInterest("Town hall")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Hall"
                },
                new PointOfInterest("Park")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "Park"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
