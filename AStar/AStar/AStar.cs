using System;
using System.Collections.Generic;

public class AStar
{
    public char[,] Map { get; private set; }
    public AStar(char[,] map)
    {
        this.Map = map;      
    }

    public static int GetH(Node current, Node goal)
    {
        int deltaX = Math.Abs(current.Col - goal.Col);
        int deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var q = new PriorityQueue<Node>();
        var child_parent = new Dictionary<Node, Node>();
        var cost = new Dictionary<Node, int>();
        cost[start] = 0;
        child_parent[start] = null;
        q.Enqueue(start);

        while (q.Count!=0)
        {
            var current = q.Dequeue();
            if (current.Row == goal.Row && current.Col == goal.Col)
            {
                break;
            }
            var neighbours = this.GetNeighbours(current);
            foreach (var neighbour in neighbours)
            {
                var newCost = cost[current] + 1;
                if (!cost.ContainsKey(neighbour) || newCost < cost[neighbour])
                {
                    cost[neighbour] = newCost;
                    neighbour.F = cost[neighbour] + GetH(neighbour, goal);
                    q.Enqueue(neighbour);
                    child_parent[neighbour] = current;
                }
               
            }
        }

        return GetPath(child_parent, goal, start);
    }
    private Stack<Node> GetPath(Dictionary<Node, Node> child_parent, Node goal, Node start)
    {
        var result = new Stack<Node>();

        if (!child_parent.ContainsKey(goal))
        {
            result.Push(start);
            return result;
        }
        result.Push(goal);
        var current = child_parent[goal];
        while (current != null)
        {
            result.Push(current);
            current = child_parent[current];
        }
        return result;
    }

    private List<Node> GetNeighbours(Node current)
    {
        var neighbours = new List<Node>();

        AddNeighbour(current.Col - 1, current.Row, neighbours);
        AddNeighbour(current.Col + 1, current.Row, neighbours);
        AddNeighbour(current.Col, current.Row - 1, neighbours);
        AddNeighbour(current.Col, current.Row + 1, neighbours);

        return neighbours;
    }

    private void AddNeighbour(int col, int row, List<Node> neighbours)
    {
        if (col>=0 && col<this.Map.GetLength(1) && row>=0 && row< this.Map.GetLength(0) && NotAWall(row,col))
        {
            neighbours.Add(new Node(row, col));
        }
    }

    private bool NotAWall(int row, int col)
    {
        return this.Map[row, col] != 'W';
    }

    private Node FindGoal(char goal)
    {
        for (int row = 0; row <this.Map.GetLength(0); row++)
        {
            for (int col = 0; col < this.Map.GetLength(1); col++)
            {
                if (this.Map[row, col] == goal)
                {
                    return new Node(row, col);
                }
            }
        }

        throw new ArgumentException("Object not present on map");
    }
}

