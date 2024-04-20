using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Dto;
using wshop3.filt;
using wshop3.Model;

namespace wshop3.Datab
{
    public interface ITermekekRepo
    {
        Task<Termekek?> TermekekIdAsync(int id);
        Task<List<TermekOsszesLekerdezDto>> TermekListAsync(Szures szur);
        Task<Termekek?> TermekTorlesAsync(int id);
        Task<Termekek> TermekUjAsync(Termekek termek, TermekKepek kep);
    }
}