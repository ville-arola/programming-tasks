using System;
using System.Collections.Generic;
using System.Linq;

namespace bank_objects
{
    public class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank("Ankkalinnan pankki");
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("Aku", "Ankka", bank.CreateAccount()));
            customers.Add(new Customer("Pelle", "Peloton", bank.CreateAccount()));
            customers.Add(new Customer("Teppo", "Tulppu", bank.CreateAccount()));
            Random rnd = new Random();
            for (int i = 0; i < 60; i++)
            {
                int c = rnd.Next(0, customers.Count()),
                    day = rnd.Next(1, 30),
                    month = rnd.Next(1, 13),
                    year = rnd.Next(2015, 2018);
                double s = rnd.NextDouble() * 2000 - 900;

                bank.AddTransactionForCustomer(customers[c].AccountNumber, new Transaction(s, new DateTime(year, month, day)));
            }
            PrintBalance(bank, customers[0]);
            PrintBalance(bank, customers[1]);
            PrintBalance(bank, customers[2]);
            PrintTransactions(bank.GetTransactionsForCustomer(customers[0].AccountNumber), customers[0]);
            PrintTransactions(bank.GetTransactionsForCustomer(customers[1].AccountNumber), customers[1]);
            PrintTransactions(bank.GetTransactionsForCustomer(customers[2].AccountNumber), customers[2]);


            var endTime = DateTime.Today;
            var time = new TimeSpan(24*30*6, 0, 0);
            var startTime = endTime.Date - time;
            Console.WriteLine("Tilitapahtumat viimeisen kuuden kuukauden ajalta:");
            PrintTransactions(bank.GetTransactionsForCustomerForTimeSpan(customers[0].AccountNumber, startTime, endTime), customers[0]);
            PrintTransactions(bank.GetTransactionsForCustomerForTimeSpan(customers[1].AccountNumber, startTime, endTime), customers[1]);
            PrintTransactions(bank.GetTransactionsForCustomerForTimeSpan(customers[2].AccountNumber, startTime, endTime), customers[2]);

            Console.ReadKey();
        }

        static void PrintBalance(Bank bank, Customer customer)
        {
            var balance = bank.GetBalanceForCustomer(customer.AccountNumber);
            Console.WriteLine("{0} - balance: {1}{2:0.00}", customer.ToString(), balance >= 0 ? "+" : "", balance);
        }

        static void PrintTransactions(List<Transaction> transactions, Customer customer)
        {
            Console.WriteLine("Tilitapahtumat ({0} {1}):", customer.FirstName, customer.LastName);
            for (int i = 0; i < transactions.Count(); i++)
            {
                Console.WriteLine("{0}\t{1}{2:0.00}", transactions[i].TimeStamp.ToShortDateString(), transactions[i].Sum >= 0 ? "+" : "", transactions[i].Sum);
            }
            Console.WriteLine("\n");
        }
    }
}
