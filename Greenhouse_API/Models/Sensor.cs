using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class Sensor
{
    public string Id { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;

    public bool? LastSeen { get; set; }

    public int? ZoneId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<SensorAlert> SensorAlerts { get; set; } = new List<SensorAlert>();

    public virtual Zone? Zone { get; set; }

    public virtual ICollection<ZonePressureRecord> ZonePressureRecords { get; set; } = new List<ZonePressureRecord>();

    public virtual ICollection<ZoneRecord> ZoneRecords { get; set; } = new List<ZoneRecord>();
}
