using Autofac.Core.Activators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec26.Bonus157
{
    public enum WorkflowResult
    { 
        Success, Failure
    }
    public class QuadraticEquationSolver
    {
        // ax^2+bx+c == 0
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                result = null;
                return WorkflowResult.Failure;
            }
            //return SolveComplex(a, b, c, disc);
            else
                return SolveSimple(a, b, disc, out result);
        }

        private WorkflowResult SolveSimple(double a, double b, double disc, out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);
            result =  Tuple.Create(
                    new Complex((-b+rootDisc)/(2*a), 0),
                    new Complex((-b-rootDisc)/(2*a), 0)
                );
            return WorkflowResult.Success;
        }

        private Tuple<Complex, Complex> SolveComplex(double a, double b, double c, double disc)
        {
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));
            return Tuple.Create(
                (-b+rootDisc)/(2*a),
                (-b-rootDisc)/(2*a)
                );
        }
    }

    //==============================================================================================
    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var solver = new QuadraticEquationSolver();
            Tuple<Complex, Complex> solution;
            var flag = solver.Start(1, 10, 16, out solution);
            if (flag == WorkflowResult.Success)
            { 
            
            }
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {

        }
    }
}
