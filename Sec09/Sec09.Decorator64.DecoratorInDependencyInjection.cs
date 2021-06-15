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

namespace Sec09.Decorator64
{
    public interface IReportingService
    {
        void Report();
    }
    public class ReportingService : IReportingService
    {
        public void Report()
        {
            WriteLine("Here is your report");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService decorated;
        public ReportingServiceWithLogging(IReportingService decorated)
        {
            this.decorated = decorated;
        }
        public void Report()
        {
            WriteLine("Commencing log...");
            decorated.Report();
            WriteLine("Ending log...");
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
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service),
                    "reporting"
                );

            //b.RegisterType<ReportingServiceWithLogging>().As<IReportingService>();

            using (var c = b.Build()) 
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }

          
        }
    }
}
