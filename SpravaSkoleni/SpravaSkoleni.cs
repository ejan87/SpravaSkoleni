using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpravaSkoleni
{
    internal class SpravaSkoleni
    {
        public List<Lektor> Lektori { get; set; }
        public List<Student> Studenti { get; set; }
        public List<Skoleni> SeznamSkoleni { get; set; }

        public SpravaSkoleni()
        {
            Lektori = new List<Lektor>();
            Studenti = new List<Student>();
            SeznamSkoleni = new List<Skoleni>();
        }

        public void NacistZamestnance()
        {
            StreamReader reader = new StreamReader("zamestnanci.txt");
            string radek = reader.ReadLine();
            while ((radek = reader.ReadLine()) != null)
            {
                string[] hodnoty = radek.Split(';');
                string kod = hodnoty[0];
                string jmeno = hodnoty[1];
                string prijmeni = hodnoty[2];
                string nazevPozice = hodnoty[3];
                bool jeLektor = false;

                if (hodnoty[4].ToLower() == "ano")
                {
                    jeLektor = true;
                }

                if (jeLektor)
                {
                    Lektor lektor = new Lektor(jmeno, prijmeni, kod, nazevPozice);
                    Lektori.Add(lektor);
                }
                else
                {
                    Student student = new Student(jmeno, prijmeni, kod, nazevPozice);
                    Studenti.Add(student);
                }
            }
            reader.Close();
        }
        public void VypisLadiciInfo()
        {
            Console.WriteLine("******* SPRÁVA ŠKOLENÍ ******");
            Console.WriteLine("Počet lektorů {0}, počet studentů {1}", Lektori.Count, Studenti.Count);

            Console.WriteLine("\nLEKTOŘI\n");
            foreach (Lektor lektor in Lektori)
            {
                Console.WriteLine("{0} - {1} {2} - {3}", lektor.Kod, lektor.Jmeno, lektor.Prijmeni, lektor.NazevPozice);
            }
            Console.WriteLine("\nSTUDENTI\n");
            foreach(Student student in Studenti)
            {
                Console.WriteLine("{0} - {1} {2} - {3}", student.Kod, student.Jmeno, student.Prijmeni, student.NazevPozice);
            }

            Console.WriteLine("\n****** SKOLENI ******\n");
            foreach (Skoleni skoleni in SeznamSkoleni)
            {
                Console.WriteLine("Název školení: {0}", skoleni.Nazev);
                Console.WriteLine("Uzavřeno: {0}", skoleni.Uzavreno);
                Console.WriteLine("Popis: {0}", skoleni.Popis);
                Console.WriteLine("Doporučený počet účastníků: {0}", skoleni.DoporucenyPocetUcastniku);
                Console.WriteLine("Datum konání: {0} - {1}", skoleni.PocatecniDatum.ToShortDateString(), skoleni.KoncoveDatum.ToShortDateString());
                Console.WriteLine("\tLektoři: ");
                foreach (Lektor lektor in skoleni.Lektori)
                {
                    Console.WriteLine("\t\t {0} {1} ({2})", lektor.Jmeno, lektor.Prijmeni, lektor.Kod);
                }
                Console.WriteLine("\tStudenti: ");
                foreach (Student student in skoleni.Studenti)
                {
                    if (skoleni.Uzavreno)
                    {
                        Console.WriteLine("\t\t {0} {1} ({2}). Hodnoceno: {3} %", student.Jmeno, student.Prijmeni, student.Kod, skoleni.HodnoceniStudentu[student]);
                    }
                    else
                    {
                        Console.WriteLine("\t\t {0} {1} ({2})", student.Jmeno, student.Prijmeni, student.Kod);
                    }
                    
                }
            }
            
        }
        public void NacistSkoleni()
        {
            StreamReader reader = new StreamReader("skoleni.txt");
            // Přeskočení prvního řádku, který nechceme číst
            reader.ReadLine();
            string radek = "";

            while ((radek = reader.ReadLine()) != null)
            {
                string[] informaceSkoleni = radek.Split(';');
                string nazev = informaceSkoleni[0];
                string popis = informaceSkoleni[1];
                string[] pocatecniDatumSlozky = informaceSkoleni[2].Split('.');
                DateTime pocatecniDatum = new DateTime(int.Parse(pocatecniDatumSlozky[2]), int.Parse(pocatecniDatumSlozky[1]), int.Parse(pocatecniDatumSlozky[0]));
                string[] koncoveDatumSLozky = informaceSkoleni[3].Split(".");
                DateTime koncoveDatum = new DateTime(int.Parse(koncoveDatumSLozky[2]), int.Parse(koncoveDatumSLozky[1]), int.Parse(koncoveDatumSLozky[0]));
                int doporucenyPocetUcastniku = int.Parse(informaceSkoleni[4]);
                bool skoleniUzavreno = false;
                if (informaceSkoleni[5] == "ANO")
                {
                    skoleniUzavreno = true;
                }
                // Přeskočíme řádek, který nechceme číst (LEKTORI - toto je jen hlavicka v souboru)
                reader.ReadLine();
                List<Lektor> lektori = new List<Lektor>();
                while ((radek = reader.ReadLine()) != "UCASTNICI")
                {
                    //Dokud nenarazíme na řádek UCASTNICI, čteme lektory
                    lektori.Add(NajdiLektoraPodleKodu(radek));
                }
                List<Student> studenti = new List<Student>();
                while ((radek = reader.ReadLine()) != "HODNOCENI")
                {
                    //Čteme studenty,dokud nenarazíme na řádek HODNOCENI
                    studenti.Add(NajdiStudentaPodleKodu(radek));
                }
                Dictionary<Student, int> hodnoceni = new Dictionary<Student, int>();
                while (true)
                {
                    //Dokud nenarazíme na řádek SKOLENI, čteme hodnocení
                    radek = reader.ReadLine();
                    if (radek == null || radek == "SKOLENI")
                    {
                        break;
                    }
                    string[] hodnotyHodnoceni = radek.Split(';');

                    hodnoceni.Add(NajdiStudentaPodleKodu(hodnotyHodnoceni[0]), Int32.Parse(hodnotyHodnoceni[1]));
                }

                /*
                 * Vytvoříme instanci Skoleni. Ta v sobě bude mít informaci, kteří lektoři a studenti se školení účastní.
                 * Zároveň jednotlivým studentům a lektorům přiřadíme informaci o výše vytvořeném školení.
                 * Toto se nám bude hodit ve chvíli, kdy budeme chtít jednoduše získat pro daného studenta nebo lektora seznam
                 * jeho školení.
                 * */
                Skoleni skoleni = new Skoleni(nazev, popis, pocatecniDatum, koncoveDatum,
                    studenti, lektori, hodnoceni, doporucenyPocetUcastniku, skoleniUzavreno);
                foreach (Student student in Studenti)
                {
                    student.Skoleni.Add(skoleni);
                }
                foreach (Lektor lektor in Lektori)
                {
                    lektor.VyucovanaSkoleni.Add(skoleni);
                }
                SeznamSkoleni.Add(skoleni);
            }
            reader.Close();
        }
        public Lektor NajdiLektoraPodleKodu(string kod)
        {
            foreach (Lektor lektor in Lektori)
            {
                if (lektor.Kod == kod)
                {
                    return lektor;
                }
            }
            return null;
        }
        public Student NajdiStudentaPodleKodu(string kod)
        {
            foreach (Student student in Studenti)
            {
                if (student.Kod == kod)
                {
                    return student;
                }
            }
            return null;
        }
        public void NoveSkoleni()
        {
            //Nazev, Popis, PocatecniDatum, KoncoveDatum, DoporucenyPocetUcastniku
            Console.WriteLine("****** VYTVOŘIT NOVÉ ŠKOLENÍ ******");
            Console.Write("Název školení: ");
            string nazev = Console.ReadLine();
            Console.Write("Popis školení: ");
            string popis = Console.ReadLine();
            Console.Write("Doporučený počet účastníků: ");
            int doporucenyPocetUcastniku = int.Parse(Console.ReadLine());

            Console.WriteLine("Počáteční datum");

            Console.Write("Den: ");
            int pocDatDen = int.Parse(Console.ReadLine());
            Console.Write("Měsíc: ");
            int pocDatMes = int.Parse(Console.ReadLine());
            Console.Write("Rok: ");
            int pocDatRok = int.Parse(Console.ReadLine());
            DateTime pocatecniDatum = new DateTime(pocDatRok, pocDatMes, pocDatDen);

            Console.WriteLine("Koncové datum");

            Console.Write("Den: ");
            int konDatDen = int.Parse(Console.ReadLine());
            Console.Write("Měsíc: ");
            int konDatMes = int.Parse(Console.ReadLine());
            Console.Write("Rok: ");
            int konDatRok = int.Parse(Console.ReadLine());
            DateTime koncoveDatum = new DateTime(konDatRok, konDatMes, konDatDen);

            Skoleni skoleni = new Skoleni(nazev, popis, pocatecniDatum, koncoveDatum,
                new List<Student>(), new List<Lektor>(), new Dictionary<Student, int>(), doporucenyPocetUcastniku, false);
            SeznamSkoleni.Add(skoleni);

            UlozitSkoleni();
            Console.WriteLine("Školení bylo úspěšně vytvořeno!");
        }
        public void UlozitSkoleni()
        {
            StreamWriter writer = new StreamWriter("skoleni.txt");
            foreach (Skoleni skoleni in SeznamSkoleni)
            {
                // Zápis SKOLENI na radek
                writer.WriteLine("SKOLENI");
                // Uložení částí datumu do proměnných a vytvoření formátovaného řetězce
                string pocatecniDatum = String.Format("{0}.{1}.{2}", skoleni.PocatecniDatum.Day,
                    skoleni.PocatecniDatum.Month, skoleni.PocatecniDatum.Year);
                string koncoveDatum = String.Format("{0}.{1}.{2}", skoleni.KoncoveDatum.Day,
                    skoleni.KoncoveDatum.Month, skoleni.KoncoveDatum.Year);
                string uzavreno = "NE";
                if (skoleni.Uzavreno)
                {
                    uzavreno = "ANO";
                }
                // Zápis dat do řádku
                writer.WriteLine("{0};{1};{2};{3};{4};{5}", skoleni.Nazev, skoleni.Popis,
                    pocatecniDatum, koncoveDatum, skoleni.DoporucenyPocetUcastniku, uzavreno);
                // Zapsání seznamu lektorů
                writer.WriteLine("LEKTORI");
                foreach (Lektor lektor in skoleni.Lektori)
                {
                    writer.WriteLine(lektor.Kod);
                }
                // Zapsání seznamu studentů
                writer.WriteLine("UCASTNICI");
                foreach (Student student in skoleni.Studenti)
                {
                    writer.WriteLine(student.Kod);
                }
                // Zapsání hodnocení studentů ve formátu KOD_ZAMESTNANCE;PROCENTULNI_HODNOCENI
                writer.WriteLine("HODNOCENI");
                foreach (Student student in skoleni.HodnoceniStudentu.Keys)
                {
                    writer.WriteLine("{0};{1}", student.Kod, skoleni.HodnoceniStudentu[student]);
                }
            }
            writer.Close();
        }
        public Skoleni VybratSkoleni()
        {
            Console.WriteLine("****** Vybrat školení ******");
            for (int i = 0; i < SeznamSkoleni.Count; i++)
            {
                string stav = "otevřené";

                if (SeznamSkoleni[i].Uzavreno)
                {
                    stav = "uzavřené";
                }
                Console.WriteLine("{0}. {1} - {2}", i + 1, SeznamSkoleni[i].Nazev, stav);
            }
            Console.WriteLine("0. zpět");
            int volba = int.Parse(Console.ReadLine()) - 1;
            if (volba >= 0 && volba < SeznamSkoleni.Count)
            {
                return SeznamSkoleni[volba];
            }
            else
            {
                return null;
            }
        }
        public void ZobrazDetailSkoleni(Skoleni skoleni)
        {
            string stav = "Otevřené";
            if (skoleni.Uzavreno)
            {
                stav = "Uzavřené";
            }
            Console.WriteLine("****** Detail šklení ******");
            Console.WriteLine("Název: {0}", skoleni.Nazev);
            Console.WriteLine("Stav: {0}", stav);
            Console.WriteLine("Popis: {0}", skoleni.Popis);
            Console.WriteLine("Doporučený počet účastníků: {0}", skoleni.DoporucenyPocetUcastniku);
            Console.WriteLine("Datum konání: {0} - {1}", skoleni.PocatecniDatum.ToShortDateString(), skoleni.KoncoveDatum.ToShortDateString());
            
            Console.WriteLine("Lektoři:");
            foreach (Lektor lektor in skoleni.Lektori)
            {
                Console.WriteLine("{0} {1} - {2}", lektor.Jmeno, lektor.Prijmeni, lektor.Kod);
            }

            Console.WriteLine("Studenti:");
            foreach (Student student in skoleni.Studenti)
            {
                if (skoleni.Uzavreno)
                {
                    Console.WriteLine("{0} {1} - {2} ({3}%)", student.Jmeno, student.Prijmeni,
                        student.Kod, skoleni.HodnoceniStudentu[student]);
                }
                else
                {
                    Console.WriteLine("{0} {1} - {2})", student.Jmeno, student.Prijmeni,
                        student.Kod);
                }
               
            }
        }
    }
}
