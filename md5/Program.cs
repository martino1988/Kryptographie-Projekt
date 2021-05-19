using System;
using System.Security.Cryptography;
using System.Text;

namespace md5
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;

            while (a==1)
            {
                try
                {
                    int auswahl = InOut.ShowMenu("Symmetrisch Verschlüsseln", "Asymmetrisch Verschlüsseln", "Hash berechnen", "Signieren", "Schlüssel erzeugen", "Beenden");
                    switch (auswahl)
                    {
                        case 1:
                            MenueSymmetrisch();
                            break;
                        case 2:
                            MenueAsymmetrisch();
                            break;
                        case 3:
                            MenueHash();
                            break;
                        case 4:
                            MenueSignieren();
                            break;
                        case 5:
                            RSA.Keygen();
                            break;
                        case 6:
                            a = 0;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fehler! " + e.Message);
                }
            }
            
        }

        private static void MenueSymmetrisch()
        {
            int a = 1;
            while (a == 1)
            {
                int choice = InOut.ShowMenu("Verschlüsseln", "Entschlüsseln", "Brute Force", "Zurück");
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Zu verschlüsselnde Nachricht eingeben:");
                        string klartext = Console.ReadLine();
                        Console.WriteLine("Schlüssel zwischen 1 und 25 eingeben:");
                        int key = Convert.ToInt32(Console.ReadLine());
                        bool prüf = Caesar.KeyPrüfen(key);
                        if (prüf)
                            Caesar.Verschlüsseln(klartext, key);
                        else
                            throw new Exception("Schlüssellänge Falsch!");
                        break;
                    case 2:
                        Console.WriteLine("Verschlüsselten Text eigeben:");
                        string geheim = Console.ReadLine();
                        Console.WriteLine("Schlüssel zwischen 1 und 25 eingeben:");
                        int key2 = Convert.ToInt32(Console.ReadLine());
                        bool prüf2 = Caesar.KeyPrüfen(key2);
                        if (prüf2)
                            Caesar.Entschlüsseln(geheim, key2);
                        else
                            throw new Exception("Schlüssellänge Falsch!");
                        break;
                    case 3:
                        Console.WriteLine("Verschlüsselten Text für Brute Force eingeben:");
                        string brutef = Console.ReadLine();
                        Caesar.BruteForce(brutef);
                        break;
                    case 4:
                        a = 0;
                        break;
                    default:
                        throw new Exception("Auswahlfehler");
                } 
            }
        }


        private static void MenueAsymmetrisch()
        {
            int a = 1;
            while (a == 1)
            {
                int choice2 = InOut.ShowMenu("Verschlüsseln", "Entschlüsseln", "Zurück");
                switch (choice2)
                {
                    
                    case 1:
                        Console.Write("Klartext: ");
                        string m = Console.ReadLine();
                        Console.Write("Public Key eingeben: ");
                        string pbk= Console.ReadLine();
                        string[] keypaarpublic = pbk.Split(':');
                        string e = keypaarpublic[0];
                        string N = keypaarpublic[1];
                        RSA.Encrypt(m, e, N);
                        break;
                    case 2:
                        Console.Write("Geheimtext eingeben: ");
                        string secret = Console.ReadLine();
                        Console.Write("Private Key eingeben: ");
                        string pvk = Console.ReadLine();
                        string[] keypaarprivate = pvk.Split(':');
                        string d = keypaarprivate[0];
                        string N2 = keypaarprivate[1]; ;
                        RSA.Decrypt(secret, d, N2);
                        break;
                    case 3:
                        a = 0;
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                } 
            }
        }



        private static void MenueHash()
        {
            int a = 1;
            while (a == 1)
            {
                int choice = InOut.ShowMenu("Hash berechnen", "Zurück");
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Text eingeben:");
                        string plain = Console.ReadLine();
                        Console.WriteLine("+++++ HASH +++++\n" + Hash.StartHash(plain) + "\n+++++ HASH +++++\n");
                        break;
                    case 2:
                        a = 0;
                        break;
                    default:
                        break;
                }
            }
        }
        private static void MenueSignieren()
        {
            int a = 1;
            while (a == 1)
            {
                int choice = InOut.ShowMenu("Keys erzeugen", "Signieren", "Verifizieren", "Zurück");
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Schlüsselpaar Sender (Alice):");
                        RSA.Keygen();
                        Console.WriteLine("Schlüsselpaar Empfänger (Bob):");
                        RSA.Keygen();
                        break;
                    case 2:
                        RSASignieren.Signieren();
                        break;
                    case 3:
                        RSASignieren.Verifizieren();
                        break;
                    case 4:
                        a = 0;
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
