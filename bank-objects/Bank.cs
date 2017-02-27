using System;
using System.Collections.Generic;
using System.Linq;

namespace bank_objects
{
    public class Bank
    {
        private string _name;
        private List<Account> _accounts;
        private Random _rnd = new Random();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public List<Account> Accounts
        {
            get { return _accounts; }
        }

        public Bank(string name)
        {
            _name = name;
            _accounts = new List<Account>();
        }
        public string CreateAccount()
        {
            string accountNumber = "FI";
            for (int i = 0; i < 16; i++)
            {
                accountNumber += _rnd.Next(0,10).ToString();
            }
            _accounts.Add(new Account(accountNumber));
            return accountNumber;
        }
        public List<Transaction> GetTransactionsForCustomer(string accountNumber)
        {
            return (from account in _accounts
                    where account.AccountNumber == accountNumber
                    select account).FirstOrDefault().Transactions;
        }
        public List<Transaction> GetTransactionsForCustomerForTimeSpan(string accountNumber, DateTime startTime, DateTime endTime)
        {
            return (from account in _accounts
                    where account.AccountNumber == accountNumber
                    select account).FirstOrDefault().GetTransactionsForTimeSpan(startTime, endTime);
        }
        public double GetBalanceForCustomer(string accountNumber)
        {
            return (from account in _accounts
                    where account.AccountNumber == accountNumber
                    select account).FirstOrDefault().Balance;
        }
        public bool AddTransactionForCustomer(string accountNumber, Transaction transaction)
        {
            return (from account in _accounts
                    where account.AccountNumber == accountNumber
                    select account).First().AddTransaction(transaction);
        }
    }
}
