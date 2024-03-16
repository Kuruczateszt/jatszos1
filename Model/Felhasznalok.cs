using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wshop3.Model;

public partial class Felhasznalok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Jelszo { get; set; } = null!;

    public string Email { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Kosar> Kosars { get; set; } = new List<Kosar>();

    public virtual ICollection<Rendelesek> Rendeleseks { get; set; } = new List<Rendelesek>();
}
