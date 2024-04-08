using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace wshop3.Model
{
    public class IdentityFelhasznalo : IdentityUser
    {
        public string TeljesNev { get; set; } = string.Empty;
        public int Iranyitoszam { get; set; }
        public string Varos { get; set; } = string.Empty;
        public string Utca { get; set; } = string.Empty;
        public int Hazszam { get; set; }
    }
}