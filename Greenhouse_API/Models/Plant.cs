using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class Plant
{
    public int Id { get; set; }

    public DateOnly AcquiredDate { get; set; }

    public int SpecimenId { get; set; }

    public int ZoneId { get; set; }

    public int? MomId { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Fertilizer> Fertilizers { get; set; } = new List<Fertilizer>();

    public virtual ICollection<Plant> InverseMom { get; set; } = new List<Plant>();

    public virtual Plant? Mom { get; set; }

    public virtual ICollection<Observation> Observations { get; set; } = new List<Observation>();

    public virtual ICollection<PlantSensorAlert> PlantAlerts { get; set; } = new List<PlantSensorAlert>();

    public virtual ICollection<PlantHumidityRecord> PlantHumidityRecords { get; set; } = new List<PlantHumidityRecord>();

    public virtual Specimen Specimen { get; set; } = null!;

    public virtual ICollection<Watering> Waterings { get; set; } = new List<Watering>();

    public virtual Zone Zone { get; set; } = null!;
}
