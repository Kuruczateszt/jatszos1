using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.DtoMap
{
    public static class RendelesekDtoMap
    {
        public static Rendelesek RendelesekUjPost(this RendelesUjPostDto Rendeles)
        {
            return new Rendelesek()
            {
                FelhasznaloId = Rendeles.FelhasznaloId,
                TermekId = Rendeles.TermekId,
                Mennyiseg = Rendeles.Mennyiseg
            };
        }

    }
}