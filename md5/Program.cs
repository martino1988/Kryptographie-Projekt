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
                            int choice = InOut.ShowMenu("Verschlüsseln", "Entschlüsseln", "Brute Force");
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
                                default:
                                    throw new Exception("Auswahlfehler");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Noch nicht definiert");
                            break;
                        case 3:
                            Console.WriteLine("Text eingeben:");
                            string plain = Console.ReadLine();
                            Console.WriteLine("+++++ HASH +++++\n" + Hash.StartHash(plain) + "\n+++++ HASH +++++\n");
                            break;
                        case 4:
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
    }
}
