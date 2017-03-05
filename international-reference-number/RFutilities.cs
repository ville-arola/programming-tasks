using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace international_reference_number
{
    public static class RFutilities
    {
        public static bool ValidateRFnumber(List<char> rf)
        {
            string tmp = string.Empty;
            foreach (char i in rf)
            {
                tmp += i;
            }
            tmp = tmp.Substring(4) + tmp.Substring(0,4);
            string rfString = string.Empty;
            for (int i = 0; i < tmp.Length; i++)
            {
                rfString += GetCharCode(tmp.ElementAt(i));
            }
            return Mod(rfString, 97) == 1;
        }

        public static List<char> GenerateRF(string input)
        {
            string cleanInput = SanitizeRFinput(input);
            if (cleanInput.Length == 0)
            {
                Console.WriteLine("Syötteestä ei voida muodostaa RF-viitettä.");
                return new List<char>();
            }
            int cutOff = cleanInput.Length < 22 ? cleanInput.Length : 21;
            if (cutOff < cleanInput.Length)
            {
                Console.WriteLine("Syöte on liian pitkä. Vain ensimmäiset 21 merkkiä huomioidaan.");
            }
            cleanInput = cleanInput.Substring(0, cutOff);
            string rf = "RF" + CalculateCheckNumber(cleanInput) + cleanInput;
            return rf.ToList();
        }

        public static string SanitizeRFinput(string input)
        {
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
                   allowedChars = " -",
                   res = string.Empty;
            char c;
            int invalidChars = 0;
            input = input.ToUpper();
            for (int i = 0; i < input.Length; i++)
            {
                c = input.ElementAt(i);
                if (validChars.IndexOf(c) >= 0)
                {
                    res += c.ToString();
                }
                else
                {
                    if (allowedChars.IndexOf(c) < 0)
                        invalidChars++;
                }
            }
            if (invalidChars > 0)
            {
                Console.WriteLine("Syötteestä poistettiin {0} kiellettyä merkkiä.", invalidChars);
            }
            return res;
        }

        public static bool HasProperFormat(string rf, out List<char> rfChars)
        {
            string sanitizedInput = SanitizeRFinput(rf);
            rfChars = new List<char>();
            int resLen = sanitizedInput.Length;
            if (resLen > 4 && resLen < 26)
            {
                if (Regex.IsMatch(sanitizedInput, @"^(RF\d{2})"))
                {
                    foreach (char i in sanitizedInput)
                    {
                        rfChars.Add(i);
                    }
                    return true;
                }
            }
            return false;
        }

        public static string PrintFormatRF(List<char> rf, int groupSize)
        {
            groupSize = groupSize < 1 ? 1 : groupSize;
            var sb = new StringBuilder();
            int len = rf.Count();
            for (int i = 0; i < len; i++)
            {
                sb.Append(string.Format("{0}", rf[i]));
                if ((i + 1) % groupSize == 0)
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }

        private static string GetCharCode(char c)
        {
            c = char.ToUpper(c);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = chars.IndexOf(c);
            if (index >= 0)
            {
                return (10 + index).ToString();
            }
            return c.ToString();
        }

        private static string CalculateCheckNumber(string rfBase)
        {
            string rfString = string.Empty;
            foreach (char i in rfBase)
            {
                rfString += GetCharCode(i);
            }
            List<char> rfBegin = new List<char> {'R', 'F', '0', '0'};
            foreach (char i in rfBegin)
            {
                rfString += GetCharCode(i);
            }
            return (98 - Mod(rfString, 97)).ToString("D2");
        }

        // Copy-paste from http://stackoverflow.com/questions/5662453/modulo-from-very-large-int-c-sharp
        private static int Mod(string x, int y)
        {
            if (x.Length == 0)
                return 0;
            string x2 = x.Substring(0, x.Length - 1);
            int x3 = int.Parse(x.Substring(x.Length - 1));
            return (Mod(x2, y) * 10 + x3) % y;
        }
    }
}
