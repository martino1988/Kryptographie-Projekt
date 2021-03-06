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
            //2 Primzahlen ermitteln
            int[] primeArray = new int[2];
            primeArray = Get2Primes();

            //N berechnen = p x q
            int N = NBerechnen(primeArray[0], primeArray[1]);

            //Phi berechnen
            int phiN = PhiVonN(primeArray[0], primeArray[1]);

            //b berechnen ggt(b, phi(n))
            int zz = BerechneZufallszahl(phiN);

            //inverses b^-1 berechnen
            int[] erg = ErweiterterEuklid(phiN, zz);

            int d = zz;
            int e = Modulo(erg[2], phiN);


            Console.WriteLine("\n+++++ Public Key +++++");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}:{1}", e, N);
            Console.ResetColor();
            Console.WriteLine("+++++ Public Key +++++");
            Console.WriteLine("\n+++++ Private Key +++++");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}:{1}", d, N);
            Console.ResetColor();
            Console.WriteLine("+++++ Private Key +++++\n");
        }

        private static int BerechneZufallszahl(int phiN)
        {
            Random r = new Random();
            int rtrn = 0;
            bool checker = false;

            //for (int i = 2; i < phiN; i++)
            //{
            //    if (Euklid(phiN, i)==true)
            //    {
            //        rtrn = i;
            //        break;
            //    }
            //}

            while (checker == false)
            {
                int zufallszahl = r.Next(2, (phiN - 1));
                //Console.WriteLine("zufallszahl: " + zufallszahl);
                if (Euklid(phiN, zufallszahl) == true)
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
            internal static int[] Get2Primes()
            {
                int prim1 = 7;
                int prim2 = 23;
                bool check = false;
                //TODO Code 2 Primzahlen
                prim1 = Prime();
                while (check == false)
                {
                    prim2 = Prime();
                    if (prim2 >= prim1)
                    {
                        check = true;
                    }

                }

            int[] primArr = new int[2];
                primArr[0] = prim1;
                primArr[1] = prim2;

                return primArr;
            }

            internal static int Prime()
            {
            var rand = new Random();
            int n = 0, i, m = 0, flag = 0;
            bool peter = false;


                while (peter == false)
                {
                    n = rand.Next(900, 9990);
                    m = n / 2;

                    for (i = 2; i <= m; i++)
                    {
                        flag = 0;
                        if (n % i == 0)
                        {
                            flag = 1;


                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        peter = true;

                    }


                }
                return n;

            }




        public static double InAsciiUmwandeln(string plain)
            {
                ASCIIEncoding ascii = new ASCIIEncoding();

                // Create an ASCII byte array.
                Byte[] bytes = ascii.GetBytes(plain);

                string asciistring = "";
                foreach (Byte b in bytes)
                {
                    asciistring += b.ToString();
                }
                //Console.WriteLine("Ascii als double: "+asciistring);
                return Convert.ToDouble(asciistring);
            }
            

        public static void Encrypt(string message, string _e, string _N)
        {
            string chars = message;
            char[] letters = chars.ToCharArray();
            //Console.WriteLine("Array: " + chars[0] + " " + chars[1]);
            string[] plaintextArray = new string[letters.Length];
            string[] ciphertextArray = new string[letters.Length];
            int z = 0;

            // TEST
                int e = Convert.ToInt32(_e);
                var ee = BigInteger.Parse(_e);
                double N = Convert.ToDouble(_N);
                var NN = BigInteger.Parse(_N);

            //TEST

            //Methode 1: als mehrere Nummernblöcke darstellen
            foreach (char item in letters)
            {
                double x = InAsciiUmwandeln(item.ToString());
                string y = x.ToString();
                plaintextArray[z] = y;
                var bi = BigInteger.Parse(y);
                var result = BigInteger.ModPow(bi, e, NN);
                ciphertextArray[z] = result.ToString();
                
                //Console.WriteLine("PlaintextArray Stelle " + z + ": " + plaintextArray[z]);
                z++;
            }

            //Console.WriteLine("Geheimtext: ");
            foreach (string item in ciphertextArray)
            {
                Console.Write(item + " ");
                //Console.Write(item);
            }
            Console.WriteLine();
            //Console.WriteLine("Ciphertext Array: " + ciphertextArray[0] + " " + ciphertextArray[1] + " " + ciphertextArray[2]);
            


            /*
            double[] cipherNumbersArray = new double[cipherArray.Length];
            int counterNumber = 0;
            foreach (string item in cipherArray)
            {
                cipherNumbersArray[counterNumber] = Convert.ToDouble(item);
                Console.WriteLine(cipherNumbersArray[counterNumber]);
                counterNumber++;
            }

            string mess = InAsciiUmwandeln(message).ToString();
            Console.WriteLine("Klartext: " + mess);
            */


            /*
            var msg = BigInteger.Parse(mess);
            Console.WriteLine(msg);
            var NN = BigInteger.Parse(_N);
            int eee = Convert.ToInt32(_e);
            var erg2 = BigInteger.ModPow(msg, eee, NN);
            Console.WriteLine("Geheimtext: " + erg2);
            */



            //double e = Convert.ToDouble(_e);
            //double N = Convert.ToDouble(_N);
            //double basis = InAsciiUmwandeln(message);
            //Console.WriteLine("Sectret: " + basis + "^" + e + " mod " + N);
            //double faktor1 = Math.Pow(basis, e);
            ////Console.WriteLine("message ^ e: " + faktor1);
            //double secret = (faktor1 % N);
            //Console.WriteLine("Sectret berechnet: " + secret);
        }


        public static void Decrypt(string _sec, string _d, string _N)
           {
            //string mess = InAsciiUmwandeln(_sec).ToString();

            var NN = BigInteger.Parse(_N);
            int dd = Convert.ToInt32(_d);

            ////Methode 2: Tectblöcke trennen
            //Console.WriteLine("METHODE 2");
            //char[] cipherchars = _sec.ToCharArray();
            //List<string> ciphertexte = new List<string>(); 
            //for (int i = 0; i < cipherchars.Length-7; i+=7)
            //{
            //    string cipher = "";
            //    cipher += cipherchars[i].ToString();
            //    cipher += cipherchars[i+1].ToString();
            //    cipher += cipherchars[i+2].ToString();
            //    cipher += cipherchars[i+3].ToString();
            //    cipher += cipherchars[i+4].ToString();
            //    cipher += cipherchars[i+5].ToString();
            //    cipher += cipherchars[i+6].ToString();
            //    ciphertexte.Add(cipher);
            //    cipher = "";
            //}
            //List<string> plainTextList = new List<string>();
            //foreach (string a in ciphertexte)
            //{
            //    var secr = BigInteger.Parse(a);
            //    var result = BigInteger.ModPow(secr, dd, NN);
            //    plainTextList.Add(result.ToString());
            //}
            //foreach (string item in plainTextList)
            //{
            //    Console.Write(item + " ");
            //}
            //ENDE METHODE 2



            //Methode 1: Mehrere Blöcke entschlüsseln
            //Console.WriteLine("METHODE 1");
            string[] cipherTextArray = _sec.Split(" ");
            string[] plainTextArray = new string[cipherTextArray.Length];
            string[] plainText = new string[cipherTextArray.Length];
            int z = 0;

            foreach (string item in cipherTextArray)
            {
                var secr = BigInteger.Parse(item);
                var result = BigInteger.ModPow(secr, dd, NN);
                plainTextArray[z] = result.ToString();

                //Console.WriteLine("PlaintextArray Stelle " + z + ": " + plaintextArray[z]);
                z++;
            }
            z = 0;
            //Console.WriteLine("Klartext (ASCII): ");
            foreach (string item in plainTextArray)
            {
                //Console.Write(item + " ");
                int unicode = Convert.ToInt32(item);
                char character = (char)unicode;
                string text = character.ToString();
                plainText[z] = text;
                z++;

            }
            Console.WriteLine("\nKlartext: ");
            foreach (string item in plainText)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            /*var secr = BigInteger.Parse(_sec);
            var NN = BigInteger.Parse(_N);
            int dd = Convert.ToInt32(_d);
            Console.WriteLine("{0} ^ {1} % {2}", secr, dd, NN);
            var erg2 = BigInteger.ModPow(secr, dd, NN);

            Console.WriteLine("Klartext: " + erg2);
            */


            //double e = Convert.ToDouble(_d);
            //double N = Convert.ToDouble(_N);
            //double basis = Convert.ToDouble(_sec);
            //Console.WriteLine("Klartext: " + basis + "^" + e + " mod " + N);
            //double faktor1 = Math.Pow(basis, e);
            //double secret = faktor1 % N;
            //Console.WriteLine("Klartext berechnet: " + secret);
        }
        }
    }

