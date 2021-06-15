using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using Sec09.Decorator63;
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

namespace Sec09.Decorator63
{
    public abstract class Shape
    {
        public virtual string AsString() => "";
    }

    public class Circle : Shape
    {
        private float radius;
        public Circle() : this(0)
        { }
        public Circle(float radius)
        {
            this.radius = radius;
        }
        public void Resize(float factor)
        {
            radius *= factor;
        }
        public override string AsString() => $"A circle with radius {radius}";
    }
    public class Square : Shape
    {
        private float side;
        public Square() : this(0.0f)
        { }
        public Square(float side)
        {
            this.side = side;
        }
        public override string AsString() => $"A square with side {side}";
    }

    public abstract class ShapeDecotrator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape shape;
        public ShapeDecotrator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecotrator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecotrator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new TCyclePolicy();
        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ColoredShape
            //: ShapeDecorator<ColoredShape, ThrowOnCyclePolicy>
            //: ShapeDecorator<ColoredShape, CyclesAllowedPolicy>
            : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private string color;
        public ColoredShape(Shape shape, string color) : base(shape)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString()
        {
            var sb = new StringBuilder($"{shape.AsString()}");

            if (policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
            {
                sb.Append($" has the color {color}");
            }

            return sb.ToString();
        }
    }

    public class TransparentShape : Shape
    {
        private Shape shape;
        private float transparency;
        public TransparentShape(Shape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has {transparency * 100.0}";
    }

    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }
    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
            {
                throw new InvalidOperationException($"Cycle detected! Type is already a {type.FullName}!");
            }
            return true;
        }
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            throw new NotImplementedException();
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string color;
        private T shape = new T();

        public ColoredShape() : this("black")
        { }
        public ColoredShape(string color)
        {
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float transparency;
        private T shape = new T();

        public TransparentShape() : this(0)
        { }
        public TransparentShape(float transparency)
        {
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has the transparency of {transparency * 100.0}%";
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
            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
            WriteLine(circle.AsString());
        }
    }
}
