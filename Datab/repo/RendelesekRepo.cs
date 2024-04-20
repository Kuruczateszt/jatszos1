using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wshop3.Model;

namespace wshop3.Datab.repo
{
    public class RendelesekRepo : IRendelesekRepo
    {
        private readonly Wshop3Context _ws3;
        public RendelesekRepo(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }
        public async Task<Rendelesek?> RendelesekIdAsync(int id)
        {
            var rendeles = await _ws3.Rendeleseks.Where(r => r.Id == id).FirstOrDefaultAsync();

            return rendeles;
        }

        public async Task<List<Rendelesek>> OsszesRendelesekAsync()
        {
            var rendelesek = await _ws3.Rendeleseks.ToListAsync();

            return rendelesek;
        }

        public async Task<Rendelesek> RendelesekUjAsync(Rendelesek rendeles)
        {
            try
            {
                await _ws3.Rendeleseks.AddAsync(rendeles);
                await _ws3.SaveChangesAsync();
                return rendeles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}