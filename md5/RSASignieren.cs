using System;
using System.Numerics;
using System.Text;

namespace md5
{
    internal class RSASignieren
    {
        internal static void Signieren()
        {
            // s = m^d mod N
            Console.WriteLine("Nachricht eingeben: ");
            string m = Console.ReadLine();
            Console.WriteLine("Privaten schlüssel (d) eingeben: ");
            string d = Console.ReadLine();
            Console.WriteLine("N eingeben: ");
            string N = Console.ReadLine();

            Console.WriteLine(Sign(m,d,N));
        }

        private static string Sign(string m, string d, string N)
        {
            string _m = InAsciiUmwandeln(m);
            var basis = BigInteger.Parse(_m);
            var expo = BigInteger.Parse(d);
            var mod = BigInteger.Parse(N);
            var result = BigInteger.ModPow(basis, expo, mod);
            string sig = result.ToString();
            sig = m + ":" + sig;
            return sig;
        }
        private static string InAsciiUmwandeln(string plain)
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
            return asciistring;
        }

        internal static void Verifizieren()
        {
            // m = s^e mod N
            Console.WriteLine("Signierte Nachricht eingeben:");
            string s = Console.ReadLine();
            Console.WriteLine("Öffentlichen Schlüssel eingeben:");
            string e = Console.ReadLine();
            Console.WriteLine("N eingeben:");
            string N = Console.ReadLine();

            Console.WriteLine("Verifikation: " + Verify(s, e, N));

        }

        private static bool Verify(string s, string e, string N)
        {
            //NAchricht und Signatur teilen
            string[] teiler = s.Split(':');
            Console.WriteLine("Nachricht: " + teiler[0]);
            Console.WriteLine("Signatur: " + teiler[1]);



            //Nachricht von Plaintext ins Ascii umwandeln
            string _m = InAsciiUmwandeln(teiler[0]);
            Console.WriteLine("Nachricht als Ascii: " + _m);

            //Signatur rückrechnen:
            var basis = BigInteger.Parse(teiler[1]);
            var expo = BigInteger.Parse(e);
            var mod = BigInteger.Parse(N);
            var result = BigInteger.ModPow(basis, expo, mod);
            Console.WriteLine("Berechnete Signatur: " + result);

            return true;
        }
    }
}