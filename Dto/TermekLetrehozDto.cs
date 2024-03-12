using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Model;

namespace wshop3.Dto
{
    public class TermekLetrehozDto
    {
        public string Nev { get; set; }

        public decimal Ar { get; set; }

        public string? Leiras { get; set; }

        public int? KategoriaId { get; set; }

        public string Kep { get; set; }

    }
}