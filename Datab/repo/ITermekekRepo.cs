using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
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

    }
}