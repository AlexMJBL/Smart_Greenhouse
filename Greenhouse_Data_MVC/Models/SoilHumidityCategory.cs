using System;
using System.Collections.Generic;

namespace Greenhouse_Data_MVC.Models;

public partial class SoilHumidityCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public float? MinHumidityPct { get; set; }

    public float? MaxHumidityPct { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Specimen> Specimen { get; set; } = new List<Specimen>();
}
