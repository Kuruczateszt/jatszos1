using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wshop3.Model;

public partial class Termekek
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public decimal Ar { get; set; }

    public string? Leiras { get; set; }

    public int? KategoriaId { get; set; }

    public int? TermekKepId { get; set; }

    [JsonIgnore]
    public virtual Kategoriak? Kategoria { get; set; }

    public virtual ICollection<RendelesTermek> RendelesTermeks { get; set; } = new List<RendelesTermek>();

    public virtual TermekKepek? TermekKep { get; set; }
}
