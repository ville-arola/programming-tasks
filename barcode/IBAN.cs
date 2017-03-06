using System.Text.RegularExpressions;

namespace barcode
{
    public static class IBAN
    {
        public static bool IsValidFinnishIBAN(string iban)
        {
            if (!Regex.IsMatch(iban, @"^(FI\d{2})"))
            {
                return false;
            }
            string ibanStart = iban.Substring(0, 4),
                   ibanBody = CommonUtilities.CharCodes(iban.Substring(4));
            iban = ibanBody + ibanStart;
            if (iban.Length != 18)
            {
                return false;
            }
            return CommonUtilities.Mod(CommonUtilities.CharCodes(iban), 97) == 1;
        }
    }
}
