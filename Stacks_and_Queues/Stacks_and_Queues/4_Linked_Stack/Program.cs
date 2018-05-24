using System;
using _Linked_Stack;

namespace _4_Linked_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedStack<char> linkedStack = new LinkedStack<char>();
            linkedStack.Push('a');
            linkedStack.Push('b');
            linkedStack.Push('c');
            linkedStack.Push('d');
            linkedStack.Push('e');

            Console.WriteLine($"linkedStack.Count -> {linkedStack.Count}");

            Console.WriteLine($"linkedStack.Pop() ->{linkedStack.Pop()}");
            Console.WriteLine($"linkedStack.Count -> {linkedStack.Count}");
                   
        }
    }
}
