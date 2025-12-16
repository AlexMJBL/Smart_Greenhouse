using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class ZonePressureRecord
{
    public int Id { get; set; }

    public float RecordedHPa { get; set; }

    public int ZoneId { get; set; }

    public int SensorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;

    public virtual Zone Zone { get; set; } = null!;
}
