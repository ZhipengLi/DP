using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec09.Decorator61
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle
    {
        private float radius;
        public Circle(float radius)
        {
            this.radius = radius;
        }
        public void Resize(float factor)
        {
            radius *= factor;
        }
        public string AsString() => $"A circle with radius {radius}";
    }
    public class Square : IShape
    {
        private float side;
        public Square(float side)
        {
            this.side = side;
        }
        public string AsString() => $"A square with side {side}";
    }

    public class ColordShape : IShape
    {
        private IShape shape;
        private string color;
        public ColordShape(IShape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transparency;
        public TransparentShape(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.transparency = transparency;
        }

        public string AsString() => $"{shape.AsString()} has {transparency * 100.0}";
    }
    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var square = new Square(1.23f);
            WriteLine(square.AsString());

            var redSquare = new ColordShape(square, "red");
            WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            WriteLine(redHalfTransparentSquare.AsString());
        }
    }
}

