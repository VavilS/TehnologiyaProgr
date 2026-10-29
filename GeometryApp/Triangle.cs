using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryApp
{
    public class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public bool IsRight => Math.Abs(Base * Base + Height * Height - Math.Pow(Math.Sqrt(Base * Base + Height * Height), 2)) < 0.0001;
        public double Hypotenuse => Math.Sqrt(Base * Base + Height * Height);
        public bool IsRightCorrected => Math.Abs(Base * Base + Height * Height - Hypotenuse * Hypotenuse) < 0.0001;

        public Triangle(string name, string color, double baseLength, double height) : base(name, color)
        {
            Kind = ShapeKind.Triangle;
            Base = baseLength;
            Height = height;
        }

        public Triangle(double baseLength, double height) : this("Triangle", "black", baseLength, height)
        {
        }

        public override double GetArea()
        {
            return 0.5 * Base * Height;
        }

        public override string ToString()
        {
            return $"[{Kind}] {Color}, kind: {Kind}, base={Base}, height={Height}";
        }
    }
}
