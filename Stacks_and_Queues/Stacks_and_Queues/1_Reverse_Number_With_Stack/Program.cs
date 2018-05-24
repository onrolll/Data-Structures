using System;
using System.Collections.Generic;
using System.Linq;

namespace _1_Reverse_Number_With_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var stack = new Stack<int>();
            for (int i = 0; i < input.Length; i++)
            {
                stack.Push(input[i]);
            }
            while(stack.Count!=0)
            {
                Console.Write(stack.Pop());
                Console.Write(" ");
            }
        }   
    }
}
