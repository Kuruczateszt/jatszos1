using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Model;

namespace wshop3.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TermekekController : ControllerBase
    {
        private readonly Wshop3Context _whop3c;
        public TermekekController(Wshop3Context whop3Context)
        {
            _whop3c = whop3Context;
        }

        [HttpGet("{id}")]
        public IActionResult TermekekId([FromRoute] int id)
        {
            var termek = _whop3c.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep).FirstOrDefault(t => t.Id == id);
            if (termek == null)
            {
                return BadRequest("Nincs ilyen termek");
            }
            return Ok(termek);
        }

        [HttpGet]
        public IActionResult OsszesTermek()
        {
            var termekek = _whop3c.Termekeks.Include(t => t.Kategoria).Include(t => t.TermekKep);
            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }
            return Ok(termekek);
        }

        [HttpDelete("id")]
        public IActionResult TermekTorles([FromRoute] int id)
        {
            var termek = _whop3c.Termekeks.FirstOrDefault(t => t.Id == id);
            if (termek == null)
            {
                return BadRequest("Nincs ilyen termek");
            }
            _whop3c.Termekeks.Remove(termek);
            _whop3c.SaveChanges();
            return Ok();
        }

    }
}