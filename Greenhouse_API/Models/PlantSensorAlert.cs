using Greenhouse_API.Enums;
using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class PlantSensorAlert
{
    public int Id { get; set; }

    public int PlantId { get; set; }

    public AlertReason Reason { get; set; }
    public SensorType SensorType { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Plant Plant { get; set; } = null!;
}
