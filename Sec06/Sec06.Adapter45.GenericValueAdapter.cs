using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec06.Vector45
{
    // Vector2f, Vector3i

    public interface IInteger
    { 
        int Value { get; }
    }
    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }
        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<TSelf, T, D>
        where D: IInteger, new ()
        where TSelf: Vector<TSelf, T, D>, new ()
    {
        protected T[] data;
        public Vector()
        {
            data = new T[new D().Value];
        }
        public Vector(params T[] values)
        {
            var requiredSzie = new D().Value;
            data = new T[requiredSzie];
            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSzie, providedSize); ++i)
                this.data[i] = values[i];
        }
        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSzie = new D().Value;
            result.data = new T[requiredSzie];
            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSzie, providedSize); ++i)
                result.data[i] = values[i];
            return result;
        }
        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
        public T X
        {
            get => data[0];
            set => data[0] = value;
        }
    }
    public class VectorOfFloat<TSelf, D>
        : Vector<TSelf, float, D>
      where D : IInteger, new()
      where TSelf: VectorOfFloat<TSelf, D>, new()
    {
    }
    public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
        where D: IInteger, new()
    {
        public VectorOfInt()
        { }
        public VectorOfInt(params int[] values) : base(values)
        { 
        
        }
        public static VectorOfInt<D> operator +
            (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
        {
            var result = new VectorOfInt<D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }
            return result;
        }
    }

    public class Vector2i : VectorOfInt<Dimensions.Two>
    {
        public Vector2i()
        { }
        public Vector2i(params int[] values) : base(values)
        { 
        
        }
    }

    public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", data)}";
        }
    }

    class Demo
    {
        //static void Main(string[] args)
        //{
        //    var v = new Vector2i(1, 2);
        //    v[0] = 0;

        //    var vv = new Vector2i(3, 2);

        //    var result = v + vv;

        //    var u = Vector3f.Create(3.5f, 2.2f, 1);

        //    u.ToString()

        //    ReadLine();
        //}
    }
}
