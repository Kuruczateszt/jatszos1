using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.DtoMap
{
    public static class TermekekDtoMap
    {
        //Összes termék lekérdezése esetén
        public static TermekOsszesLekerdezDto TermekLekerdezDto(this Termekek termek)
        {
            return new TermekOsszesLekerdezDto()
            {
                Id = termek.Id,
                Nev = termek.Nev,
                Ar = termek.Ar,
                Leiras = termek.Leiras,
                Kategoria = termek.Kategoria,
                TermekKepB64 = termek.TermekKep != null && termek.TermekKep.Kep != null ? Convert.ToBase64String(termek.TermekKep.Kep) : ""
            };
        }
    }
}