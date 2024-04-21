using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Rendelesek
{
    public int Id { get; set; }

    public string FelhasznaloId { get; set; } = null!;

    public DateTime? RendelesIdeje { get; set; }

    public virtual ICollection<RendelesTermek> RendelesTermeks { get; set; } = new List<RendelesTermek>();
}
