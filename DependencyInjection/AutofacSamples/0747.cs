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

namespace AutofacSamples0747
{
    public interface ICanSpeak
    {
        void Speak();
    }
    public class Person : ICanSpeak
    {
        public void Speak()
        {
            Console.WriteLine("Hello");
        }
    }

    public abstract class BaseHandler
    {
        public virtual string Handle(string message)
        {
            return "Handled: " + message;
        }
    }
    public class HandlerA : BaseHandler
    {
        public override string Handle(string message)
        {
            return "Handled by A: " + message;
        }
    }

    public class HandlerB : BaseHandler
    {
        public override string Handle(string message)
        {
            return "Handled by B: " + message;
        }
    }

    public interface IHandlerFactory
    {
        T GetHandler<T>() where T : BaseHandler;
    }

    class HandlerFactory : IHandlerFactory
    {
        public T GetHandler<T>() where T : BaseHandler
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class ConsumerA
    {
        private HandlerA handlerA;
        public ConsumerA(HandlerA handlerA)
        {
            this.handlerA = handlerA;
        }
        public void DoWork()
        {
            Console.WriteLine(handlerA.Handle("ConsumerA"));
        }
    }

    public class ConsumerB
    {
        private HandlerB handlerB;
        public ConsumerB(HandlerB handlerB)
        {
            this.handlerB = handlerB;
        }
        public void DoWork()
        {
            Console.WriteLine(handlerB.Handle("ConsumerB"));
        }
    }

    public class HandlerRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var swt = service as IServiceWithType;
            if (swt == null 
                || swt.ServiceType == null 
                || !swt.ServiceType.IsAssignableTo<BaseHandler>())
            {
                yield break;
            }

            yield return new ComponentRegistration(
                Guid.NewGuid(),
                new DelegateActivator(
                    swt.ServiceType,
                    (c, p) => {
                        var provider = c.Resolve<IHandlerFactory>();
                        var method = provider.GetType().GetMethod("GetHandler").MakeGenericMethod(swt.ServiceType);
                        return method.Invoke(provider, null);
                    }
                    ),
                new CurrentScopeLifetime(),
                InstanceSharing.None,
                InstanceOwnership.OwnedByLifetimeScope,
                new[] { service },
                new ConcurrentDictionary<string, object>());
        }
        public bool IsAdapterForIndividualComponents => false;
    }

    class Program
    {
        static void Main0747(string[] args)
        {
            var b = new ContainerBuilder();
            //b.RegisterSource(new Autofac.Features.ResolveAnything.AnyConcreteTypeNotAlreadyRegisteredSource());
            b.RegisterType<HandlerFactory>().As<IHandlerFactory>();
            b.RegisterSource(new HandlerRegistrationSource());
            b.RegisterType<ConsumerA>();
            b.RegisterType<ConsumerB>();

            using (var c = b.Build())
            {
                //c.Resolve<Person>().Speak();
                c.Resolve<ConsumerA>().DoWork();
                c.Resolve<ConsumerB>().DoWork();
            }

            //Console.WriteLine(container.Resolve<Child>().Parent);
            Console.ReadLine();
        }
    }
}
