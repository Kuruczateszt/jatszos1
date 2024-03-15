using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TermekekController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public TermekekController(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }

        [HttpGet("{id}")]
        public IActionResult TermekekId([FromRoute] int id)
        {
            var termek = _ws3.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep).FirstOrDefault(t => t.Id == id);
            if (termek == null)
            {
                return BadRequest("Nincs ilyen termek");
            }
            return Ok(termek);
        }

        [HttpGet("OsszesTermek")]
        public IActionResult OsszesTermek()
        {
            var termekek = _ws3.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep).ToList();
            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }

            var vissza = new List<TermekOsszesLekerdezDto>();

            foreach (var termek in termekek)
            {
                var t = new TermekOsszesLekerdezDto()
                {
                    Id = termek.Id,
                    Nev = termek.Nev,
                    Ar = termek.Ar,
                    Leiras = termek.Leiras,
                    Kategoria = termek.Kategoria,

                };
                if (termek.TermekKep != null && termek.TermekKep.Kep != null)
                {
                    t.TermekKepB64 = Convert.ToBase64String(termek.TermekKep.Kep);
                }
                vissza.Add(t);
            }
            return Ok(vissza);
        }

        [HttpDelete("id")]
        public IActionResult TermekTorles([FromRoute] int id)
        {
            var termek = _ws3.Termekeks.FirstOrDefault(t => t.Id == id);
            if (termek == null)
            {
                return BadRequest("Nincs ilyen termek");
            }
            _ws3.Termekeks.Remove(termek);
            _ws3.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult TermekUj([FromForm] IFormCollection termekAdatok)
        //curl -X POST "http://localhost:5130/api/Termekek" -F "Nev=Whiskyvalami11" -F "Ar=10,9" -F "KategoriaId=1" -F "Kep=@product_005.webp"
        {
            var termek = new Termekek
            {
                Nev = termekAdatok["Nev"],
                Ar = Convert.ToDecimal(termekAdatok["Ar"]),
                KategoriaId = Convert.ToInt32(termekAdatok["KategoriaId"])
            };

            var kep = new TermekKepek();

            IFormFile file = termekAdatok.Files["Kep"];

            if (file == null || file.Length == 0)
            {
                return BadRequest("Nincs megadva kep");
            }

            //csak jpg lehet
            if (file.ContentType != "image/jpeg")
            {
                return BadRequest("Csak jpeg fényképek tölthetőek fel lehet");
            }

            //5 MB maximum
            if (file.Length > 5242880)
            {
                return BadRequest("File mérete nem lehet nagyobb mint 5mb");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                kep.Kep = memoryStream.ToArray();
            }

            _ws3.TermekKepeks.Add(kep);
            _ws3.SaveChanges();

            termek.TermekKepId = kep.Id;
            _ws3.Termekeks.Add(termek);
            _ws3.SaveChanges();
            return Ok("termék rögzívte");
        }
    }
}
