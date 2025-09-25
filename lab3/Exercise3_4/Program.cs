using System;

namespace Lab3.Exercise3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.4: Bubble Sort with Random Numbers ===");
            
            Console.Write("Enter the size of array: ");
            int size = int.Parse(Console.ReadLine());
            
            int[] numbers = new int[size];
            Random r = new Random();
            
            // Fill array with random numbers
            for (int i = 0; i < size; i++)
            {
                numbers[i] = r.Next(1, 100); // Range between 1 to 100
            }
            
            Console.WriteLine("\nUnsorted Array:");
            PrintArray(numbers);
            
            // Sort using bubble sort
            BubbleSort(numbers);
            
            Console.WriteLine("\nSorted Array:");
            PrintArray(numbers);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Swap elements
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        
        static void PrintArray(int[] arr)
        {
            Console.Write("[ ");
            foreach (int num in arr)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("]");
        }
    }
}
