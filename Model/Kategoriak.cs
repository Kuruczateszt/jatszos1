using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Kategoriak
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public virtual ICollection<Termekek> Termekeks { get; set; } = new List<Termekek>();
}
