using System;

namespace Lab3.Exercise3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.3: Student Records ===");
            
            Console.Write("Enter number of students: ");
            int numStudents = int.Parse(Console.ReadLine());
            
            string[] names = new string[numStudents];
            string[] enrollmentNumbers = new string[numStudents];
            double[] marks = new double[numStudents];
            
            // Input student data
            for (int i = 0; i < numStudents; i++)
            {
                Console.WriteLine($"\nStudent {i + 1}:");
                Console.Write("Name: ");
                names[i] = Console.ReadLine();
                
                Console.Write("Enrollment Number: ");
                enrollmentNumbers[i] = Console.ReadLine();
                
                Console.Write("Marks: ");
                marks[i] = double.Parse(Console.ReadLine());
            }
            
            // Find and display highest and lowest marks
            int highestIndex = FindHighestMarks(marks);
            int lowestIndex = FindLowestMarks(marks);
            
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("RESULTS:");
            Console.WriteLine(new string('=', 50));
            
            Console.WriteLine($"Highest Marks: {names[highestIndex]} ({enrollmentNumbers[highestIndex]}) - {marks[highestIndex]} marks");
            Console.WriteLine($"Lowest Marks:  {names[lowestIndex]} ({enrollmentNumbers[lowestIndex]}) - {marks[lowestIndex]} marks");
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static int FindHighestMarks(double[] marks)
        {
            int highestIndex = 0;
            for (int i = 1; i < marks.Length; i++)
            {
                if (marks[i] > marks[highestIndex])
                {
                    highestIndex = i;
                }
            }
            return highestIndex;
        }
        
        static int FindLowestMarks(double[] marks)
        {
            int lowestIndex = 0;
            for (int i = 1; i < marks.Length; i++)
            {
                if (marks[i] < marks[lowestIndex])
                {
                    lowestIndex = i;
                }
            }
            return lowestIndex;
        }
    }
}
