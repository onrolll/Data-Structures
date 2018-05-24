using System;
using _Implement_Array_Based_Stack;

namespace _3_Implement_Array_Based_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrStack = new ArrayStack<int>();
            arrStack.Push(1);
            arrStack.Push(10);
            arrStack.Push(999);
            arrStack.Push(30);
            Console.WriteLine(arrStack.Pop());

            var arrFromStack = arrStack.ToArray();
            Console.WriteLine(String.Join(" ", arrFromStack));
        }
    }
}
