using System;

namespace Lab3.Exercise3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.2: Palindrome Checker ===");
            
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();
            
            // Convert to lowercase for case-insensitive comparison
            string st = input.ToLower();
            string temp = "";
            
            // Reverse the string using for loop
            for (int i = st.Length - 1; i >= 0; i--)
            {
                temp += st[i];
            }
            
            // Compare original with reversed
            if (temp == st)
            {
                Console.WriteLine($"'{input}' is a palindrome!");
            }
            else
            {
                Console.WriteLine($"'{input}' is not a palindrome.");
            }
            
            Console.WriteLine($"Original: {st}");
            Console.WriteLine($"Reversed: {temp}");
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
