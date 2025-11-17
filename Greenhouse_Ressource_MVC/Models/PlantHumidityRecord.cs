using System;
using System.Collections.Generic;

namespace Greenhouse_Ressource_MVC.Models;

public partial class PlantHumidityRecord
{
    public int Id { get; set; }

    public float RecordPct { get; set; }

    public bool InRange { get; set; }

    public int PlantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Plant Plant { get; set; } = null!;
}
