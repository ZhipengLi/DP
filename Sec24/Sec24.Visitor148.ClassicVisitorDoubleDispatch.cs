using Autofac.Core.Activators;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec24.Visitor148
{
    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }
    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }
    public class DoubleExpression : Expression
    {
        public double Value;
        public DoubleExpression(double value)
        {
            this.Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispatch
            visitor.Visit(this);
        }
    }
    public class AdditionExpression : Expression
    {
        public Expression Left, Right;
        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispatch
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter : IExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();
        public void Visit(DoubleExpression de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }
        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;
        public void Visit(DoubleExpression de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
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
            var e = new AdditionExpression(
            new DoubleExpression(1),
            new AdditionExpression(
                new DoubleExpression(2),
                new DoubleExpression(3)
                )
                );
            //var sb = new StringBuilder();
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            WriteLine(ep);

            var calc = new ExpressionCalculator();
            calc.Visit(e);
            WriteLine($"{ep} = {calc.Result}");
        }
    }

    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {
            Assert.IsTrue(typeof(IExpressionVisitor).IsInterface);
            Assert.IsTrue(typeof(Expression).IsAbstract);
            Assert.IsNotNull(typeof(Expression).GetMethod("Accept"));
            Assert.IsTrue(new DoubleExpression(1) is Expression);
            Assert.IsTrue(new ExpressionPrinter() is IExpressionVisitor);
            Assert.IsTrue(new ExpressionCalculator() is IExpressionVisitor);
        }
        [Test]
        public void ClassTest()
        {
            var e = new AdditionExpression(
                        new DoubleExpression(1),
                        new AdditionExpression(
                            new DoubleExpression(2),
                            new DoubleExpression(3)
                            )
                    );
            //var sb = new StringBuilder();
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Assert.AreEqual("(1+(2+3))", ep.ToString());

            var calc = new ExpressionCalculator();
            calc.Visit(e);
            Assert.AreEqual(6, calc.Result);
        }
    }
}
