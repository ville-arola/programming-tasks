using System.Linq;

namespace barcode
{
    public static class BBAN
    {
        public static bool ToIBAN(string bban, out string iban)
        {
            iban = string.Empty;
            if (!ConvertToMachineFormat(bban, out bban))
            {
                return false;
            }
            string countryCode = "FI";
            string tmp = bban + CommonUtilities.CharCodes(countryCode) + "00",
                   ibanCheck = string.Format("{0:D2}", 98 - CommonUtilities.Mod(tmp, 97));
            iban = string.Format("{0}{1}", countryCode, ibanCheck);
            foreach (char i in bban)
            {
                iban += i;
            }
            return iban.Length == 18;
        }

        public static bool IsValid(string bban)
        {
            int bbanLen = bban.Length;
            if (bbanLen < 8 || bbanLen > 14)
            {
                return false;
            }
            if (!ConvertToMachineFormat(bban, out bban))
            {
                return false;
            }
            int[] numbers = new int[14];
            int n;
            for (int i = 13; i >= 0; i--)
            {
                if (int.TryParse(bban.ElementAt(i).ToString(), out n))
                {
                    n = (i + 1) % 2 == 0 ? n : n * 2;
                    numbers[i] = n < 10 ? n : n - 9;
                }
                else
                {
                    return false;
                }
            }
            return numbers.Sum() % 10 == 0;
        }

        private static bool ConvertToMachineFormat(string bban, out string mfBban)
        {
            mfBban = string.Empty;
            if (!CommonUtilities.IsNumeric(bban))
            {
                return false;
            }
            int bbanLen = bban.Length;
            if (bbanLen > 14)
            {
                bbanLen = 14;
            }
            if (bbanLen > 6)
            {
                int padIndex;
                if (bban.StartsWith("4") || bban.StartsWith("5"))
                {
                    padIndex = 7;
                }
                else
                {
                    padIndex = 6;
                }
                while (bban.Length < 14)
                {
                    bban = bban.Insert(padIndex, "0");
                }
                mfBban = bban;
            }
            return mfBban.Length == 14;
        }
    }
}
