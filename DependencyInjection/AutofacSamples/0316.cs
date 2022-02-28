using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0316
{
    public class Service
    {
        public string DoSomething(int value)
        {
            return $"I have {value}";
        }
    }
    public class DomainObject
    {
        private Service service;
        private int value;
        public delegate DomainObject Factory(int value);
        public DomainObject(Service service, int value)
        {
            this.service = service;
            this.value = value;
        }
        public override string ToString()
        {
            return service.DoSomething(value);
        }
    }
    public class Demo0316
    {
        public static void Main0316(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Service>();
            cb.RegisterType(typeof(DomainObject));

            var container = cb.Build();
            var obj = container.Resolve<DomainObject>(new PositionalParameter(1, 42));
         
            Console.WriteLine(obj);

            var factory = container.Resolve<DomainObject.Factory>();
            var obj2 = factory(42);
            Console.WriteLine(obj2);

            Console.ReadLine();
        }
    }
}
