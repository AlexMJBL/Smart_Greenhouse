using Greenhouse_API.Enums;
using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class SensorAlert
{
    public int Id { get; set; }

    public AlertReason Reason { get; set; }
    public string? Message { get; set; }

    public int SensorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Sensor Sensor { get; set; } 
}
