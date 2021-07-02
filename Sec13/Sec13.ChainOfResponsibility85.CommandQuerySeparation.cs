using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
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

namespace Sec13.ChainOfResponsibility85
{
    public class Creature
    {
        public string Name;
        public int Attack, Defense;
        public Creature(string name, int attack, int defense)
        {
            this.Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            this.Attack = attack;
            this.Defense = defense;
        }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException();
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null)
                next.Add(cm);
            else
                next = cm;
        }
        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModiifer : CreatureModifier
    {
        public DoubleAttackModiifer(Creature creature) : base(creature)
        { 
        
        }
        public override void Handle()
        {
            WriteLine($"Doubleing {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        { }
        public override void Handle()
        {
        }
    }
    public class IncreasedDefenseModifier : CreatureModifier
    {
        public IncreasedDefenseModifier(Creature creature) : base(creature)
        { }

        public override void Handle()
        {
            WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
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
            var goblin = new Creature("Goblin", 2, 2);
            WriteLine(goblin);

            var root = new CreatureModifier(goblin);
            //root.Add(new NoBonusesModifier(goblin));
            root.Add(new DoubleAttackModiifer(goblin));
            root.Add(new IncreasedDefenseModifier(goblin));

            root.Handle();

            WriteLine(goblin);
           
        }
    }
}

