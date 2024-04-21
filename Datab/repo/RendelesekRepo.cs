using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wshop3.Dto;
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

        public async Task<Rendelesek> RendelesekUjAsync(RendelesUjDto rendeles)
        {
            var rendel = new Rendelesek()
            {
                FelhasznaloId = rendeles.FelhasznaloId,
            };

            using (var t = await _ws3.Database.BeginTransactionAsync())
            {
                try
                {
                    await _ws3.Rendeleseks.AddAsync(rendel);
                    //id miatt
                    await _ws3.SaveChangesAsync();
                    foreach (var termek in rendeles.Termekek)
                    {
                        var rendelTermek = new RendelesTermek()
                        {
                            RendelesId = rendel.Id,
                            TermekId = termek.Id,
                            Mennyiseg = termek.Mennyiseg
                        };
                        await _ws3.RendelesTermeks.AddAsync(rendelTermek);
                    }
                    await _ws3.SaveChangesAsync();

                    await t.CommitAsync();
                    return rendel;
                }
                catch (Exception e)
                {
                    await t.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
        }
    }
}