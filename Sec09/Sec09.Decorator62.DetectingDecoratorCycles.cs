using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using NUnit.Framework;
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

namespace Sec09.Decorator62
{
    public abstract class Shape
    {
        public virtual string AsString() => "";
    }

    public class Circle : Shape
    {
        private float radius;
        public Circle(float radius)
        {
            this.radius = radius;
        }
        //public void Resize(float factor)
        //{
        //    radius *= factor;
        //}
        public override string AsString() => $"A circle with radius {radius}";
    }

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape shape;
        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecorator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new TCyclePolicy();
        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ColoredShapeThrowOnCycle
            : ShapeDecorator<ColoredShapeThrowOnCycle, ThrowOnCyclePolicy>
            //: ShapeDecorator<ColoredShape, CyclesAllowedPolicy>
            //: ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private string color;
        public ColoredShapeThrowOnCycle(Shape shape, string color) : base(shape)
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

    public class ColoredShapeCyclesAllowed
            : ShapeDecorator<ColoredShapeCyclesAllowed, CyclesAllowedPolicy>
            //: ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private string color;
        public ColoredShapeCyclesAllowed(Shape shape, string color) : base(shape)
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

    public class ColoredShapeCyclesAbsorbed
                : ShapeDecorator<ColoredShapeCyclesAbsorbed, AbsorbCyclePolicy>
    {
        private string color;
        public ColoredShapeCyclesAbsorbed(Shape shape, string color) : base(shape)
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

    //=============================================================================
    public class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShapeCyclesAbsorbed(circle, "red");
            var colored2 = new ColoredShapeCyclesAbsorbed(colored1, "blue");

            WriteLine(colored2.AsString());
        }
    }

    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Assert.IsTrue(typeof(Shape).IsAbstract);
            Assert.IsNotNull(typeof(Shape).GetMethod("AsString"));

            var circle = new Circle(2f);
            Assert.AreEqual("A circle with radius 2", circle.AsString());
            Assert.IsTrue(circle is Shape);

            Assert.IsTrue(typeof(ShapeDecorator).IsAbstract);
            Assert.IsTrue(typeof(ShapeDecorator).IsSubclassOf(typeof(Shape)));

            Assert.IsTrue(typeof(ShapeDecoratorCyclePolicy).IsAbstract);
            Assert.IsTrue(typeof(ShapeDecoratorCyclePolicy).GetMethod("TypeAdditionAllowed").IsAbstract);
            Assert.IsTrue(typeof(ShapeDecoratorCyclePolicy).GetMethod("ApplicationAllowed").IsAbstract);

            Assert.IsTrue(typeof(CyclesAllowedPolicy).IsSubclassOf(typeof(ShapeDecoratorCyclePolicy)));
            Assert.IsTrue(typeof(ThrowOnCyclePolicy).IsSubclassOf(typeof(ShapeDecoratorCyclePolicy)));
            Assert.IsTrue(typeof(AbsorbCyclePolicy).IsSubclassOf(typeof(ShapeDecoratorCyclePolicy)));

            var colored1 = new ColoredShapeThrowOnCycle(circle, "red");
            Assert.IsTrue(colored1 is ShapeDecorator<ColoredShapeThrowOnCycle, ThrowOnCyclePolicy>);
        }
        [Test]
        public void TestThrowOnCycle()
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShapeThrowOnCycle(circle, "red");

            try
            {
                var colored2 = new ColoredShapeThrowOnCycle(colored1, "blue");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidOperationException);
            }
        }
        [Test]
        public void TestOnCyclesAllowed()
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShapeCyclesAllowed(circle, "red");

            var colored2 = new ColoredShapeCyclesAllowed(colored1, "blue");
            Assert.AreEqual("A circle with radius 2 has the color red has the color blue", colored2.AsString());
        }

        //A circle with radius 2 has the color red
        [Test]
        public void TestOnCyclesAbsorbed()
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShapeCyclesAbsorbed(circle, "red");

            var colored2 = new ColoredShapeCyclesAbsorbed(colored1, "blue");
            Assert.AreEqual("A circle with radius 2 has the color red", colored2.AsString());
        }
    }
}

