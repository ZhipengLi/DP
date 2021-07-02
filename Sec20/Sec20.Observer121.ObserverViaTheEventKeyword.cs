using Autofac;
using Autofac.Core.Activators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec20.Observer121
{
    public class FallsIllEventArgs : EventArgs
    {
        public string Address;
    }
    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "123 London Road" });
        }
        public event EventHandler<FallsIllEventArgs> FallsIll;

    }

    class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchACold();
            person.FallsIll -= CallDoctor;
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            WriteLine($"A doctor has been called to {e.Address}");
        }
    }

}