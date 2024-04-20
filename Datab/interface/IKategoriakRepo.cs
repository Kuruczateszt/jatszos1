using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Model;

namespace wshop3.Datab
{
    public interface IKategoriakRepo
    {
        Task<Kategoriak?> TermekekIdAsync(int id);
        Task<List<Kategoriak>> OsszesKategoriaAsync();
        Task<Kategoriak> KategoriaUjAsync(Kategoriak kategoria);
    }
}