namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T> where T: IComparable<T>
    {
        public Node<T> Root { get; set; }

        public Dictionary<T, Node<T>> nodesByValue { get; set; }
        public Hierarchy(T root)
        {
            this.Root = new Node<T>(root);
            //this.count = 1;
            this.nodesByValue = new Dictionary<T, Node<T>>();
            this.nodesByValue[root] = this.Root;
        }
        //private int count;

        public int Count => this.nodesByValue.Count;


        public void Add(T element, T child)
        {
            if (this.Contains(child))
            {
                throw new ArgumentException("Child already exists in Hierachy");
            }
            if (!this.Contains(element))
            {
                throw new ArgumentException("No such element in the Hierarchy");
            }
            Node<T> parent = nodesByValue[element];
            Node<T> newChild = new Node<T>(child, parent);
            parent.Children.Add(newChild);
            nodesByValue[child] = newChild;
			
        }

        public void Remove(T element)
        {
            if (this.Root.Value.CompareTo(element)==0)
            {
                throw new InvalidOperationException("Cannot remove Root!");
            }
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("No such element in the Hierarchy!");
            }

            Node<T> disposalNode = this.nodesByValue[element];
            this.nodesByValue.Remove(element);

            // Removing disposalNode from its parent children
            this.nodesByValue[disposalNode.Parent.Value].Children.Remove(disposalNode);

            // New parent for disposalNode's children
            foreach (var child in disposalNode.Children)
            {
                child.Parent = disposalNode.Parent;
            }
            // Adding the children to disposalNode's parent
            nodesByValue[disposalNode.Parent.Value].Children.AddRange(disposalNode.Children);

        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
                throw new ArgumentException("No such item in Hierarchy!");
            return this.nodesByValue[item].Children.Select(x => x.Value);
        }

       
        public T GetParent(T item)
        {
            if (!this.nodesByValue.ContainsKey(item))
                throw new ArgumentException("No such item in Hierarchy!");

            return nodesByValue[item].Parent != null ? nodesByValue[item].Parent.Value : default(T);
        }

        public bool Contains(T value)
        {
            return this.nodesByValue.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var commonElements = new List<T>();
            foreach (var item in this.nodesByValue.Keys)
            {
                if (other.Contains(item))
                    commonElements.Add(item);
            }
            return commonElements;
        } 

        public IEnumerator<T> GetEnumerator()
        {
            var q = new Queue<Node<T>>();

            q.Enqueue(this.Root);

            while (q.Count != 0)
            {
                Node<T> current = q.Dequeue();

                foreach (var child in current.Children)
                {
                    q.Enqueue(child);
                }
                yield return current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}