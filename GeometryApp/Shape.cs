using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryApp
{
    public abstract class Shape
    {
        private readonly string _name;

        public string Color { get; set; }

        public ShapeKind Kind { get; set; } = ShapeKind.Circle;

        public string Info => $"{Kind} ({Color})";


        protected Shape(string name)
        {
            _name = name;
        }

        public Shape(string name, string color) : this(name)
        {
            Color = color;
        }

        public Shape() : this("Unnamed", "black")
        {
        }

        public abstract double GetArea();

        public override string ToString()
        {
            return $"{_name} ({Info})";
        }
    }
}
