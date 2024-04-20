using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wshop3.Model;

namespace wshop3.Datab.repo
{
    public class KategoriakRepo : IKategoriakRepo
    {
        private readonly Wshop3Context _ws3;
        public KategoriakRepo(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }
        public async Task<Kategoriak?> TermekekIdAsync(int id)
        {
            var kategoria = await _ws3.Kategoriaks.FirstOrDefaultAsync(k => k.Id == id);

            return kategoria;
        }

        public async Task<List<Kategoriak>> OsszesKategoriaAsync()
        {
            var kategoriak = await _ws3.Kategoriaks.ToListAsync();

            return kategoriak;
        }
    }
}