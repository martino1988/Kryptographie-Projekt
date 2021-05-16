using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace md5
{
    class RSA
    {
        public static void Keygen()
        {
            Console.WriteLine("Eine hohe Zahl (Integer) eingeben");
            int n = Convert.ToInt32(Console.ReadLine());

            //2 Primzahlen ermitteln
            int[] primeArray = new int[2];
            primeArray = Get2Primes(n);
            //foreach (var a in primeArray)
            //{
            //    Console.WriteLine("Primzahl: " + a);
            //}

            //N berechnen = p x q
            int N = NBerechnen(primeArray[0], primeArray[1]);
            //Console.WriteLine("N: " + N);

            //Phi berechnen
            int phiN = PhiVonN(primeArray[0], primeArray[1]);
            //Console.WriteLine("Phi(n): " + phiN);

            //b berechnen ggt(b, phi(n))
            int zz = BerechneZufallszahl(phiN);
            //Console.WriteLine("Teilerfremde Zufallszahl: " + zz);

            //inverses b^-1 berechnen
            int[] erg = ErweiterterEuklid(phiN, zz);

            //for (int i = 0; i < erg.Length; i++)
            //{
            //    Console.WriteLine(i + " " + erg[i]);
            //}
            int e = zz;
            int d = Modulo(erg[2], phiN);

            Console.WriteLine("\n\n+++++ Public Key +++++");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("e: {0}\nN: {1}", e, N);
            Console.ResetColor();
            Console.WriteLine("+++++ Public Key +++++\n");
            Console.WriteLine("\n+++++ Private Key +++++");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("d: {0}\nN: {1}", d, N);
            Console.ResetColor();
            Console.WriteLine("+++++ Private Key +++++\n\n");

        }

        private static int BerechneZufallszahl(int phiN)
        {
            Random r = new Random();
            int rtrn = 0;
            bool checker = false;

            while(checker == false)
            {
                int zufallszahl = r.Next(2, (phiN - 1));
                //Console.WriteLine("zufallszahl: " + zufallszahl);
                if (Euklid(phiN,zufallszahl)==true)
                {
                    //Console.WriteLine("teilerfremde Zufallszahl ist: " + zufallszahl);
                    rtrn = zufallszahl;
                    checker = true;
                }
            }
            return rtrn;
        }

        private static int Modulo(int zz, int phi)
        {
            int r = zz % phi;
            return r < 0 ? r + phi : r;
        }
        private static double ModuloDouble(double zz, double phi)
        {
            double b = zz;
            double a = phi;
            double mod;
            // Handling negative values
            if (a < 0)
                mod = -a;
            else
                mod = a;
            if (b < 0)
                b = -b;

            // Finding mod by repeated subtraction

            while (mod >= b)
                mod = mod - b;

            // Sign of result typically depends
            // on sign of a.
            if (a < 0)
                return -mod;

            return mod;
        }

        private static int[] ErweiterterEuklid(int phiN, int zufall)
        {
            int[] rtrn = new int[3];

            if (zufall == 0)
            {
                rtrn[0] = phiN;
                rtrn[1] = 1;
                rtrn[2] = 0;
                return rtrn;
            }
            else
            {
                int[] vals = ErweiterterEuklid(zufall, phiN % zufall);
                int q = phiN / zufall;
                rtrn[0] = vals[0];
                rtrn[1] = vals[2];
                rtrn[2] = vals[1] - q * vals[2];
                return rtrn;
            }
        }

        private static bool Euklid(int phi, int zz)
        {
            bool erg = false;
            int ergebnis;
            while (phi != zz)
            {
                if (phi > zz)
                {
                    phi -= zz;
                    ergebnis = phi;
                }
                else if (zz > phi)
                {
                    zz -= phi;
                    ergebnis = zz;
                }
            }
            if (phi == zz)
            {
                ergebnis = phi;
                if (ergebnis == 1)
                {
                    erg = true;
                    //Console.WriteLine("Größrer gemeinsamer Teiler ist: " + ergebnis);
                }
                else
                {
                    //Console.WriteLine("Größrer gemeinsamer Teiler ist: " + ergebnis);
                }
            }
            return erg;
        }

        internal static int NBerechnen(int prim1, int prim2)
            {
                return prim1 * prim2;
            }
            internal static int PhiVonN(int prim1, int prim2)
            {
                return (prim1 - 1) * (prim2 - 1);
            }
            internal static int[] Get2Primes(int n)
            {
                int prim1 = 19;
                int prim2 = 17;

                //TODO Code 2 Primzahlen
                prim1 = Prime(n);
                prim2 = Prime(prim1 - 1);

                int[] primArr = new int[2];
                primArr[0] = prim1;
                primArr[1] = prim2;

                return primArr;
            }

            internal static int Prime(int z)
            {
                bool peter = false;
                bool check = false;

                while (peter == false)
                {
                    check = false;
                    for (int i = 2; i < z; i++)
                    {
                        if (z % i == 0)
                        {
                            check = true;
                            break;
                        }
                        //do nothing
                    }
                    if (check == true)
                    {
                        z--;
                    }
                    else
                    {
                        peter = true;
                        return z;
                    }

                } //while peter
                return z;
            }


           
            private static double InAsciiUmwandeln(string plain)
            {
                ASCIIEncoding ascii = new ASCIIEncoding();

                // Create an ASCII byte array.
                Byte[] bytes = ascii.GetBytes(plain);

                string asciistring = "";
                foreach (Byte b in bytes)
                {
                    asciistring += b.ToString();
                }
                Console.WriteLine("Ascii als double: "+asciistring);
                return Convert.ToDouble(asciistring);
            }

        public static void Encrypt(string message, string _e, string _N)
        {
            double e = Convert.ToDouble(_e);
            double N = Convert.ToDouble(_N);
            double basis = InAsciiUmwandeln(message);
            Console.WriteLine("Berechne " + basis + "^" + e + " mod " + N);
            double faktor1 = Math.Pow(basis, e);
            Console.WriteLine("Berechne " + faktor1 + " mod " + N);
            double secret = faktor1 % N;
            Console.WriteLine("Verschlüsselter Text (secret): " + secret);
        }


        public static void Decrypt(string _sec, string _d, string _N)
            {
            double e = Convert.ToDouble(_d);
            double N = Convert.ToDouble(_N);
            double basis = Convert.ToDouble(_sec);
            Console.WriteLine("Berechne " + basis + "^" + e + " mod " + N);
            double faktor1 = Math.Pow(basis, e);
            Console.WriteLine("Berechne " + faktor1 + " mod " + N);
            double secret = faktor1 % N;
            Console.WriteLine("Klartext: " + secret);
            }
        }
    }

