using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Coding.Exercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                yield return this.Value;
                if (this.Left != null)
                {
                    foreach (var t in this.Left.PreOrder)
                        yield return t;
                }
                if (this.Right != null)
                {
                    foreach (var t in this.Right.PreOrder)
                        yield return t;
                }
            }
        }
    }
}

