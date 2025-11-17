using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class SensorAlert
{
    public int Id { get; set; }

    public string Reason { get; set; } = null!;

    public string SensorId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;
}
