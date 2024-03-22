using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wshop3.Dto
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}