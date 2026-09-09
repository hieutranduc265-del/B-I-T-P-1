using System;

namespace BankManagement
{
    public class BankAccount
    {
        private static long _nextAccountNumber = 1000000001;

        private string _accountHolder = "";
        private decimal _balance;

        public long AccountNumber { get; init; }

        public string AccountHolder
        {
            get
            {
                return _accountHolder;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ten chu tai khoan khong duoc de trong.");
                }
                _accountHolder = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            private set
            {
                _balance = value;
            }
        }

        public BankAccount(string accountHolder, decimal initialBalance)
        {
            if (initialBalance < 50000)
            {
                throw new ArgumentException("So du ban dau phai tu 50.000 VND tro len.");
            }

            AccountNumber = _nextAccountNumber;
            _nextAccountNumber = _nextAccountNumber + 1;

            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien nap phai lon hon 0.");
                return;
            }

            Balance = Balance + amount;
            Console.WriteLine("Nap tien thanh cong " + amount + " VND.");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien rut phai lon hon 0.");
                return false;
            }

            if (Balance - amount < 50000)
            {
                Console.WriteLine("Rut tien that bai! So du con lai phai duy tri toi thieu 50.000 VND.");
                return false;
            }

            Balance = Balance - amount;
            Console.WriteLine("Rut tien thanh cong " + amount + " VND.");
            return true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("So tai khoan: " + AccountNumber);
            Console.WriteLine("Chu tai khoan: " + AccountHolder);
            Console.WriteLine("So du: " + Balance + " VND");
            Console.WriteLine("----------------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BankAccount acc1 = new BankAccount("Nguyen Van A", 100000);
            BankAccount acc2 = new BankAccount("Tran Thi B", 500000);

            acc1.DisplayInfo();
            acc2.DisplayInfo();

            try
            {
                BankAccount acc3 = new BankAccount("Le Van C", 20000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }

            acc1.Deposit(100000);
            acc1.DisplayInfo();

            acc1.Withdraw(100000);
            acc1.DisplayInfo();

            acc1.Withdraw(80000);
            acc1.DisplayInfo();
        }
    }
}