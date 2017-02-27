namespace bank_objects
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;
        private string _accountNumber;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string AccountNumber
        {
            get { return _accountNumber; }
        }

        public Customer(string firstName, string lastName, string accountNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _accountNumber = accountNumber;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}\t({2})", _firstName, _lastName, _accountNumber);
        }
    }
}
