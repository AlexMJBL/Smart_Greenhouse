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

        public virtual DbSet<PlantSensorAlert> PlantAlerts { get; set; }

        public virtual DbSet<PlantHumidityRecord> PlantHumidityRecords { get; set; }

        public virtual DbSet<Sensor> Sensors { get; set; }

        public virtual DbSet<SoilHumidityCategory> SoilHumidityCategories { get; set; }

        public virtual DbSet<Specimen> Specimens { get; set; }

        public virtual DbSet<Watering> Waterings { get; set; }

        public virtual DbSet<Zone> Zones { get; set; }

        public virtual DbSet<ZoneSensorAlert> ZoneAlerts { get; set; }

        public virtual DbSet<ZoneCategory> ZoneCategories { get; set; }

        public virtual DbSet<ZonePressureRecord> ZonePressureRecords { get; set; }

        public virtual DbSet<ZoneRecord> ZoneRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZoneCategory>().ToTable("ZoneCategories");
            modelBuilder.Entity<Plant>().ToTable("Plants");
            modelBuilder.Entity<Sensor>().ToTable("Sensors");
            modelBuilder.Entity<Fertilizer>().ToTable("Fertilizers");
            modelBuilder.Entity<Observation>().ToTable("Observations");
            modelBuilder.Entity<Specimen>().ToTable("Specimens");
            modelBuilder.Entity<Watering>().ToTable("Waterings");
            modelBuilder.Entity<Zone>().ToTable("Zones");
            modelBuilder.Entity<ZoneRecord>().ToTable("ZoneRecords");
            modelBuilder.Entity<ZoneSensorAlert>().ToTable("ZoneSensorAlerts");
            modelBuilder.Entity<ZonePressureRecord>().ToTable("ZonePressureRecords");
            modelBuilder.Entity<PlantSensorAlert>().ToTable("PlantSensorAlerts");
            modelBuilder.Entity<PlantHumidityRecord>().ToTable("PlantHumidityRecords");
            modelBuilder.Entity<SoilHumidityCategory>().ToTable("SoilHumidityCategories");
        }
    }
}
