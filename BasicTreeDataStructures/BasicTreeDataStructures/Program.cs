using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicTreeDataStructures
{
    class Program
    {
        static Dictionary<int, Tree<int>> nodesByValue = new Dictionary<int, Tree<int>>();

        static void Main(string[] args)
        {
            ReadTree();
            //PrintTree(GetRootNode());
            var sortedSetLeaves = new SortedSet<int>();
            GetLeafNodesIncreasingOrder(GetRootNode(), sortedSetLeaves);
            Console.WriteLine(string.Join(' ', sortedSetLeaves));
            GetLeavesIncreasingOrder();
            LeftmostDeepestNode();
            LongestPath();
            AllPathsWithGivenSum();
            SubtreesWithGivenSum();

        }

        private static void SubtreesWithGivenSum()
        {
            Console.WriteLine("Enter a positive integer:");
            var neededSum = int.Parse(Console.ReadLine());

            var root = GetRootNode();
            Console.WriteLine($"Subtrees of sum {neededSum}");
            var stack = new Stack<Tree<int>>();

            int sum = root.Value;
            root.Children.Reverse();
            foreach (var child in root.Children)
            {
                SubtreesWithGivenSum(child, neededSum, stack);
                sum += child.Value;
            }
            if (sum == neededSum)
            {
                stack.Push(root);
            }
            root.Children.Reverse();
            while(stack.Count!=0)
            {
                PrintSubtree(stack.Pop());
            }
       
        }

        private static void PrintSubtree(Tree<int> tree)
        {
            Console.Write($"{tree.Value} ");
            foreach (var child in tree.Children)
            {
                Console.Write($"{child.Value} ");
            }
            Console.WriteLine();
        }

        private static void SubtreesWithGivenSum(Tree<int> tree, int neededSum, Stack<Tree<int>> stack)
        {
            int sum = tree.Value;
            tree.Children.Reverse();
            foreach (var child in tree.Children)
            {
                sum += child.Value;
                SubtreesWithGivenSum(child, neededSum, stack);
            }
            tree.Children.Reverse();
            if (sum == neededSum)
            {
                stack.Push(tree);
            }
        }

        private static void AllPathsWithGivenSum()
        {
            var leaves = nodesByValue.Values
                                     .Where(x => x.Children.Count == 0)
                                     .ToList();
            Console.WriteLine("Enter a positive integer:");
            var neededSum = int.Parse(Console.ReadLine());

            Console.WriteLine($"Paths of sum {neededSum}:");

            foreach (var node in nodesByValue)
            {
                var current = node.Value;
                int sum = 0;
                while (current != null)
                {
                    sum += current.Value;
                    current = current.Parent;
                }
                if(sum == neededSum)
                {
                    PrintPathFromLeaf(node.Value);
                }
            }
        }

        private static void PrintPathFromLeaf(Tree<int> leaf)
        {
            var current = leaf;
            var stack = new Stack<int>();
            while (current != null)
            {
                stack.Push(current.Value);
                current = current.Parent;
            }
           
            Console.WriteLine(string.Join(' ', stack));
        }

        private static void LongestPath()
        {
            var nodes = nodesByValue.Values
                                   .Where(x => x.Children.Count == 0)
                                    .ToList();
            var nodeValue_Depth = new Dictionary<Tree<int>, int>();

            foreach (var node in nodes)
            {
                int count = 0;
                var current = node;
                while (current.Parent != null)
                {
                    count++;
                    current = current.Parent;
                }
                nodeValue_Depth[node] = count;
            }

            var deepestNode = nodeValue_Depth
                                  .OrderByDescending(x => x.Value)
                                  .FirstOrDefault();
            var toTheRoot = deepestNode.Key;
            var stack = new Stack<int>();
            while(toTheRoot != null)
            {
                stack.Push(toTheRoot.Value);
                toTheRoot = toTheRoot.Parent;
            }
            Console.Write("Longest path: ");
            Console.WriteLine(string.Join(' ', stack));

        }

        static void LeftmostDeepestNode()
        {
            var nodes = nodesByValue.Values
                                   .Where(x => x.Children.Count == 0)
                                    .ToList();
            var nodeValue_Depth = new Dictionary<int, int>();

            foreach (var node in nodes)
            {
                int count = 0;
                var current = node;
                while(current.Parent!=null)
                {
                    count++;
                    current = current.Parent;
                }
                nodeValue_Depth[node.Value] = count;
            }

            var deepestNode = nodeValue_Depth
                                  .OrderByDescending(x => x.Value)
                                  .FirstOrDefault();
            Console.WriteLine(deepestNode.Key);
        }

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if(!nodesByValue.ContainsKey(value))
            {
                nodesByValue[value] = new Tree<int>(value);
            }

            return nodesByValue[value];
        }
        public static void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = GetTreeNodeByValue(parent);
            Tree<int> childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }
        static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < nodeCount - 1; i++)
            {
                var edge = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                AddEdge(edge[0], edge[1]);
            }   
        }
        static Tree<int> GetRootNode()
        {
            return nodesByValue.Values.FirstOrDefault(x => x.Parent == null);
        }
        static void PrintTree(Tree<int> node, int indent = 0)
        {
            if(node == null)
            {
                return;
            }

            Console.Write(new string(' ', indent));
            Console.WriteLine(node.Value);

            if(node.Children.Count != 0)
            {
                foreach (var child in node.Children)
                {
                    PrintTree(child, indent + 2);
                }

            }
        }
        static void GetLeafNodesIncreasingOrder(Tree<int> tree,SortedSet<int> sortedSetLeaves)
        {
            //var sortedListOfLeaves = new SortedList<int,Tree<int>>();
            if (tree.Children.Count != 0)
            {
                foreach (var child in tree.Children)
                {
                    GetLeafNodesIncreasingOrder(child,sortedSetLeaves);
                }
            }
            else
            {
                sortedSetLeaves.Add(tree.Value);
            }
        }
        static void GetLeavesIncreasingOrder()
        {
            var leaves = nodesByValue.Values
                                     .Where(x => x.Children.Count == 0)
                                     .Select(x => x.Value)
                                     .OrderBy(x => x)
                                     .ToList();
            Console.WriteLine(string.Join(' ', leaves));
        }
        static void PrintMiddleNodes()
        {
            var nodes = nodesByValue.Values
                                    .Where(x => x.Parent != null && x.Children.Count != 0)
                                    .Select(x => x.Value)
                                    .OrderBy(x => x)
                                    .ToList();
            Console.WriteLine(string.Join(' ', nodes));
        }

    }
}