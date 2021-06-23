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

namespace Sec24.Visitor147
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;
    public abstract class Expression
    {
    }
    public class DoubleExpression : Expression
    {
        public double Value;
        public DoubleExpression(double value)
        {
            this.Value = value;
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
    }
    public static class ExpressionPrinter
    {
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression)] = (e,sb) =>
            {
                var ae = (AdditionExpression)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }

        };
        public static void Print(this Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }
        //public static void Print(this Expression e, StringBuilder sb)
        //{
        //    if (e is DoubleExpression de)
        //    {
        //        sb.Append(de.Value);
        //    }
        //    else if (e is AdditionExpression ae)
        //    {
        //        sb.Append("(");
        //        Print(ae.Left, sb);
        //        sb.Append("+");
        //        Print(ae.Right, sb);
        //        sb.Append(")");
        //    }
        //}
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
