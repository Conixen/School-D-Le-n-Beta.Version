namespace School_Dé_León_Beta.Version
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till School Dè Léon Applikationen!" +
                "\nBeta Version 2.3");
            Thread.Sleep(5000);
            Console.Clear();

            while (true) 
            {
                Console.WriteLine("School Dè Léon Beta.V.2.3");
                Console.WriteLine("Välkommen till Huvudmenyn" +
                "\nVälj ett av följande alternativ...(Tryck in respektive siffra)");
                Console.WriteLine("------------------------------------------------------");

                Console.WriteLine("1. Visa alla elever");
                Console.WriteLine("2. Hämta elev info");
                Console.WriteLine("3. Ny perosnal");
                Console.WriteLine("4. Visa all personal");//namn, beffatning,hur länge de jobbat, 
                Console.WriteLine("5. $$$");
                Console.WriteLine("6. Kurser/Ämnen");
                Console.WriteLine("7. Sätt betyg på elev");
                Console.WriteLine("0. Avsluta program");

                int menyChoice = Convert.ToInt32(Console.ReadLine());

                switch (menyChoice) 
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6:

                        break;

                    case 7:

                        break;

                    case 0:
                        Console.WriteLine("STänger ner programmet" +
                            "\nLämmna en review på Yelp");
                        return;
                        

                    default:
                        Console.WriteLine("Något blev fel i menyval");
                        break;

                }
            }

        }
    }
}
