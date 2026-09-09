using System;

namespace EmployeeManagement
{
    public class Person
    {
        public string Id { get; init; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }

        public Person(string id, string fullName, int birthYear)
        {
            Id = id;
            FullName = fullName;
            BirthYear = birthYear;
        }

        public int GetAge(int currentYear)
        {
            return currentYear - BirthYear;
        }
    }

    public class Employee : Person
    {
        public decimal BaseSalary { get; set; }

        public Employee(string id, string fullName, int birthYear, decimal baseSalary) 
            : base(id, fullName, birthYear)
        {
            BaseSalary = baseSalary;
        }

        public virtual decimal CalculateIncome()
        {
            return BaseSalary;
        }
    }

    public sealed class Manager : Employee
    {
        public decimal ResponsibilityAllowance { get; set; }

        public Manager(string id, string fullName, int birthYear, decimal baseSalary, decimal allowance) 
            : base(id, fullName, birthYear, baseSalary)
        {
            ResponsibilityAllowance = allowance;
        }

        public override decimal CalculateIncome()
        {
            return BaseSalary + ResponsibilityAllowance;
        }
    }

    // Khong the tao lop duoi day vi lop Manager da co tu khoa 'sealed':
    // public class SeniorManager : Manager { }
    // Nguoi bien dich se bao loi vì 'sealed' da niem phong va ngan chan ke thua.

    class Program
    {
        static void Main(string[] args)
        {
            int currentYear = 2026;

            Employee emp = new Employee("NV01", "Nguyen Van A", 1998, 10000000);
            Manager mng = new Manager("QL01", "Tran Thi B", 1990, 20000000, 5000000);

            Console.WriteLine("=== PHIEU LUONG NHAN VIEN ===");
            Console.WriteLine("Ho ten: " + emp.FullName);
            Console.WriteLine("Tuoi: " + emp.GetAge(currentYear));
            Console.WriteLine("Luong co ban: " + emp.BaseSalary + " VND");
            Console.WriteLine("Thu nhap thuc linh: " + emp.CalculateIncome() + " VND");

            Console.WriteLine("\n=== PHIEU LUONG QUAN LY ===");
            Console.WriteLine("Ho ten: " + mng.FullName);
            Console.WriteLine("Tuoi: " + mng.GetAge(currentYear));
            Console.WriteLine("Luong co ban: " + mng.BaseSalary + " VND");
            Console.WriteLine("Phu cap trach nhiem: " + mng.ResponsibilityAllowance + " VND");
            Console.WriteLine("Thu nhap thuc linh: " + mng.CalculateIncome() + " VND");
        }
    }
}