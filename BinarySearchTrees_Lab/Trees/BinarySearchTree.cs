using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    private class Node
    {
        public Node(T value, Node left = null, Node right = null)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
    public BinarySearchTree()
    {
        this.root = null;
    }
    private BinarySearchTree(Node root)
    {
        this.Copy(root);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }
        Node parent = null;
        Node current = this.root;

        while(current!=null)
        {
            parent = current;
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else return;
        }

        current = new Node(value);
        if (parent.Value.CompareTo(value) > 0)
        {
            parent.Left = current;
        }
        else parent.Right = current;

    }

    public bool Contains(T value)
    {
        var current = this.root;
        while(current!=null)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else break;
        }
        return current != null;

    }

    public void DeleteMin()
    {
        if (this.root==null)
        {
            return;
        }
        Node parent = null;
        Node min = this.root;

        while(min.Left!=null)
        {
            parent = min;
            min = parent.Left;
        }
        //if parent == null this.root has no left child -> this.root is the minimum
        if (parent == null)
        {
            this.root = min.Right;
        }
        // 
        else
        {
            parent.Left = min.Right;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        var current = this.root;
        while(current!=null)
        {
            if (item.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else if (item.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else break;
        }
        if (current==null)
        {
            return null;
        }
        return new BinarySearchTree<T>(current);

    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if(nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if(nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (this.root == null)
        {
            return;
        }
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node parent, Action<T> action)
    {
        if (parent.Left != null)
        {
            this.EachInOrder(parent.Left, action);
        }   
        action(parent.Value);
        if(parent.Right != null)
        {
            this.EachInOrder(parent.Right, action);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

        binarySearchTree.Insert(10);
        binarySearchTree.Insert(5);
        binarySearchTree.Insert(12);
        binarySearchTree.Insert(6);
        binarySearchTree.Insert(4);
        binarySearchTree.Insert(6);
        binarySearchTree.Insert(15);
        binarySearchTree.Insert(14);
        binarySearchTree.Insert(13);
        binarySearchTree.Insert(77);
        binarySearchTree.Search(100);


        binarySearchTree.EachInOrder(Console.WriteLine);


        Console.WriteLine("---------------------------");
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(4);
        bst.EachInOrder(Console.WriteLine);
        bst.DeleteMin();



    }
}
