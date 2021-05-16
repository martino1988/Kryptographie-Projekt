using System;

namespace ZumTesten
{
    class Program
    {
        static void Main(string[] args)
        {
            int phi = 1680;
            int zufall = 1679;

            int[] erg = ErweiterterEuklid(phi,zufall);

            int ergebnis = Modulo(erg[2], phi);

            Console.WriteLine("Phi(n): " + phi + "\nteilerfremde Zufallszahl: " + zufall);
            Console.WriteLine("Inverses: " + erg[2] + "\nErgebnis: " + ergebnis);
        }

        private static int Modulo(int zz, int phi)
        {
            int r = zz % phi;
            return r < 0 ? r + phi : r;
        }

        private static int[] ErweiterterEuklid(int a, int b)
        {
            int[] rtrn = new int[3];

            if (b == 0)
            {
                rtrn[0] = a;
                rtrn[1] = 1;
                rtrn[2] = 0;
                return rtrn;
            }
            else
            {
                int[] vals = ErweiterterEuklid(b, a % b);
                int q = a / b;
                rtrn[0] = vals[0];
                rtrn[1] = vals[2];
                rtrn[2] = vals[1]-q*vals[2];
                return rtrn;
            }
        }
    }
}
