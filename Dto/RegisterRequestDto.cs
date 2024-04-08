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
        public string TeljesNev { get; set; } = string.Empty;
        public int Iranyitoszam { get; set; }
        public string Varos { get; set; } = string.Empty;
        public string Utca { get; set; } = string.Empty;
        public int Hazszam { get; set; }
    }
}