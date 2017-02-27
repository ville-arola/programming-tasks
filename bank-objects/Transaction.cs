using System;

namespace bank_objects
{
    public class Transaction
    {
        private readonly DateTime _timestamp;
        private readonly double _sum;
        //private readonly bool _isValid;

        public DateTime TimeStamp
        {
            get { return _timestamp; }
        }
        public double Sum
        {
            get { return _sum; }
        }
        /*
        public bool IsValid
        {
            get { return _isValid; }
        }
        */
        public Transaction(double sum, DateTime dateTime)
        {
            //DateTime dateValue;
            //if (DateTime.TryParse(dateString, out dateValue))
            //{
                _timestamp = dateTime;
                _sum = sum;
                //_isValid = true;

            /*}
            else
            {
                _isValid = false;
            }
            */
        }
    }
}
