using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.DtoMap;
using wshop3.filt;
using wshop3.Model;

namespace wshop3.Datab.repo
{
    public class TermekekRepo : ITermekekRepo
    {
        private readonly Wshop3Context _ws3;

        public TermekekRepo(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }
        public async Task<Termekek?> TermekekIdAsync(int id)
        {
            var termek = await _ws3.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep).FirstOrDefaultAsync(t => t.Id == id);

            return termek;
        }

        public async Task<List<TermekOsszesLekerdezDto>> TermekListAsync(Szures szur)
        {
            var termekek = _ws3.Termekeks
                .Include(t => t.Kategoria)
                .Include(t => t.TermekKep)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(szur.Keres))
            {
                termekek = termekek.Where(t => t.Nev.Contains(szur.Keres) || (t.Leiras != null && t.Leiras.Contains(szur.Keres)));
            }

            if (!string.IsNullOrWhiteSpace(szur.Rendez))
            {
                //hibásan megadott érték esetén nincs rendezés
                switch (szur.Rendez.ToLower())
                {
                    case "nev":
                        termekek = szur.CsokkenoSorrend ?
                        termekek.OrderByDescending(t => t.Nev) : termekek.OrderBy(t => t.Nev);
                        break;
                    case "ar":
                        termekek = szur.CsokkenoSorrend ?
                        termekek.OrderByDescending(t => t.Ar) : termekek.OrderBy(t => t.Ar);
                        break;
                    default:
                        break;
                }
            }

            //első oldalon nincs mit kihagyni, ezért a -1
            var kihagy = (szur.Lapszam - 1) * szur.Lapmeret;
            termekek = termekek.Skip(kihagy).Take(szur.Lapmeret);

            var termekekvissza = await termekek.Select(t => t.TermekLekerdezDto()).ToListAsync();

            return termekekvissza;
        }

        public async Task<Termekek?> TermekTorlesAsync(int id)
        {
            var termek = await _ws3.Termekeks.FirstOrDefaultAsync(t => t.Id == id);

            if (termek == null)
            {
                return null;
            }

            _ws3.Termekeks.Remove(termek);
            await _ws3.SaveChangesAsync();

            return termek;
        }

        public async Task<Termekek> TermekUjAsync(Termekek termek, TermekKepek kep)
        {
            using (var t = await _ws3.Database.BeginTransactionAsync())
            {
                try
                {
                    await _ws3.TermekKepeks.AddAsync(kep);
                    await _ws3.SaveChangesAsync();
                    termek.TermekKepId = kep.Id;
                    await _ws3.Termekeks.AddAsync(termek);
                    await _ws3.SaveChangesAsync();
                    await t.CommitAsync();
                    return termek;
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
