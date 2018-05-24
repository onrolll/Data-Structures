using System;
namespace _Sequence_NM
{
    public class Node
    {
        public int Value { get; set; }
        public Node PreviousNode { get; set; }

        public Node(int value, Node previousNode)
        {
            this.Value = value;
            this.PreviousNode = previousNode;
        }
    }
}
