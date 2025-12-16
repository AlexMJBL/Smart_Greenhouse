using Greenhouse_API.Enums;
using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class ZoneRecord
{
    public int Id { get; set; }

    public float Record { get; set; }

    public bool InRange { get; set; }

    public int ZoneId { get; set; }

    public string SensorId { get; set; }

    public SensorType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;

    public virtual Zone Zone { get; set; } = null!;
}
