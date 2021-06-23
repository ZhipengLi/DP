using Autofac.Core.Activators;
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

namespace Sec24.Visitor146
{
    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }
    public class DoubleExpression : Expression 
    {
        private double value;
        public DoubleExpression(double value)
        {
            this.value = value;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append(value);
        }
    }
    public class AdditionExpression : Expression
    {
        private Expression left, right;
        public AdditionExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            left.Print(sb);
            sb.Append("+");
            right.Print(sb);
            sb.Append(")");
        }
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
            var e = new AdditionExpression(
            new DoubleExpression(1),
            new AdditionExpression(
                new DoubleExpression(2),
                new DoubleExpression(3)
                )
                );
            var sb = new StringBuilder();
            e.Print(sb);
            WriteLine(sb);
        }
    }
}
