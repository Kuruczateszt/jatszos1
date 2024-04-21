using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wshop3.Dto
{
    public class RendelesUjDto
    {
        public string FelhasznaloId { get; set; } = string.Empty;
        public List<TermekRndelesDto> Termekek { get; set; } = new List<TermekRndelesDto>();
    }
}