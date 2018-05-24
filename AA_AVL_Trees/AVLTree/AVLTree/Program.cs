using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(5);
        tree.Insert(7);
        tree.Insert(6);
        int h = tree.Root.Height;
        tree.Insert(4);
        tree.Insert(5);
        tree.Insert(6);
    }
}
