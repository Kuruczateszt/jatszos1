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
    //rendelések feldolgozásához
    public class TermekJson
    {
        public int Id { get; set; }
        public int Mennyiseg { get; set; }
    }

    public class RendelesekJson
    {
        public string FelhasznaloId { get; set; } = string.Empty;
        public List<TermekJson> Termekek { get; set; } = new List<TermekJson>();
    }


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
        public async Task<IActionResult> RendelesekUjPost([FromBody] RendelesekJson adatok)
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

            if (await _identity.Users.FirstOrDefaultAsync(u => u.Id == adatok.FelhasznaloId) == null)
            {
                return BadRequest("Nincs ilyen felhasználó");
            }

            var rendeles = new Rendelesek()
            {
                FelhasznaloId = adatok.FelhasznaloId,
            };

            foreach (var t in adatok.Termekek)
            {
                int termekId = t.Id;
                var termek = await _termekek_repo.TermekekIdAsync(termekId);
                if (termek == null)
                {
                    return BadRequest($"Nem létező termékek");
                }
                rendeles.Termeks.Add(termek);
            }

            try
            {
                rendeles = await _rendelesek_repo.RendelesekUjAsync(rendeles);
                return Ok($"rendelés felvétele sikeres: {rendeles.Id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Szerver hiba {e.Message}");
            }
        }
    }
}