using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Autofac;
using static System.Console;
using NUnit.Framework;
using System.Reflection;

namespace Sec17.Mediator107
{
    public class Actor
    {
        public List<string> Messages = new List<string>();
        protected EventBroker broker;
        public Actor(EventBroker broker)
        {
            this.broker = broker;
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; } = 0;
        public void Score()
        {
            GoalsScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalsScored });
        }
        public void AssaultReferee()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
        }
        public FootballPlayer(EventBroker broker, string name) : base(broker)
        {
            this.Name = name;
            broker.OfType<PlayerScoredEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(
                    ps =>
                        {
                            WriteLine($"{name}: Nicely done, {ps.Name}! It's your {ps.GoalsScored}");
                            Messages.Add($"{name}: Nicely done, {ps.Name}! It's your {ps.GoalsScored}");
                        }
                    );
            broker.OfType<PlayerSentOffEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(
                    ps =>
                        {
                            WriteLine($"{name}: see you in the lockers, {ps.Name}");
                            Messages.Add($"{name}: see you in the lockers, {ps.Name}");
                        }
                );
        }
    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker) : base(broker)
        {
            broker.OfType<PlayerScoredEvent>()
                .Subscribe(pe =>
                {
                    if (pe.GoalsScored < 3)
                    {
                        WriteLine($"Coach: well done, {pe.Name}!");
                        Messages.Add($"Coach: well done, {pe.Name}!");
                    }
                });
            broker.OfType<PlayerSentOffEvent>()
                .Subscribe(pe =>
                {
                    if (pe.Reason == "violence")
                    {
                        WriteLine($"Coach: how could you, {pe.Name}");
                        Messages.Add($"Coach: how could you, {pe.Name}");
                    }
                });
        }
    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }
    public class PlayerScoredEvent : PlayerEvent
    {
        public int GoalsScored { get; set; }
    }
    public class PlayerSentOffEvent : PlayerEvent
    {
        public string Reason { get; set; }
    }

    public class EventBroker : IObservable<PlayerEvent>
    {
        private Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();
        public IDisposable Subscribe(IObserver<PlayerEvent> observer)
        {
            return subscriptions.Subscribe(observer);
        }

        public void Publish(PlayerEvent pe)
        {
            subscriptions.OnNext(pe);
        }
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
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();

            cb.Register((c, p) => new FootballPlayer(
                    c.Resolve<EventBroker>(),
                    p.Named<string>("name")
                ));
            using (var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Score();
                player1.Score();
                player1.Score();
                player1.AssaultReferee();
                player2.Score();

            }
        }
    }

    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            var broker = new EventBroker();
            var actor = new Actor(broker);
            Assert.IsNotNull(actor.Messages);
            Assert.IsTrue(broker is IObservable<PlayerEvent>);

            PlayerEvent pe = new PlayerEvent { Name = "John" };
            PlayerSentOffEvent pso = new PlayerSentOffEvent { Reason = "violence" };
            PlayerScoredEvent pse = new PlayerScoredEvent { GoalsScored = 1 };

            Assert.AreEqual("Subject`1", broker.GetType().GetField("subscriptions", BindingFlags.NonPublic | BindingFlags.Instance).FieldType.Name);
        }

        [Test]
        public void BasicTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();

            cb.Register((c, p) => new FootballPlayer(
                    c.Resolve<EventBroker>(),
                    p.Named<string>("name")
                ));
            using (var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Score();
                Assert.AreEqual("Coach: well done, John!", coach.Messages.LastOrDefault());
                Assert.AreEqual("Chris: Nicely done, John! It's your 1", player2.Messages.LastOrDefault());

                player1.Score();
                Assert.AreEqual("Coach: well done, John!", coach.Messages.LastOrDefault());
                Assert.AreEqual("Chris: Nicely done, John! It's your 2", player2.Messages.LastOrDefault());

                player1.Score();
                Assert.AreEqual("Chris: Nicely done, John! It's your 3", player2.Messages.LastOrDefault());

                player1.AssaultReferee();
                Assert.AreEqual("Coach: how could you, John", coach.Messages.LastOrDefault());
                Assert.AreEqual("Chris: see you in the lockers, John", player2.Messages.LastOrDefault());

                player2.Score();
                Assert.AreEqual("Coach: well done, Chris!", coach.Messages.LastOrDefault());
                Assert.AreEqual("John: Nicely done, Chris! It's your 1", player1.Messages.LastOrDefault());
            }
        }
    }
}

