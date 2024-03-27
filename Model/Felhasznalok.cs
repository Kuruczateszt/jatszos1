using System;
using System.Collections.Generic;

namespace wshop3.Model;

public partial class Felhasznalok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Jelszo { get; set; } = null!;

    public string Email { get; set; } = null!;
}
