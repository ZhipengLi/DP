using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using NUnit.Framework;
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
        public float Value;
        public List<Neuron> In, Out;

        public Neuron(float value)
        {
            this.In = new List<Neuron>();
            this.Out = new List<Neuron>();
            this.Value = value;
        }
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

    //public class NeuronRing : List<Neuron>
    //{ 
    
    //}
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

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            var neuron = new Neuron(0.1f);
            Assert.IsTrue(neuron.Value == 0.1f);
            Assert.IsTrue(neuron.In.Count == 0);
            Assert.IsTrue(neuron.Out.Count == 0);
            Assert.IsTrue(neuron is IEnumerable);
            var layer = new NeuronLayer();
            Assert.IsTrue(layer is Collection<Neuron>);
        }
        [Test]
        public void BasicTest()
        {
            var neuron1 = new Neuron(0.1f);
            var neuron2 = new Neuron(0.2f);

            var neuron3 = new Neuron(0.3f);
            var neuron4 = new Neuron(0.4f);

            var neuron5 = new Neuron(0.5f);
            var neuron6 = new Neuron(0.6f);

            neuron1.ConnectTo(neuron2);
            var layer1 = new NeuronLayer();
            layer1.Add(neuron3);
            layer1.Add(neuron4);

            var layer2 = new NeuronLayer();
            layer2.Add(neuron5);
            layer2.Add(neuron6);

            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);

            Assert.IsTrue(neuron1.Out.Count == 3);
            Assert.IsTrue(layer1.ToList()[0].In.Count == 1);
            Assert.IsTrue(layer2.ToList()[0].In.Count == 2);
        }
    }
}
