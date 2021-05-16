using System;

namespace md5
{
    internal class InOut
    {
        internal static int ShowMenu(params object[] list)
        {
            try
            {
                Console.WriteLine("\n ++++ MENÜ ++++");
                int zähler = 1;
                foreach (string a in list)
                {
                    Console.WriteLine(zähler + ". " + a);
                    zähler++;
                }
                Console.Write("Ihre Wahl: ");
                int wahl = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine("Ihre Wahl: " + wahl);
                return wahl;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error InOut" + e.Message.ToString());
                return 0;
            }
        }
    }
}