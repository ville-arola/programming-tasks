using System.Linq;

namespace barcode
{
    public static class RF
    {
        public static bool IsValidFinnishRFnumber(string rfNum)
        {
            if (!CommonUtilities.IsNumeric(rfNum))
            {
                return false;
            }
            int len = rfNum.Length;
            if (len > 3 && len <= 20)
            {
                int checkNumber = int.Parse(CalculateFinnishRFcheckNumber(rfNum.Substring(0, len - 1)));
                if (checkNumber == int.Parse(rfNum.ElementAt(len - 1).ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidInternationalRFnumber(string input)
        {
            string tmp = input.Substring(4) + input.Substring(0, 4);
            string rfString = CommonUtilities.CharCodes(tmp);
            return CommonUtilities.Mod(rfString, 97) == 1;
        }

        public static string CalculateFinnishRFcheckNumber(string rfBase)
        {
            int i = rfBase.Length - 1,
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
            return checkNumber.ToString();
        }
    }
}
