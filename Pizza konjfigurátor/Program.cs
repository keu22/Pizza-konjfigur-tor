namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        class Pizza
        {
            public string Druh { get; set; }
            public string Pridavky { get; set; }
            public int Pocitadlo { get; set; }

            public Pizza(string druh, string pridavky, int pocitadlo)
            {
                this.Druh = druh;
                this.Pridavky = pridavky;
                this.Pocitadlo = pocitadlo;
            }
        }

        static void menu(ref int vyber)
        {
            Console.WriteLine("1. Salámová");
            Console.WriteLine("2. Šunková");
            Console.WriteLine("3. Ananasová");
            Console.WriteLine("4. Kukuřicová");
            Console.WriteLine();
            Console.Write("Výběr: ");
            vyber = int.Parse(Console.ReadLine()!);
        }

        static void rozhod(ref string rozhodnuti, ref string finRozhodnuti)
        {
            rozhodnuti = Console.ReadLine()!;
            finRozhodnuti = rozhodnuti.ToLower();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            string rozhodnuti = "", finRozhodnuti = "", pridavky = "";
            int vyber = 0, pocitadlo = 1;
            bool pridatJid = false, objednatPiz = false, oblibenaPizza = false;
            List<Pizza> piz = new List<Pizza>();
            List<Pizza> oblibenyPiz = new List<Pizza>();
            Pizza novaPizza = new Pizza("", "", 0);

            do
            {
                Console.WriteLine("Objednávka pizzy!");
                menu(ref vyber);
                switch (vyber)
                {
                    case 1:
                        novaPizza.Druh = "Salámová";
                        break;
                    case 2:
                        novaPizza.Druh = "Šunková";
                        break;
                    case 3:
                        novaPizza.Druh = "Ananasová";
                        break;
                    case 4:
                        novaPizza.Druh = "Kukuřicová";
                        break;
                }
                Console.WriteLine("Chcete si přidat extra ? Y/N");
                rozhod(ref rozhodnuti, ref finRozhodnuti);

                if (finRozhodnuti == "y")
                {
                    do
                    {
                        menu(ref vyber);
                        switch (vyber)
                        {
                            case 1:
                                pridavky += "Salámová, ";
                                break;
                            case 2:
                                pridavky += "Šunková, ";
                                break;
                            case 3:
                                pridavky += "Ananasová, ";
                                break;
                            case 4:
                                pridavky += "Kukuřicová, ";
                                break;
                        }
                        Console.WriteLine("Chcete si přidat ještě ? Y/N");
                        rozhod(ref rozhodnuti, ref finRozhodnuti);

                        if (finRozhodnuti == "y")
                        {
                            pridatJid = true;
                        }
                        else if (finRozhodnuti == "n")
                        {
                            pridatJid = false;
                            novaPizza.Pocitadlo = pocitadlo;
                            pocitadlo++;
                            piz.Add(new Pizza(novaPizza.Druh, novaPizza.Pridavky = pridavky, novaPizza.Pocitadlo));
                            pridavky = "";
                        }
                    } while (pridatJid);
                }

                else if (finRozhodnuti == "n")
                {
                    novaPizza.Pocitadlo = pocitadlo;
                    pocitadlo++;
                    piz.Add(new Pizza(novaPizza.Druh, novaPizza.Pridavky = pridavky, novaPizza.Pocitadlo));
                }

                Console.WriteLine("Chceš si objednat další Pizzu ? : Y/N");
                rozhod(ref rozhodnuti, ref finRozhodnuti);

                if (finRozhodnuti == "y")
                {
                    objednatPiz = true;
                }
                else if (finRozhodnuti == "n")
                {
                    objednatPiz = false;
                }
            } while (objednatPiz);

            Console.Clear();
            do
            {
                Console.WriteLine("Chceš si přidat objednávku do oblíbených? Y/N");
                rozhod(ref rozhodnuti, ref finRozhodnuti);
                if (finRozhodnuti == "y")
                {
                    foreach (Pizza lol in piz)
                    {
                        Console.WriteLine("{0}.        Druh: {1}       Přídavky: {2}", lol.Pocitadlo, lol.Druh, lol.Pridavky);
                    }
                    Console.WriteLine("Jakou položku jsi chete uložit do oblíbených ?: ");
                    vyber = int.Parse(Console.ReadLine()!);
                    Pizza editPizza = piz.Find(piz => piz.Pocitadlo == vyber)!;
                    oblibenyPiz.Add(new Pizza(editPizza.Druh, editPizza.Pridavky, editPizza.Pocitadlo));
                    oblibenaPizza = true;
                }
                else if (finRozhodnuti == "n")
                {
                    oblibenaPizza = false;
                }
            } while (oblibenaPizza);

            string ukladani = @"Pizza.txt";
            try
            {
                using (StreamWriter zapis = new StreamWriter(ukladani))
                {
                    zapis.WriteLine("Objednávka");
                    foreach (Pizza lol in piz)
                    {
                        zapis.WriteLine("{0}.        Druh: {1}       Přídavky: {2}", lol.Pocitadlo, lol.Druh, lol.Pridavky);
                    }
                    zapis.WriteLine();
                    zapis.WriteLine("Oblíbené položky");
                    foreach (Pizza lol2 in oblibenyPiz)
                    {
                        zapis.WriteLine("{0}.        Dr   h: {1}       Přídavky: {2}", lol2.Pocitadlo, lol2.Druh, lol2.Pridavky);
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

            Console.Clear();
            string precist = File.ReadAllText(ukladani);
            Console.WriteLine(precist);
            Console.ReadKey();
        }
    }
}
