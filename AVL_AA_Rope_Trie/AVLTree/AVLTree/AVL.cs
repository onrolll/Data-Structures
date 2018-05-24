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

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void Delete(T v)
    {
        if (this.root==null)
        {
            return;
        }
        this.root = this.Delete(v, this.root);

    }

    private Node<T> Delete(T element, Node<T> root)
    {
        if (root == null)
        {
            return root;
        }
        int root2element = root.Value.CompareTo(element);

        if (root2element > 0)
        {
            root.Left = this.Delete(element, root.Left);
        }
        else if(root2element < 0)
        {
            root.Right = this.Delete(element, root.Right);
        }
        else
        {
            if (root.Left == null)
            {
                return root.Right;
            }
            else if(root.Right == null)
            {
                return root.Left;
            }

            root.Value = this.Min(element, root.Right);
            root.Right = this.Delete(root.Value, root.Right);
        }
        UpdateHeight(root);
        root = Balance(root);
        UpdateHeight(root);

        return root;
    }

    private T Min(T element, Node<T> right)
    {
        while(right.Left != null)
        {
            right = right.Left;
        }
        return right.Value;
    }

    public void DeleteMin(Node<T> node = null)
    {
        Node<T> parent = null;
        if (node == null)
        {
            node = this.root;
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
            {
                this.root = null;
                return;
            }
        }
        while (node.Left != null)
        {
            parent = node;
            node = node.Left;
        }
        parent.Left = node.Right;
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

        node = Balance(node);
        UpdateHeight(node);
        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
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

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }
}
