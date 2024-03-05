using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KosarController : ControllerBase
    {
        private readonly Wshop3Context _wshop3c;
        public KosarController(Wshop3Context whop3Context)
        {
            _wshop3c = whop3Context;
        }

        [HttpGet("{id}")]
        public IActionResult KosarId([FromRoute] int id)
        {
            var kosar = _wshop3c.Kosars
            .Include(k => k.Termek)
            .Include(f => f.Felhasznalo)
            .FirstOrDefault(k => k.Id == id);

            if (kosar == null)
            {
                return BadRequest("Nincs ilyen kosár");
            }
            return Ok(kosar);
        }
    }
}
