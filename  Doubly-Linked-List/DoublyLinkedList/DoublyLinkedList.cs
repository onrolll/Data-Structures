using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class Node<T>
    {
        public T Value
        {
            get;
            set;
        }
        public Node<T> NextNode
        {
            get;
            set;
        }
        public Node<T> PreviousNode
        {
            get;
            set;
        }
        public Node(T value)
        {
            this.Value = value;
        }
    }

    private Node<T> head;
    private Node<T> tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if(this.Count == 0)
        {
            this.head = this.tail = new Node<T>(element);
        }
        else
        {
            Node<T> newHead = new Node<T>(element);
            newHead.NextNode = this.head;
            this.head.PreviousNode = newHead;
            this.head = newHead;
        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node<T>(element);
        }
        else
        {
            Node<T> newTail = new Node<T>(element);
			newTail.PreviousNode = this.tail;
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if(this.Count == 0)
        {
            throw new InvalidOperationException("List is empty!");
        }
        T firstElement = this.head.Value;
        this.head = this.head.NextNode;
        if(this.head!=null)
        {
            this.head.PreviousNode = null;
        }
        else
        {
            this.tail = null;
        }
        this.Count--;
        return firstElement;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty!");
        }
        T lastElement = this.tail.Value;
        this.tail = this.tail.PreviousNode;
        if(this.tail != null)
        {
            this.tail.NextNode = null;
        }
        else
        {
            this.head = null;
        }
        this.Count--;
        return lastElement;
    }

    public void ForEach(Action<T> action)
    {
        Node<T> currentNode = this.head;
        while (currentNode!=null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> currentNode = this.head;
        while(currentNode!=null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        T[] arr = new T[this.Count];
        int index = 0;
        Node<T> currentNode = this.head;

        while(currentNode != null)
        {
            arr[index++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }
        return arr;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
