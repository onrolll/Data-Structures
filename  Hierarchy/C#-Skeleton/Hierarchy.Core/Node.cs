using System;
using System.Collections.Generic;

namespace Hierarchy.Core
{
    public class Node<T> where T: IComparable<T>
    {
        public List<Node<T>> Children { get; set; }
        public T Value { get; set; }
        public Node<T> Parent { get; set; }

        public Node(T value, Node<T> parent = null)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<Node<T>>();
        }

        public int CompareTo(T obj)
        {
            return this.Value.CompareTo(obj);
        }
    }
}
