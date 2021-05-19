using System;
using System.Collections.Generic;
using System.Text;

namespace md5
{
    class Caesar
    {
        static char[] basisarray = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] basisarraygross = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        //METHODEN:
        //Methode Verschlüsseln
        internal static void Verschlüsseln(string _geheim, int _key)
        {
            //TODO: Text verschlüseln und verschlüsselten Text auf der Konsole ausgeben
            char[] chararray = _geheim.ToCharArray();

            //Zeichen umwandeln
            for (int i = 0; i < chararray.Length; i++)
            {
                for (int ii = 0; ii < basisarray.Length; ii++)
                {
                    if (chararray[i] == basisarray[ii]) // Wenn kleinbuchstabe
                    {
                        int newkey = ii + _key;
                        if (newkey >= basisarray.Length)
                        {
                            newkey -= basisarray.Length;
                            chararray[i] = basisarray[newkey];
                            break;
                        }
                        else if (newkey < basisarray.Length)
                        {
                            chararray[i] = basisarray[newkey];
                            break;
                        }
                    }
                    else if (chararray[i] == basisarraygross[ii]) // Wenn Großbuchstabe
                    {
                        int newkey = ii + _key;
                        if (newkey >= basisarraygross.Length)
                        {
                            newkey -= basisarraygross.Length;
                            chararray[i] = basisarraygross[newkey];
                            break;
                        }
                        else if (newkey < basisarraygross.Length)
                        {
                            chararray[i] = basisarraygross[newkey];
                            break;
                        }
                    }
                }
            }

            //Zeichen ausgeben
            TextDrucken(chararray, _key);
        }

        internal static bool KeyPrüfen(int key)
        {
            if (key > 0 && key < 26)
            {
                return true;
            }
            else
                return false;
        }

        // Methode Entschlüsseln
        internal static void Entschlüsseln(string verschl, int key2)
        {
            //TODO: Verschlüsselten Text entschlüsseln und auf der Konsole ausgeben 
            char[] chararray = verschl.ToCharArray();

            //Zeichen umwandeln
            for (int i = 0; i < chararray.Length; i++)
            {
                for (int ii = 0; ii < basisarray.Length; ii++)
                {
                    if (chararray[i] == basisarray[ii]) // Wenn Kleinbuchstabe
                    {
                        int newkey = ii - key2;
                        if (newkey < 0)
                        {
                            newkey = basisarray.Length - (newkey * (-1));
                            chararray[i] = basisarray[newkey];
                            break;
                        }
                        else if (newkey >= 0)
                        {
                            chararray[i] = basisarray[newkey];
                            break;
                        }
                    }
                    else if (chararray[i] == basisarraygross[ii]) // Wenn Grossbuchstabe
                    {
                        int newkey = ii - key2;
                        if (newkey < 0)
                        {
                            newkey = basisarraygross.Length - (newkey * (-1));
                            chararray[i] = basisarraygross[newkey];
                            break;
                        }
                        else if (newkey >= 0)
                        {
                            chararray[i] = basisarraygross[newkey];
                            break;
                        }
                    }
                }
            }

            //Zeichen ausgeben
            TextDrucken(chararray, key2);
        }
        // Methode Brute Force
        internal static void BruteForce(string brutef)
        {
            for (int i = 1; i <= 26; i++)
            {
                Entschlüsseln(brutef, i);
            }
        }
        // TEXT ausgeben
        private static void TextDrucken(char[] chararray, int _key)
        {
            Console.WriteLine("Verwendeter Key: " + _key);
            Console.Write("Text:\t");
            foreach (char a in chararray)
            {
                Console.Write(a);
            }
            Console.WriteLine("\n");
        }
    }
}
