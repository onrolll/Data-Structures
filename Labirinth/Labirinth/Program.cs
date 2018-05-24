using System;
using System.Collections.Generic;

namespace Labirinth
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = new int[size, size];
            int[] startPosition = new int[2];

            for (int row = 0; row < size; row++)
            {
                char[] input = Console.ReadLine().ToCharArray();
                for (int col = 0; col < size; col++)
                {
                    switch (input[col])
                    {
                        case '0':
                            matrix[row, col] = 0;
                            break;

                        case 'x':
                            matrix[row, col] = -1;
                            break;

                        case '*':
                            matrix[row, col] = -2;
                            startPosition[0] = row;
                            startPosition[1] = col;
                            break;
                    }

                }


            }

            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(new Cell(startPosition[0], startPosition[1], 0));

            while (queue.Count!=0)
            {
                Cell current = queue.Dequeue();

                if (current.Row+1<size 
                    && matrix[current.Row+1,current.Col] >= 0)
                    //&& current.Value + 1 <= matrix[current.Row + 1, current.Col])
                {
                    if (matrix[current.Row +1,current.Col]==0)
                    {
                        matrix[current.Row + 1, current.Col] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row + 1, current.Col, current.Value + 1));
                    }
                    else if(current.Value+1 <= matrix[current.Row + 1,current.Col])
                    {
                        matrix[current.Row + 1, current.Col] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row + 1, current.Col, current.Value + 1));
                    }
                   
                }
                if (current.Row-1 >= 0 
                    && matrix[current.Row-1,current.Col] >= 0 )
                   // && current.Value + 1 <= matrix[current.Row-1,current.Col])
                {
                    if (matrix[current.Row - 1, current.Col] == 0)
                    {
                        matrix[current.Row - 1, current.Col] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row - 1, current.Col, current.Value + 1));
                    }
                    else if (current.Value + 1 <= matrix[current.Row - 1, current.Col])
                    {
                        matrix[current.Row - 1, current.Col] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row - 1, current.Col, current.Value + 1));
                    }

                }
                if (current.Col + 1 < size
                    && matrix[current.Row,current.Col+1]>=0 )
                    //&& current.Value+1 <= matrix[current.Row, current.Col + 1])
                {
                    if (matrix[current.Row, current.Col+1] == 0)
                    {

                        matrix[current.Row, current.Col + 1] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row, current.Col + 1, current.Value + 1));
                    }
                    else if (current.Value + 1 <= matrix[current.Row, current.Col+1])
                    {
                         matrix[current.Row, current.Col + 1] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row, current.Col + 1, current.Value + 1));
                    }
                }
                if (current.Col - 1 >= 0 
                    && matrix[current.Row, current.Col - 1] >=0 )
                   // && matrix[current.Row, current.Col - 1] >= current.Value+1)
                {
                    if (matrix[current.Row, current.Col - 1] == 0)
                    {
                        matrix[current.Row, current.Col - 1] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row, current.Col - 1, current.Value + 1));
                    }
                    else if (current.Value + 1 <= matrix[current.Row, current.Col - 1])
                    {
                        matrix[current.Row, current.Col - 1] = current.Value + 1;
                        queue.Enqueue(new Cell(current.Row, current.Col - 1, current.Value + 1));
                    }
                }

            }

            for (int row= 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int value = matrix[row, col];
                    if (value == -2)
                    {
                        Console.Write("*");
                    }
                    else if(value== -1)
                    {
                        Console.Write("x");
                    }
                    else if(value == 0)
                    {
                        Console.Write("u");
                    }
                    else
                    {
                        Console.Write($"{matrix[row, col]}");
                    }
                }
                Console.WriteLine();
            }





        }
    }
}
