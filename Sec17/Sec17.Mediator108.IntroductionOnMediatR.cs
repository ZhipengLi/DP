using Autofac;
using MediatR;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec17.Mediator108
{
    // ping
    public class PingCommand : IRequest<PongResponse>
    { 
                    
    }
    public class PongResponse
    {
        public DateTime Timestamp;
        public PongResponse(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }

    //[usedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.Parse("Jan 1, 2019")))
                .ConfigureAwait(false);
        }
    }

    //==============================================================================================
    public class Demo
    {
        static async Task Main(string[] args)
        {
            await mainAsync();
            ReadLine();
        }
        public static async Task<string> mainAsync()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx => 
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(Demo).Assembly)
                .AsImplementedInterfaces();

            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();
            var response = await mediator.Send(new PingCommand());

            // 1/1/2019 12:00:00 AM
            WriteLine($"we got a response at {response.Timestamp}");
            return await Task.FromResult(response.Timestamp.ToString());
        }
    }
    //================================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            PingCommandHandler command = new PingCommandHandler();
            Assert.IsTrue(command is IRequestHandler<PingCommand, PongResponse>);
        }
        [Test]
        public async Task BasicTest()
        {
            var res = await Demo.mainAsync();
            Assert.AreEqual("1/1/2019 12:00:00 AM", res);
        }
    }
}
