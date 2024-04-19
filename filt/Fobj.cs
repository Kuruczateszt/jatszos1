using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wshop3.filt
{
    public class Szures
    {
        //rendez + irány
        public string? Rendez { get; set; } = null;
        public bool CsokkenoSorrend { get; set; } = false;
        //kereső szó
        public string? Keres { get; set; } = null;
        //tördeléshez
        //ha nincs megadva, mindig az első oldal jelenik meg
        public int Lapszam { get; set; } = 1;
        //egyszerre megjelenő termékek száma.
        public int Lapmeret { get; set; } = 10;
    }
}