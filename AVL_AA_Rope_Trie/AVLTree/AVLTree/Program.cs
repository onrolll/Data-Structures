using System;

class Program
{
    static void Main(string[] args)
    {
        // Arrange
        AVL<int> avl = new AVL<int>();
        for (int i = 1; i < 10; i++)
        {
            avl.Insert(i);
        }

        avl.Delete(4);
        avl.Delete(2);
        avl.Delete(1);

        var root = avl.Root;

       
       
        avl.EachInOrder(Console.WriteLine);
       
        // Act
      //  avl.DeleteMin();
        // Act
        //avl.EachInOrder(Console.WriteLine);
       
    }
}
