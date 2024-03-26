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

    public virtual ICollection<Rendelesek> Rendeleseks { get; set; } = new List<Rendelesek>();

    public virtual TermekKepek? TermekKep { get; set; }

    public virtual ICollection<Rendelesek> Rendeles { get; set; } = new List<Rendelesek>();
}
