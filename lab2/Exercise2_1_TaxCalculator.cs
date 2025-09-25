using System;

namespace Lab2
{
    class Exercise2_1_TaxCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Tax Calculator ===");
            Console.Write("Enter the amount of money: ");
            
            if (double.TryParse(Console.ReadLine(), out double money))
            {
                double taxRate;
                double totalTax;
                
                if (money < 10000)
                {
                    taxRate = 5.0;
                }
                else if (money >= 10000 && money <= 100000)
                {
                    taxRate = 8.0;
                }
                else // More than 100,000
                {
                    taxRate = 8.5;
                }
                
                totalTax = money * (taxRate / 100);
                
                Console.WriteLine($"\nMoney: {money:C}");
                Console.WriteLine($"Tax Rate: {taxRate}%");
                Console.WriteLine($"Total Tax: {totalTax:C}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}