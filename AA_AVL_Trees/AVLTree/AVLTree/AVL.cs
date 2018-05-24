 using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }
    private static int Height(Node<T> node)
    {
        if (node == null)
            return 0;
        return node.Height;
    }
    private static void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }
        UpdateHeight(node);
        node = Balance(node);
        return node;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private static Node<T> RotateLeft(Node<T> node)
    {
        Node<T> right = node.Right;
        node.Right = right.Left;
        right.Left = node;
        //right.Height = node.Height;
        UpdateHeight(node);
        UpdateHeight(node);

        return right;
    }

    private static Node<T> RotateRight(Node<T> node)
    {
        Node<T> left = node.Left;
        node.Left = left.Right;
        left.Right = node;
        //left.Height = node.Height;

        UpdateHeight(node);
        UpdateHeight(left);

        return left;
    }

    private static Node<T> Balance(Node<T> node)
    {
        // if balance < 0 node is right-heavy
        int balance = Height(node.Left) - Height(node.Right);

        if (balance < -1) // right child is too heavy
        {
            balance = Height(node.Right.Left) - Height(node.Right.Right);
            if (balance > 0) // first we have to RR(node.Right) then we do LL(node)
               node.Right = RotateRight(node.Right);

            return RotateLeft(node);
        }
        else if (balance > 1) // left child is too heavy
        {
            balance = Height(node.Left.Left) - Height(node.Left.Right);
            if (balance < 0) // LR(node.Left)
                node.Left = RotateLeft(node.Left);

            return RotateRight(node);
        }
        return node;
    }
}
