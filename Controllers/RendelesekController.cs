using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wshop3.Datab;
using wshop3.Dto;
using wshop3.Migrations2;
using wshop3.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace wshop3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RendelesekController : ControllerBase
    {
        private readonly IdentityContext _identity;
        private readonly IRendelesekRepo _rendelesek_repo;
        private readonly ITermekekRepo _termekek_repo;
        public RendelesekController(IdentityContext IdentityContext, IRendelesekRepo rendelesek_repo, ITermekekRepo termekek_repo)
        {
            _identity = IdentityContext;
            _rendelesek_repo = rendelesek_repo;
            _termekek_repo = termekek_repo;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("RendelesId/{id}")]
        public async Task<IActionResult> rendelesekId([FromRoute] int id)
        {
            var rendeles = await _rendelesek_repo.RendelesekIdAsync(id);
            if (rendeles == null)
            {
                return BadRequest("Nincs ilyen renelés");
            }
            return Ok(rendeles);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("OsszesRendeles")]
        public async Task<IActionResult> OsszesRendelesek()
        {
            var rendelesek = await _rendelesek_repo.OsszesRendelesekAsync();

            if (rendelesek.Count() == 0)
            {
                return BadRequest("Nincsenek rendelesek");
            }

            return Ok(rendelesek);

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("RendelesekUj")]
        public async Task<IActionResult> RendelesekUjPost([FromBody] RendelesUjDto adatok)
        {
            // {
            //     "FelhasznaloId": 12,
            //     "Termekek":
            //     [
            //     {
            //         "Id": 1,
            //         "Mennyiseg": 12
            //     },
            //     {
            //         "Id": 1,
            //         "Mennyiseg": 12
            //     }
            //     ]
            // }

            if (!await _identity.Users.AnyAsync(u => u.Id == adatok.FelhasznaloId))
            {
                return BadRequest("Nincs ilyen felhasználó");
            }

            if (adatok.Termekek.Count() == 0)
            {
                return BadRequest("Nincsenek termékek");
            }

            var termekek2 = new Dictionary<int, int>();
            foreach (var t in adatok.Termekek)
            {
                //léteznek -e a termékek
                if (await _termekek_repo.TermekLetezikEAsync(t.Id) == false)
                {
                    return BadRequest($"Nem létező termékek");
                }

                //több azonos termék esetén összeadjuk a mennyiséget a rendeléshez
                if (termekek2.ContainsKey(t.Id))
                {
                    termekek2[t.Id] += t.Mennyiseg;
                }
                else
                {
                    termekek2.Add(t.Id, t.Mennyiseg);
                }
            }

            var rendezett_t_lista = new List<TermekRndelesDto>();
            foreach (var t in termekek2)
            {
                rendezett_t_lista.Add(new TermekRndelesDto()
                {
                    Id = t.Key,
                    Mennyiseg = t.Value
                });
            }

            adatok.Termekek = rendezett_t_lista;

            try
            {
                var rendeles = await _rendelesek_repo.RendelesekUjAsync(adatok);
                return Ok($"rendelés felvétele sikeres: {rendeles.Id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Szerver hiba {e.Message}");
            }
        }
    }
}
