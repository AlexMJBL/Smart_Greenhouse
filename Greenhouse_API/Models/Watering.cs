using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class Watering
{
    public int Id { get; set; }

    public float HumPctBefore { get; set; }

    public float HumPctAfter { get; set; }

    public int WaterQuantityMl { get; set; }

    public int PlantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Plant Plant { get; set; } = null!;
}
