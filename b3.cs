using System;
using System.Collections.Generic;

namespace OrderManagement
{
    public class DiscountCalculator
    {
        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount * 0.95m;
        }

        public decimal ApplyDiscount(decimal totalAmount, double percentage)
        {
            decimal percentDecimal = (decimal)percentage / 100m;
            return totalAmount - (totalAmount * percentDecimal);
        }

        public decimal ApplyDiscount(decimal totalAmount, decimal fixedVoucher, decimal minimumOrder)
        {
            if (totalAmount >= minimumOrder)
            {
                return totalAmount - fixedVoucher;
            }
            return totalAmount;
        }
    }

    public class DeliveryService
    {
        public string OrderId { get; set; }
        public double DistanceKm { get; set; }

        public DeliveryService(string orderId, double distanceKm)
        {
            OrderId = orderId;
            DistanceKm = distanceKm;
        }

        public virtual decimal CalculateShippingFee()
        {
            return (decimal)DistanceKm * 5000m;
        }
    }

    public class ExpressDelivery : DeliveryService
    {
        public ExpressDelivery(string orderId, double distanceKm) 
            : base(orderId, distanceKm)
        {
        }

        public override decimal CalculateShippingFee()
        {
            decimal baseFee = base.CalculateShippingFee();
            return (baseFee * 1.5m) + 20000m;
        }
    }

    public class EcoDelivery : DeliveryService
    {
        public EcoDelivery(string orderId, double distanceKm) 
            : base(orderId, distanceKm)
        {
        }

        public override decimal CalculateShippingFee()
        {
            decimal baseFee = base.CalculateShippingFee();
            if (DistanceKm > 10)
            {
                return baseFee * 0.9m;
            }
            return baseFee;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DiscountCalculator calculator = new DiscountCalculator();
            decimal orderTotal = 1000000m;

            Console.WriteLine("=== TEST GIAM GIA (METHOD OVERLOADING) ===");
            Console.WriteLine("Gia ban dau: " + orderTotal + " VND");
            Console.WriteLine("Giam mac dinh 5%: " + calculator.ApplyDiscount(orderTotal) + " VND");
            Console.WriteLine("Giam 10% tuy chinh: " + calculator.ApplyDiscount(orderTotal, 10.0) + " VND");
            Console.WriteLine("Giam voucher 50.000 VND (don tu 500.000 VND): " + calculator.ApplyDiscount(orderTotal, 50000m, 500000m) + " VND");

            Console.WriteLine("\n=== TEST PHI VAN CHUYEN (METHOD OVERRIDING) ===");
            List<DeliveryService> deliveries = new List<DeliveryService>();
            deliveries.Add(new ExpressDelivery("DH01", 12.0));
            deliveries.Add(new EcoDelivery("DH02", 12.0));
            deliveries.Add(new EcoDelivery("DH03", 5.0));

            foreach (DeliveryService delivery in deliveries)
            {
                Console.WriteLine("Ma don hang: " + delivery.OrderId);
                Console.WriteLine("Quang duong: " + delivery.DistanceKm + " km");
                Console.WriteLine("Phi van chuyen: " + delivery.CalculateShippingFee() + " VND");
                Console.WriteLine("----------------------------------");
            }
        }
    }
}