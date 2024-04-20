using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategoriakController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        private readonly IKategoriakRepo _repo;
        public KategoriakController(Wshop3Context whop3Context, IKategoriakRepo repo)
        {
            _ws3 = whop3Context;
            _repo = repo;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("KategoriaId/{id}")]
        public async Task<IActionResult> KategoriaId([FromRoute] int id)
        {
            var kategoria = await _repo.TermekekIdAsync(id);
            if (kategoria == null)
            {
                return BadRequest("Nincs ilyen kategória");
            }
            return Ok(kategoria);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("OsszesKategoria")]
        public IActionResult OsszesKategoria()
        {
            var kategoriak = _ws3.Kategoriaks;
            if (kategoriak.Count() == 0)
            {
                return BadRequest("Nincsenek kategóriak");
            }
            return Ok(kategoriak);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("kategoriaUJ")]
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