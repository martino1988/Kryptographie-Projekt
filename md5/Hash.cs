using System;
using System.Collections.Generic;
using System.Text;

namespace md5
{
    class Hash
    {
        private const int BLOCKLÄNGE = 16;

        internal static string StartHash(string plain)
        {
            //TODO Ascii von plain erstellen
            string asciiINT = InAsciiUmwandeln(plain);

            //TODO ascii auf correcte länge bringen
            string asciiKorrekt = AufLaengeAnpassen(asciiINT);

            //TODO IV Bestimmen (anhand vom Ascii)
            string iv = DenIVBestimmen(plain);

            //TODO ascii in blöcke aufteilen (Liste)
            List<string> blockListe = InBloeckeAufteilen(asciiKorrekt);

            //TODO Blöcke und IV verrechnen
            string hash1 = HashBerechnen(blockListe, iv);

            //TODO in hexa umwandeln
            string hash2 = InHexaUmwandeln(hash1);

            return hash2;
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
            return asciistring;
        }



        private static string DenIVBestimmen(string plain)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();

            // Create an ASCII byte array.
            Byte[] bytes = ascii.GetBytes(plain);

            Byte[] inivektor = new Byte[bytes.Length];
            int zähler = 0;
            for (int i = bytes.Length-1; i >=0 ; i--)
            {
                inivektor[zähler] = bytes[i];
                zähler++;
            }
            string IVString = "";
            foreach(Byte b in inivektor)
            {
                IVString += b.ToString();
            }
            IVString = AufLaengeAnpassen(IVString);
            IVString = AufBlocklaengeAbschneiden(IVString, BLOCKLÄNGE);
            return IVString;
        }

        private static string AufBlocklaengeAbschneiden(string input, int N)
        {
            char[] charArr = input.ToCharArray();
            char[] korrekteLänge = new char[N];
            string retour = "";

            for (int i = 0; i < N; i++)
            {
                korrekteLänge[i] = charArr[i];
            }
            foreach (char c in korrekteLänge)
            {
                retour += c.ToString();
            }
            return retour;
        }

        private static string AufLaengeAnpassen(string inputstring)
        {
            bool checker = true;
            while (inputstring.Length % BLOCKLÄNGE != 0)
            {
                if (checker == true)
                {
                    inputstring = inputstring + "0";
                    checker = false;
                }
                else if (checker == false)
                {
                    inputstring = inputstring + "1";
                    checker = true;
                }
            }
            return inputstring;
        }



        private static List<string> InBloeckeAufteilen(string asciiKorrekt)
        {
            List<string> neueListe = new List<string>();

            string text = asciiKorrekt;
            int stringLength = text.Length;
            int blockgroesse = BLOCKLÄNGE;

            for (int i = 0; i < stringLength; i += blockgroesse)
            {
                if ((i + blockgroesse) > stringLength)
                {
                    blockgroesse = stringLength - i;
                }
                string neu = text.Substring(i, blockgroesse);
                neueListe.Add(neu);
            }
            return neueListe;
        }



        private static string HashBerechnen(List<string> blockListe, string iv)
        {
            char[] blockArray;// = blockListe[0].ToCharArray();
            char[] ivArray = iv.ToCharArray();

            for (int i = 0; i < blockListe.Count; i++)
            {
                blockArray = blockListe[i].ToCharArray();
                ivArray = ArrayVermischen(ivArray, blockArray);
            }

            string hash = "";
            foreach (char c in ivArray)
            {
                hash += c.ToString();
            }
            return hash;
        }

        private static char[] ArrayVermischen(char[] ivArray, char[] blockArray)
        {
            string zwischen = "";
            for (int i = 0; i < ivArray.Length; i++)
            {
                zwischen += CharacterVerrechen(ivArray[i], blockArray[i]);
            }
            char[] retourArray = zwischen.ToCharArray();
            return retourArray;
        }

        private static string CharacterVerrechen(char v1, char v2)
        {
            int zahl = v1 + v2;

            while (zahl >= 10)
            {
                zahl = QuersummeBerechnen(zahl);
            }
            return zahl.ToString();
        }

        private static int QuersummeBerechnen(int zahl)
        {
            int i = zahl;
            int result = 0;
            if (i > 0)
            {
                while (i > 0)
                {
                    result += i % 10;
                    i /= 10;
                }
            }
            return result;
        }

        private static string InHexaUmwandeln(string hash1)
        {
            double d = Convert.ToDouble(hash1);
            byte[] bytes = BitConverter.GetBytes(d);
            return BitConverter.ToString(bytes);
        }
    }
}
