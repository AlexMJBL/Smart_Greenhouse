using System;
using System.Collections.Generic;

namespace Greenhouse_Data_MVC.Models;

public partial class Specimen
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SoilHumidityCatId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();

    public virtual SoilHumidityCategory SoilHumidityCat { get; set; } = null!;
}
