using System;
using System.Security.Cryptography;
using System.Text;

namespace md5
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                try
                {
                    int auswahl = InOut.ShowMenu("Symmetrisch Verschlüsseln", "Asymmetrisch Verschlüsseln", "Hash berechnen", "Signieren");
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
                            Console.WriteLine("Noch nicht definiert");
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
                            Caesar.Verschlüsseln(geheim, key2);
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
                int choice2 = InOut.ShowMenu("Schlüssel generieren", "Verschlüsseln", "Entschlüsseln", "Zurück");
                switch (choice2)
                {
                    case 1:
                        RSA.Keygen();
                        break;
                    case 2:
                        Console.Write("Klartext: ");
                        string m = Console.ReadLine();
                        Console.Write("e eingeben: ");
                        string e = Console.ReadLine();
                        Console.Write("N eingeben: ");
                        string N = Console.ReadLine();
                        RSA.Encrypt(m, e, N);
                        break;
                    case 3:
                        Console.Write("Geheimtext eingeben: ");
                        string secret = Console.ReadLine();
                        Console.Write("d eingeben: ");
                        string d = Console.ReadLine();
                        Console.Write("N eingeben: ");
                        string N2 = Console.ReadLine();
                        RSA.Decrypt(secret, d, N2);
                        break;
                    case 4:
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
            while (true)
            {
                throw new NotImplementedException();
            }        
        }
    }
}
