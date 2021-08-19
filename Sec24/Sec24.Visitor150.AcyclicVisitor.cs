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

namespace Sec24.Visitor150
{
    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable obj);
    }
    public abstract class Expression
    {
        public abstract void Accept(IVisitor<Expression> visitor);
    }
    public class DoubleExpression : Expression
    {
        public int Value;
        public DoubleExpression(int value)
        {
            this.Value = value;
        }
        public override void Accept(IVisitor<Expression> visitor)
        {
            if (visitor is IVisitor<DoubleExpression> typed)
            {
                typed.Visit(this);
            }
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
        public override void Accept(IVisitor<Expression> visitor)
        {
            if (visitor is IVisitor<AdditionExpression> typed)
            {
                typed.Visit(this);
            }
        }
    }
    public class ExpressionPrinter : IVisitor<Expression>,
        IVisitor<DoubleExpression>,
        IVisitor<AdditionExpression>
    {
        private StringBuilder builder = new StringBuilder();
        public void Visit(Expression e)
        { }
        public void Visit(DoubleExpression e)
        {
            builder.Append(e.Value);
        }
        public void Visit(AdditionExpression e)
        {
            builder.Append("(");
            e.Left.Accept(this);
            builder.Append("+");
            e.Right.Accept(this);
            builder.Append(")");
        }
        public override string ToString()
        {
            return builder.ToString();
        }
    }
    //=============================================================================
    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var e = new AdditionExpression(
                    left: new DoubleExpression(1),
                    right: new AdditionExpression(
                        left: new DoubleExpression(2),
                        right: new DoubleExpression(3)
                        )
                );
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            WriteLine(ep);
        }
    }

    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Assert.IsTrue(typeof(Expression).IsAbstract);
            Assert.IsNotNull(typeof(Expression).GetMethod("Accept"));

            var ep = new ExpressionPrinter();
            //Assert.IsTrue(ep is IVisitor);
            Assert.IsTrue(ep is IVisitor<Expression>);
            Assert.IsTrue(ep is IVisitor<DoubleExpression>);
            Assert.IsTrue(ep is IVisitor<AdditionExpression>);
        }
        [Test]
        public void BasicTest()
        {
            var e = new AdditionExpression(
                        left: new DoubleExpression(1),
                        right: new AdditionExpression(
                            left: new DoubleExpression(2),
                            right: new DoubleExpression(3)
                            )
                    );
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Assert.AreEqual("(1+(2+3))", ep.ToString());
        }
    }
}
