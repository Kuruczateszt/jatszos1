using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wshop3.Model;

public partial class TermekKepek
{
    public int Id { get; set; }

    public int? TermekId { get; set; }

    public byte[]? Kep { get; set; }

    [JsonIgnore]
    public virtual ICollection<Termekek> Termekeks { get; set; } = new List<Termekek>();
}
