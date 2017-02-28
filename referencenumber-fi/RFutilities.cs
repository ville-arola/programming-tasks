using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace referencenumber_fi
{
    public static class RFutilities
    {
        public static bool ValidateRFnumber(List <char> rf)
        {
            bool res = false;
            int len = rf.Count();
            if (len > 3 && len <= 20)
            {
                int checkNumber = CalculateCheckNumber(rf.GetRange(0, len - 1));
                if (checkNumber == rf[len - 1])
                {
                    res = true;
                }
            }
            return res;
        }

        public static char CalculateCheckNumber(List<char> rfBase)
        {
            int i = rfBase.Count() - 1,
                j = 0,
                sumMultiplied = 0,
                checkNumber = 0;
            int[] multipliers = { 7, 3, 1 };
            while (i >= 0)
            {
                if (j > 2)
                {
                    j = 0;
                }
                sumMultiplied += multipliers[j] * int.Parse(rfBase[i].ToString());
                j++;
                i--;
            }
            while ((sumMultiplied + checkNumber) % 10 != 0)
            {
                checkNumber++;
            }
            return char.Parse(checkNumber.ToString());
        }

        public static List<char> SanitizeRFstring(string rf)
        {
            List<char> res = new List <char>();
            string nums = "1234567890";
            string otherAllowed = "- ";
            char c;
            int invalidChars = 0;
			bool leadingZeros = true;
            for (int i = 0; i < rf.Length; i++)
            {
                c = rf.ElementAt(i);
                if (leadingZeros)
				{
				    if (c.Equals('0'))
				    {
                        continue;
                    }
				    else
				    {
                        leadingZeros = false;
                    }
				}
                if (nums.IndexOf(c) >= 0)
                {
                    res.Add(c);
                }
                else if (otherAllowed.IndexOf(c) < 0)
                {
                    invalidChars++;
                }
            }
            if (invalidChars > 0)
            {
                Console.WriteLine("Syötteessä oli {0} kiellettyä merkkiä!", invalidChars);
            }
            return res;
        }

        public static string PrintFormatRF(List<char> rf)
        {
            var sb = new StringBuilder();
            int len = rf.Count();
            for (int i = len - 1; i >= 0; i--)
            {
                if ((len - (i + 1)) % 5 == 0)
                {
                    sb.Insert(0, ' ');
                }
                sb.Insert(0, string.Format("{0}", rf[i]));
            }
            return sb.ToString();
        }
    }
}
