using System;

namespace InterfaceAndPolymorphism
{
    public interface IShape
    {
        double CalculateArea();
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            this.Radius = radius;
        }
        public double CalculateArea()
        {
            return Math.PI * Math.Pow(Radius, 2); // Use Radius property
        }
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public double CalculateArea()
        {
            return Width * Height;
        }
    }

    public class Square : IShape
    {
        public double Side { get; set; }
        public Square(double side)
        {
            this.Side = side;
        }
        public double CalculateArea()
        {
            return Side * Side;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IShape[] arr = {
                new Circle(1),
                new Rectangle(1, 1),
                new Square(1)
            };

            foreach (IShape shape in arr)
            {
                Console.WriteLine(shape.CalculateArea());
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"Area of shape {i + 1}: {arr[i].CalculateArea()}"); // More descriptive output
                Console.WriteLine($"Type of shape {i + 1}: {arr[i].GetType()}");
            }

            Console.ReadKey();
        }
    }
}
