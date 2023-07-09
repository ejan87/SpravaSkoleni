using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpravaSkoleni
{
    internal class Student : Zamestnanec
    {
        public List<Skoleni> Skoleni { get; set; }

        public Student(string jmeno, string prijmeni, string kod, string nazevPozice)
            :base(jmeno, prijmeni, kod, nazevPozice)
        {
            Skoleni = new List<Skoleni>();
        }
    }
}
