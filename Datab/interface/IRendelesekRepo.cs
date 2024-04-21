using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.Datab
{
    public interface IRendelesekRepo
    {
        Task<Rendelesek?> RendelesekIdAsync(int id);
        Task<List<Rendelesek>> OsszesRendelesekAsync();
        Task<Rendelesek> RendelesekUjAsync(RendelesUjDto rendeles);
    }
}