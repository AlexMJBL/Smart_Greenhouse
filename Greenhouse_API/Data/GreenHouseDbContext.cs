using Greenhouse_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse_API.Data
{
    public class GreenHouseDbContext : DbContext
    {
        public GreenHouseDbContext(DbContextOptions<GreenHouseDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Fertilizer> Fertilizers { get; set; }

        public virtual DbSet<Observation> Observations { get; set; }

        public virtual DbSet<Plant> Plants { get; set; }

        public virtual DbSet<PlantAlert> PlantAlerts { get; set; }

        public virtual DbSet<PlantHumidityRecord> PlantHumidityRecords { get; set; }

        public virtual DbSet<Sensor> Sensors { get; set; }

        public virtual DbSet<SensorAlert> SensorAlerts { get; set; }

        public virtual DbSet<SoilHumidityCategory> SoilHumidityCategories { get; set; }

        public virtual DbSet<Specimen> Specimens { get; set; }

        public virtual DbSet<Watering> Waterings { get; set; }

        public virtual DbSet<Zone> Zones { get; set; }

        public virtual DbSet<ZoneAlert> ZoneAlerts { get; set; }

        public virtual DbSet<ZoneCategory> ZoneCategories { get; set; }

        public virtual DbSet<ZonePressureRecord> ZonePressureRecords { get; set; }

        public virtual DbSet<ZoneRecord> ZoneRecords { get; set; }
    }
}
