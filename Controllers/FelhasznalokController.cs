using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Model;

namespace wshop3.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FelhasznalokController : ControllerBase
    {
        private readonly Wshop3Context _whop3Context;
        public FelhasznalokController(Wshop3Context whop3Context)
        {
            _whop3Context = whop3Context;
        }

        [HttpGet]
        public IActionResult OsszesTermek()
        {
            var termekek = _whop3Context.Termekeks;
            if (termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termekek");
            }
            return Ok(termekek);
        }

    }
}