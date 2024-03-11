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

        [HttpGet]
        public IActionResult OsszesTermek()
        {
            var termekek = _ws3.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep);
            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }
            return Ok(termekek);
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
        public IActionResult TermekUj([FromBody] TermekLetrehozDto termek)
        {
            var UjTermek = new Termekek
            {
                Nev = termek.Nev,
                Ar = termek.Ar,
                Leiras = termek.Leiras,
                KategoriaId = termek.KategoriaId
            };

            _ws3.Add(UjTermek);
            _ws3.SaveChanges();
            return Ok(UjTermek);
        }
    }
}
