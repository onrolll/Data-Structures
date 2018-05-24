using System;
using System.Collections.Generic;

namespace _2_Calculate_Sequence_With_a_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            var n = int.Parse(Console.ReadLine());

            queue.Enqueue(n);

            for (int i = 0; i < 50; i++)
            {
                var s = queue.Dequeue();
                Console.Write($"{s} ");

                queue.Enqueue(s + 1);
                queue.Enqueue(2 * s + 1);
                queue.Enqueue(s + 2);
            }



        }
    }
}
