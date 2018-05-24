using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T:IComparable
{
    private Node root;

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
            
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);

    }
    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }
    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        node.Count = 1 + SoftCount(node.Left) + SoftCount(node.Right);
        return node;
    }

    public int SoftCount()
    {
        return SoftCount(this.root);
    }
    public int SoftCount(Node node)
    {
       if (node == null)
        {
            return 0;
        }

        return node.Count;
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
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }
    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }
    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
    }
    
   
    
    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

   

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void DeleteMin()
    {
        
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }

        Node current = this.root;
        Node parent = null;
       
       
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }


        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }

    }
    public T DeleteMin(Node parent, Node node)
    {
        
         if (node == null)
         {
            return default(T);
         }

        Node current = node;


        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        T value = current.Value;
        if (parent == null)
        {
            current = current.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
        return value;
    }



    public void Delete(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
        if (!Contains(element))
        {
            Console.WriteLine("Node with such value does not exist!");
            return;
        }

        this.root = DeleteRec(element, this.root);

    }

    private Node DeleteRec(T element, Node root)
    {
       
        if (root == null)
        {
            return root;
        }

        int compareRoot2Element = root.Value.CompareTo(element);

        if (compareRoot2Element>0)
        {
            root.Left = DeleteRec(element, root.Left);
        }
        else if (compareRoot2Element<0)
        {
            root.Right = DeleteRec(element, root.Right);
        }
        else
        {
           
            // node with 0/1 children
            if (root.Left == null)
                return root.Right;
            else if (root.Right == null)
                return root.Left;

            // node w/ 2 children

            root.Value = minRec(root.Right);

            // Delete the inorder successor
            root.Right = DeleteRec(root.Value, root.Right);

        }
       
        return root;

    }

    public void Delete(T element, Node node)
    {
        if (node == null)
        {
            Console.WriteLine("Node with such value does not exist!");
        }
        int node2element = node.Value.CompareTo(element);
        if (node2element > 0)
        {
            Delete(element, node.Left);
        }
        else if (node2element < 0)
        {
            Delete(element, node.Right);   
        }
        else
        {
            node.Value = minRec(node.Right);

            // Checks if node.Right from the above line existed at all.
            // If not, makes sure that there is node.Left,
            // and assigns it to be the node.
           
            if (node.Value.CompareTo(default(T)) == 0 && node.Left != null)
            {
                node = node.Left;
                return;
            }
            Console.WriteLine($"Count(node) -> {Count(node)}");


        }

    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
        Node current = root;
        Node parent = null;

        while(current.Right != null)
        {
            parent = current;
            current = current.Right;
        }
        if (parent == null)
        {
            this.root = this.root.Left;
        }
        else
        {
            parent.Right = current.Left;
        }
     }
    public void DeleteMaxRecursively()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
        this.root = this.DeleteMaxRecursively(this.root);
    }

    private Node DeleteMaxRecursively(Node node)
    {
        // node is max
        if (node.Right == null)
        {
            return node.Left;
        }
        node.Right = DeleteMaxRecursively(node.Right);
        node.Count--;
        return node;
    }

    public void DeleteMinRecursively()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
        this.root = this.DeleteMinRecursively(this.root);
    }

    private Node DeleteMinRecursively(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = DeleteMinRecursively(node.Left);
        node.Count--;
        return node;
    }

    public int Count()
    {
        int count = Count(this.root, 0);
        return count;
    }

    private int Count(Node node, int count = 0)
    {
            if (node == null)
        {
            return count;
        }
        count++;
        count = Count(node.Left, count);
        count = Count(node.Right, count);
        return count;
    }

   

    public T Select(int rank)
    {
        throw new NotImplementedException();
    }

    public T Ceiling(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }

        Node ceiling = Ceiling(element, this.root);
        if (ceiling == null)
        {
            throw new InvalidOperationException($"No nodes with value smaller than {element} !");
        }
        return ceiling.Value;    }

    private Node Ceiling(T element, Node node)
    {
        if (node == null)
        {
            return node;
        }
        int node2value = node.Value.CompareTo(element);
        if (node2value>0)
        {
            if (node.Left != null && node.Left.Value.CompareTo(element) > 0) return Ceiling(element, node.Left);
            return node;
        }
        else
        {
            return Ceiling(element, node.Right);
        }
    }

    public T Floor(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }

        Node floor = Floor(element, this.root);
        if (floor == null)
        {
            throw new InvalidOperationException($"No nodes with value smaller than {element} !");
        }
        return floor.Value;
    }

    private Node Floor(T element, Node node)
    {
        if(node == null)
        {
            return node;
        }
        int node2element = node.Value.CompareTo(element);

        if (node2element >= 0)
        {
            return Floor(element, node.Left);
        }
        else
        {
            if(node.Right!=null && node.Right.Value.CompareTo(element) < 0) return Floor(element, node.Right);
            
            return node;
        }
    }

    public class Node
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
        public int Count { get; set; }
    }
    public int Rank(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
        int rank = this.Rank(element, this.root);
        return rank;
    }
    public int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }
        int compare = element.CompareTo(node.Value);

        if (compare<0)
        {
            return this.Rank(element, node.Left);
        }
        if (compare>0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }
        //if (node.Left == null) return 0;
        return this.Count(node.Left);
    }

    public Node SelectNodeValueWithNumberOfSmallerElements(int n)
    {
        if (n >= Count(this.root))
        {
            throw new InvalidOperationException("N is too big");
        }
        if (this.root == null)
        {
            throw new InvalidOperationException("BST is empty!");
        }
       Node node = SelectNodeValueWithNumberOfSmallerElements(n, this.root);

        return node;
    }

    public Node SelectNodeValueWithNumberOfSmallerElements(int n, Node node)
    {
        
        if (Count(node.Left) > n)
        {
            return  SelectNodeValueWithNumberOfSmallerElements(n, node.Left);
        }
        else if (Count(node.Left) < n)
        {
            return SelectNodeValueWithNumberOfSmallerElements(n - 1 - Count(node.Left), node.Right);
        }
        return node;
    }
    public T minRec(Node root)
    {
        T minv = root.Value;
        while (root.Left != null)
        {
            minv = root.Left.Value;
            root = root.Left;
        }

        return minv;
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.EachInOrder(Console.WriteLine);
        //   bst.DeleteMax();
       // bst.DeleteMaxRecursively();
        bst.EachInOrder(Console.WriteLine);
       // bst.DeleteMinRecursively();
        bst.EachInOrder(Console.WriteLine);
        int nodeCount = bst.Count();
        Console.WriteLine(nodeCount);

        int softCount = bst.SoftCount();
        Console.WriteLine($"Softcount: {softCount}");

        int rank = bst.Rank(9);
        Console.WriteLine(rank);

        var select = bst.SelectNodeValueWithNumberOfSmallerElements(9);
        Console.WriteLine(select.Value);

        int floor = bst.Floor(5);
        Console.WriteLine($"floor -> {floor}");

        int ceiling = bst.Ceiling(4);
        Console.WriteLine($"ceiling -> {ceiling}");

       bst.Delete(9);
        bst.EachInOrder(Console.WriteLine);
    }
}