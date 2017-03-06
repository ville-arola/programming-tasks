using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace barcode
{
    public static class BarCodeUtilities
    {
        public static bool ComposeBarCode(string accountNum, string rfNum, double sum, string dueDate, out List<int> barCode)
        {
            string res = string.Empty;
            if (IBAN.IsValidFinnishIBAN(accountNum) && (RF.IsValidFinnishRFnumber(rfNum) || RF.IsValidInternationalRFnumber(rfNum)))
            {
                res += rfNum.StartsWith("RF") ? "5" : "4";
                res += FormatAccountNumber(accountNum);
                if (sum >= 0)
                {
                    res += sum > 999999.99 ? "00000000" : int.Parse(Math.Round(sum * 100).ToString()).ToString("D8");
                }
                res += FormatRFnumber(rfNum);
                res += IsProperDate(dueDate) ? dueDate : "000000";
            }
            barCode = new List<int>();
            if (res.Length == 54)
            {
                barCode.Add(105);
                int i = 0;
                while (i < res.Length - 1)
                {
                    barCode.Add(int.Parse(res.Substring(i, 2)));
                    i += 2;
                }
                barCode.Add(CalculateCheckNumber(barCode));
                return true;
            }
            return false;
        }

        public static string PrintFormatBarCode(List<int> barCode)
        {
            string res = string.Format("[{0}]", barCode[0]);
            int codeLen = barCode.Count();
            for (int i = 1; i < codeLen - 1; i++)
            {
                res += string.Format(" {0:00}", barCode[i]);
            }
            res += string.Format(" [{0}] [stop]", barCode[codeLen - 1]);
            return res;
        }

        private static int CalculateCheckNumber(List<int> code)
        {
            int checkSum = code[0];
            for (int i = 1; i < code.Count(); i++)
            {
                checkSum += code[i] * i;
            }
            return CommonUtilities.Mod(checkSum.ToString(), 103);
        }

        private static bool IsProperDate(string dateStr)
        {
            DateTime d;
            return DateTime.TryParseExact(dateStr, "yyMMdd", new CultureInfo("fi-FI"), DateTimeStyles.None, out d);
        }

        private static string FormatAccountNumber(string accountNum)
        {
            return accountNum.Substring(2);
        }

        private static string FormatRFnumber(string rfNum)
        {
            string res = string.Empty;
            if (rfNum.ToUpper().StartsWith("RF"))
            {
                if (!CommonUtilities.IsNumeric(rfNum.Substring(2)))
                {
                    res = "00000000000000000000000";
                }
                else
                {
                    res = rfNum.Substring(2,2);
                    string end = rfNum.Substring(4);
                    while ((res + end).Length < 23)
                    {
                        res += "0";
                    }
                    res += end;
                }
            }
            else
            {
                while ((res + rfNum).Length < 23)
                {
                    res += "0";
                }
                res += rfNum;
            }
            return res;
        }    
    }
}
