using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wshop3.Model;

namespace wshop3.Service.IAuth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityFelhasznalo identityFelhasznalo, IEnumerable<string> roles);
    }
}