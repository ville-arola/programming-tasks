using System.Linq;
using System.Text.RegularExpressions;

namespace barcode
{
    public static class CommonUtilities
    {
        public static string CharCodes(string str)
        {
            string res = string.Empty;
            foreach (char i in str)
            {
                res += GetCharCode(i);
            }
            return res;
        }

        public static string GetCharCode(char c)
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

        public static string SanitizeAlphaNumeric(string input)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz",
                   allowedChars = "1234567890" + alphabet + alphabet.ToUpper();
            return Filter(input, allowedChars);
        }

        public static bool IsNumeric(string input)
        {
            return !Regex.IsMatch(input, @"\D");
        }

        private static string Filter(string input, string allowedChars)
        {
            string res = string.Empty;
            char c;
            for (int i = 0; i < input.Length; i++)
            {
                c = input.ElementAt(i);
                if (allowedChars.IndexOf(c) >= 0)
                {
                    res += c;
                }
            }
            return res;
        }

        // Copy-paste from http://stackoverflow.com/questions/5662453/modulo-from-very-large-int-c-sharp
        public static int Mod(string x, int y)
        {
            if (x.Length == 0)
                return 0;
            string x2 = x.Substring(0, x.Length - 1);
            int x3 = int.Parse(x.Substring(x.Length - 1));
            return (Mod(x2, y) * 10 + x3) % y;
        }
    }
}
