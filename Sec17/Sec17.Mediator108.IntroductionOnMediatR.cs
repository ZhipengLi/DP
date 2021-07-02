using Autofac;
using MediatR;
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
    public class PingCommadnHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow))
                .ConfigureAwait(false);
        }
    }
    class Demo
    {
        static async Task Main(string[] args)
        {
            await mainAsync();
            ReadLine();
        }
        static async Task mainAsync()
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

            WriteLine($"we got a response at {response.Timestamp}");
        }
    }

}
