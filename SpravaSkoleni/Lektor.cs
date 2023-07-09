using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpravaSkoleni
{
    internal class Lektor : Zamestnanec
    {
        public List<Skoleni> VyucovanaSkoleni { get; set; }

        public Lektor(string jmeno, string prijmeni, string kod, string nazevPozice)
            :base (jmeno, prijmeni, kod, nazevPozice)
        {
            VyucovanaSkoleni = new List<Skoleni>();
        }
    }
}
