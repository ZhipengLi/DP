using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec08.Composite53
{
    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self,
            IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other))
                return;
            foreach (var from in self)
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }
    public class Neuron : IEnumerable<Neuron>
    {
        public float value;
        public List<Neuron> In, Out;

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    { 
        
    }

    public class NeuronRing : List<Neuron>
    { 
    
    }
    //class Demo1
    //{
    //    static void Main(string[] args)
    //    {
    //        var neuron1 = new Neuron();
    //        var neuron2 = new Neuron();

    //        neuron1.ConnectTo(neuron2);
    //        var layer1 = new NeuronLayer();
    //        var layer2 = new NeuronLayer();

    //        neuron1.ConnectTo(layer1);
    //        layer1.ConnectTo(layer2);
    //        // 
    //        ReadLine();
    //    }
    //}
}
