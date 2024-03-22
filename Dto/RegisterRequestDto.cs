using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wshop3.Dto
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
    }
}