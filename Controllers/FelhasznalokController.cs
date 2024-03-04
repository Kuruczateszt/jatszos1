using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Dto;
using wshop3.Model;

namespace wshop3.Controllers
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

        [HttpGet("{id}")]
        public IActionResult FelhasznaloId([FromRoute] int id)
        {
            var felhasznalo = _whop3Context.Felhasznaloks.FirstOrDefault(f => f.Id == id);
            if (felhasznalo == null)
            {
                return BadRequest("Nincs ilyen felhasználó");
            }
            return Ok(felhasznalo);
        }

        [HttpGet]
        [Route("OsszesFelhasznalo")]
        public IActionResult OsszesFelhasznalo()
        {
            var felhasznalok = _whop3Context.Felhasznaloks.ToList();
            if (felhasznalok.Count() == 0)
            {
                return BadRequest("Nincsenek felhasználók");
            }
            return Ok(felhasznalok);
        }

        [HttpPost("FelhasznaloHozzaadas")]
        public IActionResult FelhasznaloHozzaadas([FromBody] FelhasznaloLetrehozDto felhasznalo)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(felhasznalo.Jelszo));

            var ujfelhasznalo = new Felhasznalok
            {
                Email = felhasznalo.Email,
                Jelszo = Convert.ToBase64String(hash),
                Nev = felhasznalo.Nev
            };

            System.Console.WriteLine(ujfelhasznalo);

            _whop3Context.Felhasznaloks.Add(ujfelhasznalo);
            _whop3Context.SaveChanges();
            return Ok();
        }
    }
}