using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Termekek
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public decimal Ar { get; set; }

    public string? Leiras { get; set; }

    public int? KategoriaId { get; set; }

    public virtual Kategoriak? Kategoria { get; set; }

    public virtual ICollection<Kosar> Kosars { get; set; } = new List<Kosar>();

    public virtual ICollection<Rendelesek> Rendeleseks { get; set; } = new List<Rendelesek>();
}
