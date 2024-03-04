using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Kosar
{
    public int Id { get; set; }

    public int? FelhasznaloId { get; set; }

    public int? TermekId { get; set; }

    public int? Mennyiseg { get; set; }

    public virtual Felhasznalok? Felhasznalo { get; set; }

    public virtual Termekek? Termek { get; set; }
}
