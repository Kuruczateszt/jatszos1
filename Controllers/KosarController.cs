using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KosarController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public KosarController(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }

        [HttpGet("KosarId/{id}")]
        public IActionResult KosarId([FromRoute] int id)
        {
            var kosar = _ws3.Kosars
            .Include(k => k.Termek)
            .Include(f => f.Felhasznalo)
            .FirstOrDefault(k => k.Id == id);

            if (kosar == null)
            {
                return BadRequest("Nincs ilyen kosár");
            }
            return Ok(kosar);
        }

        //csak adminoknak
        [HttpGet("OsszesKosar")]
        public IActionResult OsszesKosar()
        {
            var kosarak = _ws3.Kosars.Include(k => k.Felhasznalo).Include(k => k.Termek).ToList();
            if (kosarak.Count() == 0)
            {
                return BadRequest("Nincsenek kosárak");
            }

            //   {
            //     "id": 2,
            //     "felhasznaloId": 1,
            //     "termekId": 2,
            //     "mennyiseg": 12,
            //     "felhasznalo": {
            //       "id": 1,
            //       "nev": "Sebaj Tóbiás",
            //       "jelszo": "teszt",
            //       "email": "sebajtobias@mail.com",
            //       "rendeleseks": []
            //     }

            return Ok(kosarak);
        }
    }
}
