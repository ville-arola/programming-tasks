using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iban_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Syötä BBAN-tilinumero: ");
                string bban = Console.ReadLine();
                if (bban.Length == 0)
                {
                    break;
                }
                bban = cleanBBAN(bban);
                string res = string.Empty;
                if (isValidBBAN(bban))
                {
                    Console.WriteLine("Syöte OK...");
                    string countryCode = "FI";
                    res = bban + substituteLetters(countryCode) + "00";
                    string ibanCheck = string.Format("{0:D2}", 98 - mod(res, 97));
                    res = string.Format("{0}{1}", countryCode, ibanCheck);
                    for (int i = 0; i < bban.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            res += " ";
                        }
                        res += bban.ElementAt(i);
                    }
                    Console.WriteLine("IBAN-tilinumero: {0}", res);
                }
                else
                {
                    Console.WriteLine("Virheellinen syöte.");
                }
            }
        }

        static string cleanBBAN(string bban)
        {
            // Converts letters to corresponding number codes,
            // strips non-numeric characters and 
            // fixes length to 14 numbers
            // with appropriate zero-padding.
            bban = substituteLetters(bban);
            int bbanLen = bban.Length;
            if (bbanLen > 14)
            {
                bbanLen = 14;
                Console.WriteLine("BBAN-tilinumerosta huomioidaan ensimmäiset 14 numeroa.");
            }
            string res = string.Empty,
                   digits = "1234567890";
            for (int i=0; i<bbanLen; i++)
            {
                if ((digits.IndexOf(bban.ElementAt(i)) >= 0))
                {
                    res += bban.ElementAt(i).ToString();
                }
            }
            if (res.Length > 6)
            {
                int padIndex;
                if (res.StartsWith("4") || res.StartsWith("5"))
                {
                    padIndex = 7;
                }
                else
                {
                    padIndex = 6;
                }
                while (res.Length < 14)
                {
                    res = res.Insert(padIndex, "0");
                }
            }
            return res;
        }

        static bool isValidBBAN(string bban)
        {
            // Checks whether input is a valid BBAN number.
            if (bban.Length != 14)
            {
                return false;
            }
            int[] numbers = new int[14];
            int n;
            for (int i=13; i>=0; i--)
            {
                if (int.TryParse(bban.ElementAt(i).ToString(), out n))
                {
                    n = (i + 1) % 2 == 0 ? n : n*2;
                    numbers[i] = n < 10 ? n : n-9;
                }
                else
                {
                    return false;
                }
            }
            return numbers.Sum() % 10 == 0;
        }

        static string substituteLetters(string str)
        {
            // Replaces letters with corresponding number codes.
            str = str.ToUpper();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", res = string.Empty;
            int charIndex;
            for (int i = 0; i < str.Length; i++)
            {
                charIndex = chars.IndexOf(str.ElementAt(i));
                if (charIndex < 0)
                {
                    res += str.ElementAt(i);
                }
                else
                {
                    res += (10+charIndex).ToString();
                }
            }
            return res;
        }

        // Copy-paste from http://stackoverflow.com/questions/5662453/modulo-from-very-large-int-c-sharp
        static int mod(string x, int y)
        {
            if (x.Length == 0)
                return 0;
            string x2 = x.Substring(0, x.Length - 1);
            int x3 = int.Parse(x.Substring(x.Length - 1));
            return (mod(x2, y) * 10 + x3) % y;
        }
    }
}
