using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace wshop3.Model
{
    public class IdentityFelhasznalo : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}