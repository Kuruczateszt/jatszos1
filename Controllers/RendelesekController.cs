using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RendelesekController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public RendelesekController(Wshop3Context wshop3Context)
        {
            _ws3 = wshop3Context;
        }

        [HttpGet]
        public IActionResult OsszesRendelesek()
        {
            var rendelesek = _ws3.Rendeleseks;

            if (rendelesek.Count() == 0)
            {
                return BadRequest("Nincsenek rendelesek");
            }

            return Ok(rendelesek);

        }
    }
}