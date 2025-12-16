using System;
using System.Collections.Generic;

namespace Greenhouse_API.Models;

public partial class PlantAlert
{
    public int Id { get; set; }

    public int PlantId { get; set; }

    public string Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Plant Plant { get; set; } = null!;
}
