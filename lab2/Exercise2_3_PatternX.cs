using System;

namespace Lab2
{
    class Exercise2_3_PatternX
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Pattern Printing with 'x' ===");
            
            // Pattern starts with 15 x's and decreases by 2 each line
            int[] xCounts = { 15, 13, 11, 9, 7, 5, 3, 1 };
            
            for (int i = 0; i < xCounts.Length; i++)
            {
                // Calculate spaces for centering
                int spaces = i;
                
                // Print leading spaces
                for (int j = 0; j < spaces; j++)
                {
                    Console.Write(" ");
                }
                
                // Print x's
                for (int k = 0; k < xCounts[i]; k++)
                {
                    Console.Write("x");
                }
                
                Console.WriteLine();
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}