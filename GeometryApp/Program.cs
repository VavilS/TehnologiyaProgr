using System;
using System.Collections.Generic;

namespace GeometryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>
        {
            new Circle("MyCircle", "red", 5),
            new Rectangle("MyRect", "blue", 4, 6),
            new Triangle("MyTri", "green", 3, 4)
        };

            foreach (var shape in shapes)
            {
                // Вывод базовой информации
                Console.WriteLine(shape.ToString());
                Console.WriteLine($"  area: {shape.GetArea():0.00}");

                // Проверка типа и интерфейса (pattern matching)
                if (shape is Circle circle)
                {
                    Console.WriteLine($"  diameter: {circle.Diameter}, area: {circle.GetArea():0.00}");

                    if (circle is IScalable scalableCircle)
                    {
                        Console.WriteLine("Circle scaled by 2.");
                        scalableCircle.Scale(2);
                        Console.WriteLine($"  new radius: {circle.Radius}, new area: {circle.GetArea():0.00}");
                    }
                }
                else if (shape is Rectangle rectangle)
                {
                    Console.WriteLine($"  is square: {rectangle.IsSquare}, area: {rectangle.GetArea():0.00}");
                    Console.WriteLine("Rectangle cannot scale.");
                }
                else if (shape is Triangle triangle)
                {
                    Console.WriteLine($"  is right: {triangle.IsRightCorrected}, area: {triangle.GetArea():0.00}");
                    Console.WriteLine("Triangle cannot scale.");
                }

                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
