using System;

namespace PaymentSystem
{
    public interface IPayable
    {
        bool ProcessPayment(decimal amount);
    }

    public interface IRefundable
    {
        bool ProcessRefund(decimal amount, string reason);
    }

    public abstract class PaymentGateway
    {
        public string TransactionId { get; init; }
        public DateTime CreationDate { get; init; }
        public string Status { get; protected set; }

        protected PaymentGateway(string transactionId)
        {
            TransactionId = transactionId;
            CreationDate = DateTime.Now;
            Status = "Pending";
        }

        public abstract void ValidateConnection();

        public virtual void LogTransaction(string message)
        {
            Console.WriteLine("[" + TransactionId + "] " + message + " (Trang thai: " + Status + ")");
        }
    }

    public class MomoPayment : PaymentGateway, IPayable, IRefundable
    {
        public string PhoneNumber { get; set; }

        public MomoPayment(string transactionId, string phoneNumber) : base(transactionId)
        {
            PhoneNumber = phoneNumber;
        }

        public override void ValidateConnection()
        {
            Console.WriteLine("Kiem tra ket noi API MoMo cho so dien thoai " + PhoneNumber + "... Thanh cong!");
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien thanh toan khong hop le.");
                return false;
            }

            Status = "Success";
            LogTransaction("Thanh toan MoMo thanh cong so tien: " + amount + " VND");
            return true;
        }

        public bool ProcessRefund(decimal amount, string reason)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien hoan khong hop le.");
                return false;
            }

            Status = "Refunded";
            LogTransaction("Hoan tien MoMo thanh cong so tien: " + amount + " VND. Ly do: " + reason);
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MomoPayment momo = new MomoPayment("TXN123456", "0987654321");

            momo.ValidateConnection();
            Console.WriteLine("----------------------------------");

            IPayable payableService = momo;
            payableService.ProcessPayment(500000);
            Console.WriteLine("----------------------------------");

            IRefundable refundableService = momo;
            refundableService.ProcessRefund(500000, "Khach hang huy don");
            Console.WriteLine("----------------------------------");
        }
    }
}