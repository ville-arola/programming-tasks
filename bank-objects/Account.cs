using System;
using System.Collections.Generic;
using System.Linq;

namespace bank_objects
{
    public class Account
    {
        private string _accountNumber;
        private List<Transaction> _transactions;
        private double _balance;

        public string AccountNumber
        {
            get { return _accountNumber; }
        }
        public List<Transaction> Transactions
        {
            get { return (from transaction in _transactions
                          orderby transaction.TimeStamp
                          select transaction).ToList();
            }
        }
        public double Balance
        {
            get { return _balance; }
        }

        public Account(string accountNumber)
        {
            _accountNumber = accountNumber;
            _transactions = new List<Transaction>();
            _balance = 0;
        }
        public List<Transaction> GetTransactionsForTimeSpan(DateTime startTime, DateTime endTime)
        {
            List<Transaction> res = (from transaction in _transactions
                        where transaction.TimeStamp >= startTime && transaction.TimeStamp <= endTime
                        orderby transaction.TimeStamp
                        select transaction).ToList();
            return res;
        }
        public bool AddTransaction(Transaction transaction)
        {
            bool res = false;
            //if (transaction.IsValid)
            //{
                _transactions.Add(transaction);
                double balanceBeforeTransaction = _balance;
                if (_transactions.Last().Equals(transaction))
                {
                    _balance += transaction.Sum;
                }
                if (_balance - transaction.Sum == balanceBeforeTransaction)
                {
                    res = true;
                }
            //}
            return res;
        }
    }
}
