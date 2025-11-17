using System;
using System.Collections.Generic;

namespace Greenhouse_Data_MVC.Models;

public partial class ZonePressureRecord
{
    public int Id { get; set; }

    public float RecordedHPa { get; set; }

    public int ZoneId { get; set; }

    public string SensorId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;

    public virtual Zone Zone { get; set; } = null!;
}
