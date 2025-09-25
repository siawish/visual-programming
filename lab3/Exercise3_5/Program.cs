using System;

namespace Lab3.Exercise3_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.5: Array Addition ===");
            
            Console.Write("Enter the size of arrays: ");
            int size = int.Parse(Console.ReadLine());
            
            int[] arr1 = new int[size];
            int[] arr2 = new int[size];
            
            // Input first array
            Console.WriteLine("\nEnter elements for first array:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Element {i + 1}: ");
                arr1[i] = int.Parse(Console.ReadLine());
            }
            
            // Input second array
            Console.WriteLine("\nEnter elements for second array:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Element {i + 1}: ");
                arr2[i] = int.Parse(Console.ReadLine());
            }
            
            Console.WriteLine("\nFirst Array:");
            PrintArray(arr1);
            
            Console.WriteLine("\nSecond Array:");
            PrintArray(arr2);
            
            // Add arrays and display result
            AddArray(arr1, arr2);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void AddArray(int[] arr1, int[] arr2)
        {
            int[] result = new int[arr1.Length];
            
            for (int i = 0; i < arr1.Length; i++)
            {
                result[i] = arr1[i] + arr2[i];
            }
            
            Console.WriteLine("\nSum of Arrays:");
            PrintArray(result);
        }
        
        static void PrintArray(int[] theArray)
        {
            Console.Write("[ ");
            foreach (int num in theArray)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("]");
        }
    }
}
