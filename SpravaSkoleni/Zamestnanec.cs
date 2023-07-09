using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpravaSkoleni
{
    internal class Zamestnanec
    {
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Kod { get; set; }
        public string NazevPozice { get; set; }

        public Zamestnanec(string jmeno, string prijmeni, string kod, string nazevPozice)
        {
            
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Kod = kod;
            NazevPozice = nazevPozice;
        }
    }
}
