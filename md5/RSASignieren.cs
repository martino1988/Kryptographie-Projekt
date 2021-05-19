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
            Console.WriteLine("(Alice) Nachricht eingeben");
            string message = Console.ReadLine();

            Console.WriteLine("(Alice) Public Key von Bob eingeben:");
            string pubKBob = Console.ReadLine();
            string[] keyPaarBobPublic = pubKBob.Split(':');
            string eBob = keyPaarBobPublic[0];
            string nBob = keyPaarBobPublic[1];

            Console.WriteLine("(Alice) Deinen Private Key eingeben:");
            string privKAlice = Console.ReadLine();
            string[] keyPaarAlicePrivate = privKAlice.Split(':');
            string dAlice = keyPaarAlicePrivate[0];
            string nAlice = keyPaarAlicePrivate[1];

            Console.WriteLine("\n+++ Verschlüsselte Nachricht +++");
            RSA.Encrypt(message, eBob, nBob);
            Console.WriteLine("\n+++ Signatur +++");
            RSA.Encrypt(message, dAlice, nAlice);

        }

        internal static void Verifizieren()
        {
            // m = s^e mod N
            Console.WriteLine("\nSignatur verifizieren");
            Console.WriteLine("(Bob) Verschlüsselte Nachricht eingeben:");
            string cipher = Console.ReadLine();
            Console.WriteLine("(Bob) Signatur eingeben:");
            string signature = Console.ReadLine();

            Console.WriteLine("(Bob) Gib deinen private Key ein:");
            string privateKBob = Console.ReadLine();
            string[] keyPaarBobPrivate = privateKBob.Split(':');
            string dBob = keyPaarBobPrivate[0];
            string nBob = keyPaarBobPrivate[1];

            Console.WriteLine("(Bob) Gib den public Key von Alice ein:");
            string pubKAlice = Console.ReadLine();
            string[] keyPaarAlicePublic = pubKAlice.Split(':');
            string eAlice = keyPaarAlicePublic[0];
            string nAlice = keyPaarAlicePublic[1];

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nEntschlüsseln");
            Console.ResetColor();
            RSA.Decrypt(cipher, dBob, nBob);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nSignatur verifizieren");
            Console.ResetColor();
            RSA.Decrypt(signature, eAlice, nAlice);
        }
    }
}