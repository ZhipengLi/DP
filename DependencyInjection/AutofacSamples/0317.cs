using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0317
{
    public class Entity
    {
        public delegate Entity Factory();
        private static Random random = new Random();
        private int number;
        public Entity()
        {
            this.number = random.Next();
        }
        public override string ToString()
        {
            return "test:" + number;
        }
    }
    public class ViewModel
    {
        private Entity.Factory entityFactory;
        public ViewModel(Entity.Factory entityFactor)
        {
            this.entityFactory = entityFactor;
        }
        public void Method()
        {
            var entity = entityFactory();
            Console.WriteLine(entity);
        }
    }
    public class Demo0316
    {
        public static void Main0317(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Entity>().InstancePerDependency();
            cb.RegisterType<ViewModel>();
            var container = cb.Build();
            var vm = container.Resolve<ViewModel>();
            
            vm.Method();
            vm.Method();

            Console.ReadLine();
        }
    }
}
