using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class ZoneCategory
{
    public int Id { get; set; }

    public string Name { get; set; }

    public float HumidityMinPct { get; set; }

    public float HumidityMaxPct { get; set; }

    public float LuminosityMinLux { get; set; }

    public float LuminosityMaxLux { get; set; }

    public float TemperatureMinC { get; set; }

    public float TemperatureMaxC { get; set; }

    public float? PressureMinPa { get; set; }
    public float? PressureMaxPa { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Zone> Zones { get; set; } = new List<Zone>();
}
