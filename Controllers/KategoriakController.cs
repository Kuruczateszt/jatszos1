using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriakController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public KategoriakController(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }

        [HttpPut("/kategoriaUJ")]
        public IActionResult kategoriaUj([FromBody] KategoriakDto kategoriaDto)
        {
            if (kategoriaDto.Nev == string.Empty)
            {
                return BadRequest("Nincs megadva nev");
            }

            var kategoria = new Kategoriak()
            {
                Nev = kategoriaDto.Nev
            };
            _ws3.Kategoriaks.Add(kategoria);
            _ws3.SaveChanges();
            return Ok(kategoria);
        }
    }
}