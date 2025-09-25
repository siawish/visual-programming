using System;

namespace Lab3.Exercise3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.1: Odd/Even Numbers Table ===");
            
            int[] numbers = new int[10];
            
            // Input 10 numbers from user
            Console.WriteLine("Enter 10 numbers:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Number {i + 1}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }
            
            // Separate odd and even numbers
            int[] oddNumbers = new int[10];
            int[] evenNumbers = new int[10];
            int oddCount = 0, evenCount = 0;
            
            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                {
                    evenNumbers[evenCount] = num;
                    evenCount++;
                }
                else
                {
                    oddNumbers[oddCount] = num;
                    oddCount++;
                }
            }
            
            // Display in table format
            Console.WriteLine("\n" + new string('=', 20));
            Console.WriteLine("{0,-10} {1,-10}", "ODD", "EVEN");
            Console.WriteLine(new string('=', 20));
            
            int maxCount = Math.Max(oddCount, evenCount);
            for (int i = 0; i < maxCount; i++)
            {
                string oddValue = i < oddCount ? oddNumbers[i].ToString() : "";
                string evenValue = i < evenCount ? evenNumbers[i].ToString() : "";
                Console.WriteLine("{0,-10} {1,-10}", oddValue, evenValue);
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
