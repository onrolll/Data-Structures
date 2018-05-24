using System;
using System.Collections.Generic;

interface IBinarySearchTree<T> where T: IComparable
{
    //Basic Tree Operations
    void Insert(T element);
    bool Contains(T element);
    void EachInOrder(Action<T> action);

    //Binary Search Tree Operations
    BinarySearchTree<T> Search(T element);
    void Delete(T element);
    void DeleteMin();
  //  T DeleteMin(Node node);
    void DeleteMax();
    int Count();
    int Rank(T element);
    T Select(int rank);
    T Ceiling(T element);
    T Floor(T element);
    IEnumerable<T> Range(T startRange, T endRange);
}

//public class Node<T>
//{
//    public Node(T value, Node<T> left = null, Node<T> right = null)
//    {
//        this.Value = value;
//        this.Left = left;
//        this.Right = right;
//    }

//    public T Value { get; }
//    public Node<T> Left { get; set; }
//    public Node<T> Right { get; set; }
//    public int Count { get; set; }
//}
