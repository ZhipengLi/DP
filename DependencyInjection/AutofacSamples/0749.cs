using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0749
{
    public interface IReportingService
    {
        void Report();
    }
    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your report");
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
            Console.WriteLine("Commencing log...");
            decorated.Report();
            Console.WriteLine("Ending log...");
        }
    }

    class Program
    {
        static void Main749(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                    (context, service) => new ReportingServiceWithLogging(service), "reporting"
                );
            using (var c = b.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }

                //Console.WriteLine(container.Resolve<Child>().Parent);
                Console.ReadLine();
        }
    }
}
