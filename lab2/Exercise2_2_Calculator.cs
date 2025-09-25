using System;

namespace Lab2
{
    class Exercise2_2_Calculator
    {
        static void Main(string[] args)
        {
            bool continueCalculating = true;
            
            while (continueCalculating)
            {
                Console.Clear();
                Console.WriteLine("=== Calculator ===");
                Console.WriteLine("Menu");
                Console.WriteLine("Press 1 for add");
                Console.WriteLine("Press 2 for subtraction");
                Console.WriteLine("Press 3 for multiplication");
                Console.WriteLine("Press 4 for Division");
                Console.WriteLine("Press 5: for exit");
                Console.Write("\nEnter your choice: ");
                
                string choice = Console.ReadLine();
                
                if (choice == "5")
                {
                    continueCalculating = false;
                    Console.WriteLine("Thank you for using Calculator!");
                    break;
                }
                
                if (choice == "1" || choice == "2" || choice == "3" || choice == "4")
                {
                    Console.Write("Enter first number: ");
                    if (double.TryParse(Console.ReadLine(), out double num1))
                    {
                        Console.Write("Enter second number: ");
                        if (double.TryParse(Console.ReadLine(), out double num2))
                        {
                            double result = 0;
                            string operation = "";
                            
                            switch (choice)
                            {
                                case "1":
                                    result = num1 + num2;
                                    operation = "Addition";
                                    break;
                                case "2":
                                    result = num1 - num2;
                                    operation = "Subtraction";
                                    break;
                                case "3":
                                    result = num1 * num2;
                                    operation = "Multiplication";
                                    break;
                                case "4":
                                    if (num2 != 0)
                                    {
                                        result = num1 / num2;
                                        operation = "Division";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Division by zero is not allowed!");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    break;
                            }
                            
                            Console.WriteLine($"\n{operation} Result: {num1} {GetOperatorSymbol(choice)} {num2} = {result}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid second number!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid first number!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please select 1-5.");
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        
        static string GetOperatorSymbol(string choice)
        {
            return choice switch
            {
                "1" => "+",
                "2" => "-",
                "3" => "*",
                "4" => "/",
                _ => ""
            };
        }
    }
}