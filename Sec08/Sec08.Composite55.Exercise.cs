using System;
using System.Collections;
using System.Collections.Generic;

namespace Coding.Exercise
{
    public interface IValueContainer : IEnumerable<int>
    {
    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return this.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    class Demo
    {
        public static void Main(string[] args)
        {
            ManyValues mv1 = new ManyValues() { 1, 2, 3 };
            ManyValues mv2 = new ManyValues() { 4, 5, 6 };
            SingleValue sv1 = new SingleValue() { Value = 7 };
            List<IValueContainer> list = new List<IValueContainer>() { mv1, mv2, sv1 };
            Console.WriteLine($"the total value is: {list.Sum()}");
            Console.ReadLine();
        }
    }
}
