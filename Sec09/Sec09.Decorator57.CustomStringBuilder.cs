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

namespace Sec09.Decorator53
{
    public class CodeBuilder
    {
        private StringBuilder builder = new StringBuilder();
        public CodeBuilder Clear()
        {
            builder.Clear();
            return this;
        }
        public CodeBuilder AppendLine(string str)
        {
            builder.AppendLine(str);
            return this;
        }
        public override string ToString()
        {
            return builder.ToString();
        }
       
    }
    //public class Demo
    //{
    //    static void Main(string[] args)
    //    {
    //        var cb = new CodeBuilder();

    //        cb.AppendLine("Class Foo")
    //            .AppendLine("{")
    //            .AppendLine("}");
    //        WriteLine(cb);
    //        ReadLine();
    //    }
    //}
}

