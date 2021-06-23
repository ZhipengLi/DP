using Autofac.Core.Activators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec21.State130
{
    public class Switch
    {
        public State State;
        public Switch()
        {
            this.State = new OffState(this);
        }
        public void On() 
        {
            State.On(this);
        }
        public void Off()
        {
            State.Off(this);
        }
    }

    public abstract class State
    {
        public virtual void On(Switch sw)
        {
            WriteLine("Light is already on.");
        }
        public virtual void Off(Switch sw)
        {
            WriteLine("Light is already off.");
        }
    }

    public class OnState : State
    {
        public OnState(Switch sw)
        {
            WriteLine("light turned on.");
        }
        public override void Off(Switch sw)
        {
            WriteLine("Light turned off.");
            sw.State = new OffState(sw);
        }
    }
    public class OffState : State
    {
        public OffState(Switch sw)
        {
            WriteLine("light turned off.");
        }
        public override void On(Switch sw)
        {
            WriteLine("Light turned on.");
            sw.State = new OnState(sw);
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
            var ls = new Switch();
            ls.On();
            ls.Off();
            ls.Off();
        }
    }
}
