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
namespace Coding.Exercise
{
    public class Participant
    {
        private Mediator mediator;
        public int Value { get; set; }

        public Participant(Mediator mediator)
        {
            mediator.Participants.Add(this);
            this.mediator = mediator;
        }

        public void Say(int n)
        {
            // todo
            this.mediator.Participants.Where(p => p != this).ToList().ForEach(p => p.Increase(n));
        }
        private void Increase(int n)
        {
            this.Value += n;
        }
    }

    public class Mediator
    {
        private List<Participant> participants = new List<Participant>();
        public List<Participant> Participants => this.participants;
    }
}
