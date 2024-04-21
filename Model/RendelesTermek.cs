using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wshop3.Model;

public partial class RendelesTermek
{
    public int RendelesId { get; set; }

    public int TermekId { get; set; }

    public int Mennyiseg { get; set; }

    [JsonIgnore]
    public virtual Rendelesek Rendeles { get; set; } = null!;

    public virtual Termekek Termek { get; set; } = null!;
}
