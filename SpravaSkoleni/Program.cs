namespace SpravaSkoleni
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpravaSkoleni spravaSkoleni = new SpravaSkoleni();
            spravaSkoleni.NacistZamestnance();
            spravaSkoleni.NacistSkoleni();
            //spravaSkoleni.VypisLadiciInfo();

            //string[] polozkyMenu = { "Vytvoření nového školení", "Zobrazení seznamu školení", "Ukončit program", "Výpis ladících informací" };
            //string nadpis = "Správa školení";
            int volba = 0;
            while (volba != 4)
            {
                volba = ZobrazVyberMenu("Hlavní Menu", new string[] { "Vytvoření nového školení", "Zobrazení seznamu školení", "Výpis ladících informací", "Ukončit program" });

                switch (volba)
                {
                    case 1:
                        //Console.WriteLine("Implementujeme");
                        spravaSkoleni.NoveSkoleni();
                        break;
                    case 2:
                        Skoleni vybraneSkoleni = spravaSkoleni.VybratSkoleni();
                        if (vybraneSkoleni != null)
                        {
                            int volbaDetailSkoleni = 0;
                            while (volbaDetailSkoleni != 6)
                            {
                                spravaSkoleni.ZobrazDetailSkoleni(vybraneSkoleni);
                                if (vybraneSkoleni.Uzavreno)
                                {
                                    Console.WriteLine("Školení je uzavřené a nelze s ním provést " +
                                        "žádnou další operaci. Pro návrat zpět stiskněte libovolnou klávesu!");
                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    volbaDetailSkoleni = ZobrazVyberMenu("Školení", new string[] {"Přihlásit studenta", "Odhlásit studenta",
                                    "Přiřadit lektora", "Odebrat lektora", "Uzavřít školení", "Zpět na hlavní menu"});

                                    switch (volbaDetailSkoleni)
                                    {
                                        case 1:
                                            // Zde zavolat metodu
                                            // spravaSkoleni.PrihlasitStudenta(vybraneSkoleni)
                                            break;
                                        case 2:
                                            // Zde zavolat metodu
                                            // spravaSkoleni.OdhlasitStudenta(vybraneSkoleni)
                                            break;
                                        case 3:
                                            // Zde zavolat metodu
                                            // spravaSkoleni.PriraditLektora(vybraneSkoleni)
                                            break;
                                        case 4:
                                            // Zde zavolat metodu
                                            // spravaSkoleni.OdebratLektora(vybraneSkoleni)
                                            break;
                                        case 5:
                                            // Zde zavolat metodu
                                            // spravaSkoleni.UzavritSkoleni(vybraneSkoleni)
                                            break;
                                    }
                                }
                            }  
                        }
                        break;
                    case 3:
                        spravaSkoleni.VypisLadiciInfo();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Chybně zadaná volba! Prosím zadejte 1, 2, 3 nebo 4!");
                        break;
                }
            }

            
        }

        static int ZobrazVyberMenu(string nadpis, string[] polozkyMenu)
        {
            int volba = 0;
            Console.WriteLine("*** MENU - {0} ***", nadpis);
            while (volba <= 0 || volba > polozkyMenu.Length + 1)
            {
                for (int i = 0; i < polozkyMenu.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, polozkyMenu[i]);
                }
                Console.Write("Vaše volba: ");
                volba = Int32.Parse(Console.ReadLine());
            }
            return volba;
        }
    }
}