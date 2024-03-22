using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wshop3.Dto
{
    public class RegisterResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }
}