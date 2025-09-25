using System;

Console.WriteLine("=== Calculator Demo ===");
Console.WriteLine("Menu");
Console.WriteLine("Press 1 for add");
Console.WriteLine("Press 2 for subtraction");
Console.WriteLine("Press 3 for multiplication");
Console.WriteLine("Press 4 for Division");
Console.WriteLine("Press 5: for exit");

// Demo: Addition
Console.WriteLine("\n--- Demo: Addition (1) ---");
Console.WriteLine("Choice: 1");
Console.WriteLine("First number: 10");
Console.WriteLine("Second number: 5");
double result1 = 10 + 5;
Console.WriteLine($"Addition Result: 10 + 5 = {result1}");

// Demo: Subtraction
Console.WriteLine("\n--- Demo: Subtraction (2) ---");
Console.WriteLine("Choice: 2");
Console.WriteLine("First number: 20");
Console.WriteLine("Second number: 8");
double result2 = 20 - 8;
Console.WriteLine($"Subtraction Result: 20 - 8 = {result2}");

// Demo: Multiplication
Console.WriteLine("\n--- Demo: Multiplication (3) ---");
Console.WriteLine("Choice: 3");
Console.WriteLine("First number: 6");
Console.WriteLine("Second number: 4");
double result3 = 6 * 4;
Console.WriteLine($"Multiplication Result: 6 * 4 = {result3}");

// Demo: Division
Console.WriteLine("\n--- Demo: Division (4) ---");
Console.WriteLine("Choice: 4");
Console.WriteLine("First number: 12");
Console.WriteLine("Second number: 3");
double result4 = 12 / 3;
Console.WriteLine($"Division Result: 12 / 3 = {result4}");

// Demo: Division by zero
Console.WriteLine("\n--- Demo: Division by Zero (4) ---");
Console.WriteLine("Choice: 4");
Console.WriteLine("First number: 10");
Console.WriteLine("Second number: 0");
Console.WriteLine("Error: Division by zero is not allowed!");

Console.WriteLine("\n--- Demo: Exit (5) ---");
Console.WriteLine("Choice: 5");
Console.WriteLine("Thank you for using Calculator!");

Console.WriteLine("\nCalculator demonstration completed successfully!");