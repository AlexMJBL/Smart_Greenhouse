using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Greenhouse_Data_MVC.Models;

public partial class SerreContext : DbContext
{
    public SerreContext()
    {
    }

    public SerreContext(DbContextOptions<SerreContext> options)
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.8.3-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Fertilizer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fertilizers");

            entity.HasIndex(e => e.PlantId, "fk_fertilizer_plant");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PlantId)
                .HasColumnType("int(11)")
                .HasColumnName("plant_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Plant).WithMany(p => p.Fertilizers)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_fertilizer_plant");
        });

        modelBuilder.Entity<Observation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("observations");

            entity.HasIndex(e => e.PlantId, "fk_observation_plant");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PlantId)
                .HasColumnType("int(11)")
                .HasColumnName("plant_id");
            entity.Property(e => e.Rating)
                .HasMaxLength(20)
                .HasColumnName("rating");

            entity.HasOne(d => d.Plant).WithMany(p => p.Observations)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_observation_plant");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("plants");

            entity.HasIndex(e => e.MomId, "fk_plant_mom");

            entity.HasIndex(e => e.SpecimenId, "fk_plant_specimen");

            entity.HasIndex(e => e.ZoneId, "fk_plant_zone");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AcquiredDate).HasColumnName("acquired_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.MomId)
                .HasColumnType("int(11)")
                .HasColumnName("mom_id");
            entity.Property(e => e.SpecimenId)
                .HasColumnType("int(11)")
                .HasColumnName("specimen_id");
            entity.Property(e => e.ZoneId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_id");

            entity.HasOne(d => d.Mom).WithMany(p => p.InverseMom)
                .HasForeignKey(d => d.MomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_plant_mom");

            entity.HasOne(d => d.Specimen).WithMany(p => p.Plants)
                .HasForeignKey(d => d.SpecimenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plant_specimen");

            entity.HasOne(d => d.Zone).WithMany(p => p.Plants)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plant_zone");
        });

        modelBuilder.Entity<PlantAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("plant_alerts");

            entity.HasIndex(e => e.PlantId, "fk_alert_plant_plant");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PlantId)
                .HasColumnType("int(11)")
                .HasColumnName("plant_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");

            entity.HasOne(d => d.Plant).WithMany(p => p.PlantAlerts)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_alert_plant_plant");
        });

        modelBuilder.Entity<PlantHumidityRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("plant_humidity_records");

            entity.HasIndex(e => e.PlantId, "fk_plante_humidite_record_plant");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.InRange).HasColumnName("in_range");
            entity.Property(e => e.PlantId)
                .HasColumnType("int(11)")
                .HasColumnName("plant_id");
            entity.Property(e => e.RecordPct).HasColumnName("record_pct");

            entity.HasOne(d => d.Plant).WithMany(p => p.PlantHumidityRecords)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plante_humidite_record_plant");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sensors");

            entity.HasIndex(e => e.ZoneId, "fk_sensor_zone");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastSeen).HasColumnName("last_seen");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.ZoneId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_id");

            entity.HasOne(d => d.Zone).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_sensor_zone");
        });

        modelBuilder.Entity<SensorAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sensor_alerts");

            entity.HasIndex(e => e.SensorId, "fk_sensor_alert_sensor");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.SensorId)
                .HasMaxLength(50)
                .HasColumnName("sensor_id");

            entity.HasOne(d => d.Sensor).WithMany(p => p.SensorAlerts)
                .HasForeignKey(d => d.SensorId)
                .HasConstraintName("fk_sensor_alert_sensor");
        });

        modelBuilder.Entity<SoilHumidityCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("soil_humidity_categories");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MaxHumidityPct).HasColumnName("max_humidity_pct");
            entity.Property(e => e.MinHumidityPct).HasColumnName("min_humidity_pct");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Specimen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("specimens");

            entity.HasIndex(e => e.SoilHumidityCatId, "fk_specimen_humidity");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SoilHumidityCatId)
                .HasColumnType("int(11)")
                .HasColumnName("soil_humidity_cat_id");

            entity.HasOne(d => d.SoilHumidityCat).WithMany(p => p.Specimen)
                .HasForeignKey(d => d.SoilHumidityCatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_specimen_humidity");
        });

        modelBuilder.Entity<Watering>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("waterings");

            entity.HasIndex(e => e.PlantId, "fk_watering_specimen");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.HumPctAfter).HasColumnName("hum_pct_after");
            entity.Property(e => e.HumPctBefore).HasColumnName("hum_pct_before");
            entity.Property(e => e.PlantId)
                .HasColumnType("int(11)")
                .HasColumnName("plant_id");
            entity.Property(e => e.WaterQuantityMl)
                .HasColumnType("int(11)")
                .HasColumnName("water_quantity_ml");

            entity.HasOne(d => d.Plant).WithMany(p => p.Waterings)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("fk_watering_specimen");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zones");

            entity.HasIndex(e => e.ZoneCategoryId, "fk_zone_zone_category");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ZoneCategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_category_id");

            entity.HasOne(d => d.ZoneCategory).WithMany(p => p.Zones)
                .HasForeignKey(d => d.ZoneCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zone_zone_category");
        });

        modelBuilder.Entity<ZoneAlert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zone_alerts");

            entity.HasIndex(e => e.ZoneId, "fk_zone_alert_zone");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.ZoneId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_id");

            entity.HasOne(d => d.Zone).WithMany(p => p.ZoneAlerts)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zone_alert_zone");
        });

        modelBuilder.Entity<ZoneCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zone_categories");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.HumidityMaxPct).HasColumnName("humidity_max_pct");
            entity.Property(e => e.HumidityMinPct).HasColumnName("humidity_min_pct");
            entity.Property(e => e.LuminosityMaxLux).HasColumnName("luminosity_max_lux");
            entity.Property(e => e.LuminosityMinLux).HasColumnName("luminosity_min_lux");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TemperatureMaxC).HasColumnName("temperature_max_c");
            entity.Property(e => e.TemperatureMinC).HasColumnName("temperature_min_c");
        });

        modelBuilder.Entity<ZonePressureRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zone_pressure_records");

            entity.HasIndex(e => e.SensorId, "fk_zone_pressure_sensor");

            entity.HasIndex(e => e.ZoneId, "fk_zone_pressure_zone");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.RecordedHPa).HasColumnName("recorded_hPa");
            entity.Property(e => e.SensorId)
                .HasMaxLength(50)
                .HasColumnName("sensor_id");
            entity.Property(e => e.ZoneId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_id");

            entity.HasOne(d => d.Sensor).WithMany(p => p.ZonePressureRecords)
                .HasForeignKey(d => d.SensorId)
                .HasConstraintName("fk_zone_pressure_sensor");

            entity.HasOne(d => d.Zone).WithMany(p => p.ZonePressureRecords)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fk_zone_pressure_zone");
        });

        modelBuilder.Entity<ZoneRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zone_records");

            entity.HasIndex(e => e.SensorId, "fk_zone_record_sensor");

            entity.HasIndex(e => e.ZoneId, "fk_zone_record_zone");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.InRange).HasColumnName("in_range");
            entity.Property(e => e.Record).HasColumnName("record");
            entity.Property(e => e.SensorId)
                .HasMaxLength(50)
                .HasColumnName("sensor_id");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .HasColumnName("type");
            entity.Property(e => e.ZoneId)
                .HasColumnType("int(11)")
                .HasColumnName("zone_id");

            entity.HasOne(d => d.Sensor).WithMany(p => p.ZoneRecords)
                .HasForeignKey(d => d.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zone_record_sensor");

            entity.HasOne(d => d.Zone).WithMany(p => p.ZoneRecords)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zone_record_zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
