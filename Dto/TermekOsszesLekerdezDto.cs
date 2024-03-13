using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Model;

namespace wshop3.Dto
{
    public class TermekOsszesLekerdezDto
    {
        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public decimal Ar { get; set; }

        public string? Leiras { get; set; }

        public virtual Kategoriak? Kategoria { get; set; }

        public string TermekKepB64 { get; set; } = string.Empty;
    }
}