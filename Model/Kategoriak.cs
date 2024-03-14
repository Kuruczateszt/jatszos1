using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wshop3.Model;

public partial class Kategoriak
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Termekek> Termekeks { get; set; } = new List<Termekek>();
}
