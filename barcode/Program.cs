using System;
using System.Collections.Generic;

namespace barcode
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Programming tasks - barcode\n");
            Console.Write("Luo virtuaaliviivakoodi (1)\nLopeta (2)\n");
            string action;
            do
            {
                Console.Write("Valitse toiminto: ");
                action = Console.ReadLine();
                if (action == "1")
                {
                    GenerateBarCode();
                }
            } while (action != "2");
        }

        private static void GenerateBarCode()
        {
            Console.WriteLine("Syötä pankkiviivakoodiin tarvittavat tiedot.");
            string accountNum = string.Empty,
                   rfNum = string.Empty,
                   dateStr = string.Empty;
            double sum;
            DateTime dueDate = new DateTime();

            do
            {
                Console.Write("Saajan tilinumero: ");
            } while (!IsValidAccountNumber(Console.ReadLine(), out accountNum));

            do
            {
                Console.Write("Laskun viitenumero: ");
            } while (!IsValidRFnumber(Console.ReadLine(), out rfNum));

            do
            {
                Console.Write("Summa: ");
            } while (!IsValidSum(Console.ReadLine(), out sum));

            do
            {
                Console.Write("Laskun eräpäivä (DD.MM.YYYY): ");
                dateStr = Console.ReadLine();
                if (dateStr.Length == 0)
                {
                    break;
                }
            } while (!IsValidDueDate(dateStr, out dueDate));
            dateStr = dateStr.Length == 0 ? "000000" : dueDate.ToString("yyMMdd");
            
            List<int> virtualBarCode;
            if (BarCodeUtilities.ComposeBarCode(accountNum, rfNum, sum, dateStr, out virtualBarCode))
            {
                Console.WriteLine(BarCodeUtilities.PrintFormatBarCode(virtualBarCode));
            }
            else
            {
                Console.WriteLine("Virtuaaliviivakoodin luonti epäonnistui.");
            }
            
        }

        private static bool IsValidDueDate(string input, out DateTime dueDate)
        {
            if (DateTime.TryParse(input, out dueDate))
            {
                return true;
            }
            Console.WriteLine("Syötetty eräpäivä ei ollut kelvollinen.");
            return false;
        }

        private static bool IsValidSum(string input, out double sum)
        {
            input = input.Replace(".", ",");
            if (double.TryParse(input, out sum))
            {
                if (sum >= 0)
                {
                    sum = Math.Round(sum * 100) / 100;
                    return true;
                }
            }
            Console.WriteLine("Syötetty summa ei ollut kelvollinen.");
            return false;
        }

        private static bool IsValidRFnumber(string input, out string rfNum)
        {
            input = CommonUtilities.SanitizeAlphaNumeric(input.ToUpper());
            rfNum = input;
            if (RF.IsValidFinnishRFnumber(input) || RF.IsValidInternationalRFnumber(input))
            {
                return true;
            }
            Console.WriteLine("Syötetty viitenumero ei ollut kelvollinen.");
            return false;
        }

        private static bool IsValidAccountNumber(string input, out string accountNum)
        {
            input = CommonUtilities.SanitizeAlphaNumeric(input.ToUpper());
            accountNum = input;
            if (IBAN.IsValidFinnishIBAN(input))
            {
                return true;
            }
            if (BBAN.IsValid(input))
            {
                if (BBAN.ToIBAN(input, out accountNum))
                {
                    return true;
                }
            }
            Console.WriteLine("Syötetty tilinumero ei ollut kelvollinen.");
            return false;
        }
    }
}