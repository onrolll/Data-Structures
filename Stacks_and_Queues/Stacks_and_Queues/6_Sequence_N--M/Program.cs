using System;
using System.Linq;
using System.Collections.Generic;
using _Sequence_NM;

namespace _6_Sequence_N__M
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new Queue<Node>();
         
            var nm = Console.ReadLine().Split().Select(int.Parse).ToArray();

            q.Enqueue(new Node(nm[0], null));

            while (q.Count != 0)
            {
                var node = q.Dequeue();
                if (node.Value < nm[1])
                {
                    q.Enqueue(new Node(node.Value + 1, node));
                    q.Enqueue(new Node(node.Value + 2, node));
                    q.Enqueue(new Node(node.Value * 2, node));
                }
                else if (node.Value == nm[1])
                {
                    PrintSolution(node);
                    return;
                }


            }
            Console.WriteLine("No Solution!");
        }

        private static void PrintSolution(Node node)
        {
            var s = new Stack<int>();
            var currentNode = node;

            while(currentNode != null)
            {
                s.Push(currentNode.Value);
                currentNode = currentNode.PreviousNode;
            }

            Console.WriteLine(string.Join(" -> ", s));
        }
    }
}