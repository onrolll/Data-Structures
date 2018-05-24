using System;
using System.Collections.Generic;
namespace _Linked_Stack
{
    public class LinkedStack<T>
    {
        private Node<T> firstNode;
        public int Count { get; private set; }

        public LinkedStack()
        {
            this.Count = 0;
        }
       
        private class Node<T>
        {
            public T Value { get; private set; }
            public Node<T> NextNode { get; set; }
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }
        }
        //TODO Push should add firstNode
        public void Push(T element)
        {
            if (this.Count == 0)
            {
                this.firstNode = new Node<T>(element);
                this.Count++;
            }
            else
            {
                var secondNode = this.firstNode;
                this.firstNode = new Node<T>(element);
                Count++;
               
            }
        }
        //TODO Pop should pop the firstNode!
        public T Pop()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException("linkedStack is empty!");
            }
            else
            {
                Node<T> popedElement = this.firstNode;
                this.firstNode = popedElement.NextNode;
                Count--;
                return popedElement.Value;
            }
        }

    }


}