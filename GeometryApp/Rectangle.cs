using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryApp
{ 
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public bool IsSquare => Math.Abs(Width - Height) < 0.0001;

        public Rectangle(string name, string color, double width, double height) : base(name, color)
        {
            Kind = ShapeKind.Rectangle;
            Width = width;
            Height = height;
        }

        public Rectangle(double width, double height) : this("Rectangle", "black", width, height)
        {
        }

        public override double GetArea()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"[{Kind}] {Color}, kind: {Kind}, {Width}x{Height}";
        }
    }
}
