using System;
using System.Collections.Generic;
using System.Linq;

namespace referencenumber_fi
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programming tasks - referencenumber-fi\n");
            Console.Write("Tarkasta viitenumero (1)\nLuo viitenumeroja (2)\nLopeta (3)\n");
            string action;
            do
            {
                Console.Write("Valitse toiminto: ");
                action = Console.ReadLine();
                switch (action)
                {
                    case "1":
                    {
                        RFvalidation();
                        break;
                    }
                    case "2":
                    {
                        RFgeneration();
                        break;
                    }
                }
            } while (!action.Equals("3"));
        }

        private static void RFvalidation()
        {
            Console.Write("Syötä viitenumero: ");
            List <char> rfNum = RFutilities.SanitizeRFstring(Console.ReadLine());
            if (RFutilities.ValidateRFnumber(rfNum))
            {
                Console.WriteLine("{0} - OK", RFutilities.PrintFormatRF(rfNum));
            }
            else
            {
                Console.WriteLine("Viitenumero on virheellinen.");
            }
        }

        private static void RFgeneration()
        {
            Console.Write("Viitenumeron alkuosa: ");
            List<char> res = RFutilities.SanitizeRFstring(Console.ReadLine());
            int n, i;
            bool inputSuccess;
            do
            {
                Console.Write("Viitenumerojen määrä: ");
                inputSuccess = int.TryParse(Console.ReadLine(), out n);
                if (!inputSuccess)
                {
                    Console.WriteLine("Virheellinen syöte.");
                }
            } while (!inputSuccess);

            if (res.Count() > 19)
            {
                Console.WriteLine("Syöte on liian pitkä. Vain ensimmäiset 19 numeroa huomioidaan.");
                res = res.GetRange(0, 19); 
            }

            List<List<char>> rfNumbers = new List<List<char>>();
            for (i = 1; i <= n; i++)
            {
                List<char> rf = new List<char>(res);
                if (n > 1)
                {
                    string iStr = i.ToString();
                    int iLen = iStr.Length,
                        rfLength = rf.Count() + iLen + 1;
                    if (rfLength > 20)
                    {
                        rf = rf.GetRange(0, 19 - iLen);
                    }
                    for (int j = 0; j < iLen; j++)
                    {
                        rf.Add(iStr.ElementAt(j));
                    }
                }
                char checkNum = RFutilities.CalculateCheckNumber(rf);
                rf.Add(checkNum);
                rfNumbers.Add(rf);
            }
            if (n == 1)
            {
                Console.WriteLine("{0}", RFutilities.PrintFormatRF(rfNumbers[0]));
            }
            else
            {
                Console.WriteLine("Luotiin viitenumerot..");
                for (i = 0; i < rfNumbers.Count(); i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, RFutilities.PrintFormatRF(rfNumbers[i]));
                }
            }
        }
    }
}
