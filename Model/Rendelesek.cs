using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Rendelesek
{
    public int Id { get; set; }

    public string FelhasznaloId { get; set; } = null!;

    public int? TermekId { get; set; }

    public int? Mennyiseg { get; set; }

    public DateTime? RendelesIdeje { get; set; }

    public virtual Termekek? Termek { get; set; }

    public virtual ICollection<Termekek> Termeks { get; set; } = new List<Termekek>();
}
