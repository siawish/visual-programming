using System;

namespace Lab2
{
    class Exercise2_4_NumberPattern
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Number Pattern (1-15) ===");
            
            int number = 1;
            
            // Pattern: 1 number in row 1, 2 numbers in row 2, etc.
            for (int row = 1; row <= 5; row++)
            {
                for (int col = 1; col <= row; col++)
                {
                    Console.Write(number);
                    
                    // Add space between numbers (except for last number in row)
                    if (col < row)
                    {
                        Console.Write(" ");
                    }
                    
                    number++;
                    
                    // Stop when we reach 15
                    if (number > 15)
                        break;
                }
                
                Console.WriteLine();
                
                // Stop when we reach 15
                if (number > 15)
                    break;
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}