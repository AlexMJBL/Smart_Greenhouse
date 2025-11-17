using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class Zone
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int ZoneCategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    public virtual ICollection<ZoneAlert> ZoneAlerts { get; set; } = new List<ZoneAlert>();

    public virtual ZoneCategory ZoneCategory { get; set; } = null!;

    public virtual ICollection<ZonePressureRecord> ZonePressureRecords { get; set; } = new List<ZonePressureRecord>();

    public virtual ICollection<ZoneRecord> ZoneRecords { get; set; } = new List<ZoneRecord>();
}
