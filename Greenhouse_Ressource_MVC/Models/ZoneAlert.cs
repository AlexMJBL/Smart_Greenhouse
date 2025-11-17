using System;
using System.Collections.Generic;

namespace Greenhouse_Ressource_MVC.Models;

public partial class ZoneAlert
{
    public int Id { get; set; }

    public int ZoneId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Zone Zone { get; set; } = null!;
}
