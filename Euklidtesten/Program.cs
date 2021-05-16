using System;

namespace Euklidtesten
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Größter gemeinsamer Teiler nach Euklid Implementieren Sie ein Programm zur Berechnung des größten gemeinsamen Teilers 
                //zweier Zahlen.Beide Zahlen werden über die Konsole eingegeben.
                //Verwenden Sie den Algorithmus von Euklid: 
                //Ziehen Sie von der größeren Zahl die jeweils kleinere Zahl ab, solange bis beide Zahlen gleich sind.
                Console.WriteLine("Erste Zahl eingeben:");
                int z1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Zweite Zahl eingeben:");
                int z2 = Convert.ToInt32(Console.ReadLine());
                bool ergebnis = Euklid(z1, z2);
                Console.WriteLine("Ergebnis ist: " + ergebnis);

            }
        }

        private static bool Euklid(int z1, int z2)
        {
            bool erg = false;
            int ergebnis;
            while (z1 != z2)
            {
                if (z1 > z2)
                {
                    z1 -= z2;
                    ergebnis = z1;
                }
                else if (z2 > z1)
                {
                    z2 -= z1;
                    ergebnis = z2;
                }
            }
            if (z1 == z2)
            {
                ergebnis = z1;
                if (ergebnis == 1)
                {
                    erg = true;
                    Console.WriteLine("Größrer gemeinsamer Teiler ist: " + ergebnis);
                }
                else
                {
                    Console.WriteLine("Größrer gemeinsamer Teiler ist: " + ergebnis);
                }
            }
            return erg;
        }
    }
}
