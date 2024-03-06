using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wshop3.Model;

namespace wshop3.Controllers
{
    [ApiController]
    public class KategoriakController : ControllerBase
    {
        private readonly Wshop3Context _ws3;
        public KategoriakController(Wshop3Context whop3Context)
        {
            _ws3 = whop3Context;
        }
    }
}