using System;
namespace _Linked_Queue
{
    public class LinkedQueue<T>
    {
        public int Count { get; private set; }
        private Node<T> Tail { get;  set; }
        private Node<T> Head { get; set; }
        private class Node<T>
        {
            public T Value { get; private set; }
            public Node<T> NextNode { get; set; }
            public Node<T> PreviousNode { get; set; }
            public Node(T value, Node<T> nextNode = null, Node<T> prevNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
                this.PreviousNode = prevNode;
            }
        }
        public LinkedQueue()
        {
            this.Count = 0;
        }
        public void Enqueue(T element)
        {
            if (this.Count == 0)
            {
                this.Tail = this.Head = new Node<T>(element);
            }
            else
            {
                Node<T> newTail = new Node<T>(element);
                newTail.PreviousNode = this.Tail;
                this.Tail.NextNode = newTail;
                this.Tail = newTail;
            }
            this.Count++;
        }
        public T Dequeue()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("LinkedQueue is empty!");
            }

            T dequeuedHead = this.Head.Value;
            this.Head = this.Head.NextNode;

            if(this.Head != null)
            {
                this.Head.PreviousNode = null;
            }
            else
            {
                this.Tail = null;
            }
            this.Count--;
            return dequeuedHead;


        }
        public T[] ToArray()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("LinkedQueue is empty!");
            }

            T[] arr = new T[this.Count];
            var node = this.Head;

            for (int i = 0; i < this.Count; i++)
            {
                arr[i] = node.Value;
                node = node.NextNode;
            }

            return arr;
        }
       
    }
}
