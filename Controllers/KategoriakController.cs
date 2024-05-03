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
        private readonly IKategoriakRepo _repo;
        public KategoriakController(IKategoriakRepo repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("OsszesKategoria")]
        public async Task<IActionResult> OsszesKategoria()
        {
            var kategoriak = await _repo.OsszesKategoriaAsync();
            if (kategoriak.Count() == 0)
            {
                return BadRequest("Nincsenek kategóriak");
            }
            return Ok(kategoriak);
        }

        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPost("kategoriaUJ")]
        public async Task<IActionResult> kategoriaUj([FromBody] KategoriakDto kategoriaDto)
        {
            if (kategoriaDto.Nev == string.Empty)
            {
                return BadRequest("Nincs megadva nev");
            }

            var kategoria = new Kategoriak()
            {
                Nev = kategoriaDto.Nev
            };

            try
            {
                kategoria = await _repo.KategoriaUjAsync(kategoria);
                return Ok($"kategoria felvétele sikeres: {kategoria.Nev}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Szerver hiba {e.Message}");
            }
        }
    }
}