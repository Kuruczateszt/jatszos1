using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.DtoMap;
using wshop3.filt;
using wshop3.Model;

namespace wshop3.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TermekekController : ControllerBase
    {
        private readonly Wshop3Context _ws3;

        //segéd funkció a jpg file -ok ellenőrzéséhez
        private static bool Jpge(Stream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);

            //jpeg első 4 byte -ja ez kell legyen
            if (buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF && buffer[3] == 0xE0)
            {
                return true;
            }

            return false;
        }
        public TermekekController(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("OsszesTermek")]
        public IActionResult OsszesTermek([FromQuery] Szures szur)
        {
            var termekek = _ws3.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep).ToList().Select(t => t.TermekLekerdezDto()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(szur.Keres))
            {
                termekek = termekek.Where(t => t.Nev.Contains(szur.Keres) || (t.Leiras != null && t.Leiras.Contains(szur.Keres)));
            }

            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }

            return Ok(termekek);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("Termek/{keres}")]
        public IActionResult OsszesTermek([FromRoute] string keres)
        {
            var termekek = _ws3.Termekeks
            .Where(t => t.Nev.Contains(keres) || (t.Leiras != null && t.Leiras.Contains(keres)))
            .Include(t => t.Kategoria)
            .Include(t => t.TermekKep)
            .ToList().Select(t => t.TermekLekerdezDto());
            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }
            return Ok(termekek);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpDelete("TermekTorles/{id}")]
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

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("TermekUj")]
        public IActionResult TermekUj([FromForm] IFormCollection termekAdatok)
        //curl -X POST "http://localhost:5130/api/Termekek" -F "Nev=Whiskyvalami11" -F "Ar=10,9" -F "KategoriaId=1" -F "Kep=@product_005.webp"
        {
            if (termekAdatok["Nev"] == string.Empty ||
            termekAdatok["Ar"] == string.Empty ||
            termekAdatok["KategoriaId"] == string.Empty ||
            //csak egy file -t lehet feltölteni a kép választó mezőben
            termekAdatok.Files["Kep"] == null || termekAdatok.Files.Count != 1)
            {
                return BadRequest("Nincsenek az adatok megfelelően megadva");
            }

            var termek = new Termekek
            {
                Nev = termekAdatok["Nev"]!,
                Ar = Convert.ToDecimal(termekAdatok["Ar"]),
                //termék leírás nem kötelező
                Leiras = termekAdatok["Leiras"]!,
                KategoriaId = Convert.ToInt32(termekAdatok["KategoriaId"])
            };

            var kep = new TermekKepek();

            IFormFile file = termekAdatok.Files["Kep"]!;

            if (file == null || file.Length == 0)
            {
                return BadRequest("Nincs megadva kep");
            }

            //csak jpg lehet
            if (file.ContentType != "image/jpeg" || !Jpge(file.OpenReadStream()))
            {
                return BadRequest("Csak jpeg fényképek tölthetőek fel");
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

            using (var t = _ws3.Database.BeginTransaction())
            {
                try
                {
                    _ws3.TermekKepeks.Add(kep);
                    _ws3.SaveChanges();
                    termek.TermekKepId = kep.Id;
                    _ws3.Termekeks.Add(termek);
                    _ws3.SaveChanges();
                    t.Commit();
                    return Ok("termék rögzívte");
                }
                catch (Exception)
                {
                    t.Rollback();
                    return StatusCode(500, "Szerver hiba");
                }
            }
        }
    }
}
