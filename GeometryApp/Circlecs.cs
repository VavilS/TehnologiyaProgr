using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryApp
{
    public class Circle : Shape, IScalable
    {
        public double Radius { get; set; }

        public double Diameter => Radius * 2;

        public Circle(string name, string color, double radius) : base(name, color)
        {
            Kind = ShapeKind.Circle;
            Radius = radius;
        }

        public Circle(double radius) : this("Circle", "black", radius)
        {
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public void Scale(double factor)
        {
            if (factor <= 0) throw new ArgumentException("Factor must be positive.");
            Radius *= factor;
        }

        public override string ToString()
        {
            return $"[{Kind}] {Color}, kind: {Kind}, r={Radius}";
        }
    }
}
