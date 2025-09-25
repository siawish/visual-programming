using System;

namespace Lab3.Exercise3_6
{
    class Circle
    {
        private double radius;
        
        public Circle(double radius)
        {
            this.radius = radius;
        }
        
        public double CalculateArea()
        {
            return Math.PI * radius * radius;
        }
        
        public double CalculateCircumference()
        {
            return 2 * Math.PI * radius;
        }
        
        public double GetRadius()
        {
            return radius;
        }
        
        public void SetRadius(double radius)
        {
            this.radius = radius;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Exercise 3.6: Circle Area and Circumference Calculator ===");
            
            Console.Write("Enter the radius of the circle: ");
            double radius = double.Parse(Console.ReadLine());
            
            Circle circle = new Circle(radius);
            
            double area = circle.CalculateArea();
            double circumference = circle.CalculateCircumference();
            
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("CIRCLE CALCULATIONS");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"Radius:        {circle.GetRadius():F2}");
            Console.WriteLine($"Area:          {area:F2}");
            Console.WriteLine($"Circumference: {circumference:F2}");
            Console.WriteLine(new string('=', 40));
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
