using System;
using System.Collections.Generic;

namespace Greenhouse_Ressource_MVC.Models;

public partial class Fertilizer
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int? PlantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Plant? Plant { get; set; }
}
