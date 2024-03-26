using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace wshop3.Controllers
{
    //rendelések feldolgozásához
    public class TermekJson
    {
        public int Id { get; set; }
        public int Mennyiseg { get; set; }
    }

    public class RendelesekJson
    {
        public int FelhasznaloId { get; set; }
        public List<TermekJson> Termekek { get; set; } = new List<TermekJson>();
    }


    [ApiController]
    [Route("api/[controller]")]
    public class RendelesekController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public RendelesekController(Wshop3Context wshop3Context)
        {
            _ws3 = wshop3Context;
        }

        [HttpGet("RendelesId/{id}")]
        public IActionResult rendelesekId([FromRoute] int id)
        {
            var rendeles = _ws3.Rendeleseks.Where(r => r.Id == id);
            if (rendeles == null)
            {
                return BadRequest("Nincs ilyen renelés");
            }
            return Ok(rendeles);
        }

        [HttpGet("OsszesRendeles")]
        public IActionResult OsszesRendelesek()
        {
            var rendelesek = _ws3.Rendeleseks;

            if (rendelesek.Count() == 0)
            {
                return BadRequest("Nincsenek rendelesek");
            }

            return Ok(rendelesek);

        }

        [HttpPost("RendelesekUj")]
        public IActionResult RendelesekUjPost([FromBody] RendelesekJson adatok)
        {
            // {
            //     "FelhasznaloId": 12,
            //     "Termekek":
            //     [
            //     {
            //         "Id": 1,
            //         "Mennyiseg": 12
            //     },
            //     {
            //         "Id": 1,
            //         "Mennyiseg": 12
            //     }
            //     ]
            // }

            var rendeles = new Rendelesek()
            {
                FelhasznaloId = adatok.FelhasznaloId,
            };

            foreach (var t in adatok.Termekek)
            {
                int termekId = t.Id;
                var termek = _ws3.Termekeks.FirstOrDefault(t => t.Id == termekId);
                if (termek == null)
                {
                    return BadRequest($"Nem létező termék, id: {termekId}");
                }
                rendeles.Termeks.Add(termek);
            }

            _ws3.Rendeleseks.Add(rendeles);
            _ws3.SaveChanges();

            return Ok("Rendeles elfogadva");
        }
    }
}