using System;
using System.Collections.Generic;

namespace international_reference_number
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programming tasks - international referencenumber\n");
            Console.Write("Tarkasta viitenumero (1)\nLuo viitenumero (2)\nLopeta (3)\n");
            string action;
            do
            {
                Console.Write("Valitse toiminto: ");
                action = Console.ReadLine();
                switch (action)
                {
                    case "1":
                        RFvalidation();
                        break;
                    case "2":
                        RFgeneration();
                        break;
                }
            } while (!action.Equals("3"));
        }

        private static void RFvalidation()
        {
            Console.Write("Syötä kansainvälinen viitenumero: ");
            List<char> rfNumber = new List<char>();
            if (RFutilities.HasProperFormat(RFutilities.SanitizeRFinput(Console.ReadLine()), out rfNumber))
            {
                Console.WriteLine("{0} - OK", RFutilities.PrintFormatRF(rfNumber, 4));
            }
            else
            {
                Console.WriteLine("Syöte on virheellinen.");
            }
        }

        private static void RFgeneration()
        {
            Console.Write("Syötä viitenumeron loppuosa: ");
            List<char> rfNumber = RFutilities.GenerateRF(Console.ReadLine());
            Console.WriteLine("{0}", RFutilities.PrintFormatRF(rfNumber, 4));
        }
    }
}
