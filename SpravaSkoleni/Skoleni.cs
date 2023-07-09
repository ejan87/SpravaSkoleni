using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpravaSkoleni
{
    internal class Skoleni
    {
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public DateTime PocatecniDatum { get; set; }
        public DateTime KoncoveDatum { get; set; }
        public List<Student> Studenti { get; set; }
        public List<Lektor> Lektori { get; set; }
        public Dictionary<Student, int> HodnoceniStudentu { get; set; }
        public int DoporucenyPocetUcastniku { get; set; }
        public bool Uzavreno { get; set; }

        public Skoleni(string nazev, string popis, DateTime pocatecniDatum, DateTime koncoveDatum, List<Student> studenti,
                        List<Lektor> lektori, Dictionary<Student, int> hodnoceniStudentu, int doporucenyPocetUcastniku, bool uzavreno)
        {
            Nazev = nazev;
            Popis = popis;
            PocatecniDatum = pocatecniDatum;
            KoncoveDatum = koncoveDatum;
            Studenti = studenti;
            Lektori = lektori;
            HodnoceniStudentu = hodnoceniStudentu;
            DoporucenyPocetUcastniku = doporucenyPocetUcastniku;
            Uzavreno = uzavreno;
        }
    }
}
