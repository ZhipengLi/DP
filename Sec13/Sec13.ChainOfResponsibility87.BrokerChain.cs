using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec13.ChainOfResponsibility87
{
    public class Game
    {
        public event EventHandler<Query> Queries;
        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);
        }
    }
    public class Query
    {
        public string CreatureName;
        public enum Argument
        {
            Attack, Defense
        }
        public Argument WhatToQuery;
        public int Value;
        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException();
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature
    {
        private Game game;
        public string Name;
        private int attack, defense;

        public Creature(Game game, string name, int attack, int defense)
        {
            this.game = game;
            this.Name = name;
            this.attack = attack;
            this.defense = defense;
        }
        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }
        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        private Game game;
        protected Creature creature;
        public CreatureModifier(Game game, Creature creature)
        {
            this.game = game;
            this.creature = creature;
            game.Queries += Handle;
        }
        protected abstract void Handle(object sender, Query q);
        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        { }
        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Attack)
                q.Value *= 2;
        }
    }
    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        { }
        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Defense)
                q.Value += 3;
        }
    }

    //============================================================================================
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var game = new Game();
            var goblin = new Creature(game, "strong goblin",3 ,2 );
            WriteLine(goblin);

            using (new DoubleAttackModifier(game, goblin))
            {
                WriteLine(goblin);
                using (new IncreaseDefenseModifier(game, goblin))
                {
                    WriteLine(goblin);
                }
            }
            WriteLine(goblin);

        }
    }
    //============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Assert.IsNotNull(typeof(Game).GetMethod("PerformQuery"));
            Assert.IsNotNull(typeof(Game).GetEvent("Queries"));

            Game game = new Game();
            Query q = new Query("storng goblin", Query.Argument.Attack, 1);
            game.PerformQuery(this, q);

            Assert.IsNotNull(typeof(CreatureModifier).GetMethod("Handle", BindingFlags.Instance | BindingFlags.NonPublic));
            Assert.IsNotNull(typeof(CreatureModifier).GetMethod("Dispose"));

        }
        [Test]
        public void BasicTest()
        {
            var game = new Game();
            var goblin = new Creature(game, "strong goblin", 3, 2);
            Assert.AreEqual("Name: strong goblin, Attack: 3, Defense: 2", goblin.ToString());

            using (new DoubleAttackModifier(game, goblin))
            {
                Assert.AreEqual("Name: strong goblin, Attack: 6, Defense: 2", goblin.ToString());
                using (new IncreaseDefenseModifier(game, goblin))
                {
                    Assert.AreEqual("Name: strong goblin, Attack: 6, Defense: 5", goblin.ToString());
                }
            }
            Assert.AreEqual("Name: strong goblin, Attack: 3, Defense: 2", goblin.ToString());
        }
    }
}


//  namespace Coding.Exercise
//{
//    public abstract class Creature
//    {
//        public Game game;
//        public virtual int Attack { get; set; }
//        public virtual int Defense { get; set; }
//        public Creature(Game game)
//        {
//            this.game = game;
//            //this.game.Creatures.Add(this);
//        }
//    }

//    public class Goblin : Creature
//    {
//        public Goblin(Game game): base(game)
//        {
//            Attack = 1;
//            Defense = 1;
//        }
//        public override int Attack
//        {
//            get => (this.game.Creatures.Any(c => c is GoblinKing)) ? (base.Attack + 1) : base.Attack;
//            set => base.Attack = value;
//        }
//        public override int Defense
//        { 
//            get => base.Defense + this.game.Creatures.Count - 1;
//            set => base.Defense = value;
//        }
//    }

//    public class GoblinKing : Goblin
//    {
//        public GoblinKing(Game game) : base(game)
//        {
//            Attack = 3;
//            Defense = 3;
//        }

//    }

//    public class Game
//    {
//        public IList<Creature> Creatures;
//    }
//}


