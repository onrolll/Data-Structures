using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
		// Check if the given item can fit in our quadtree
		if (!item.Bounds.IsInside(this.Bounds))
			return false;

		int depth = 1;
		var currentNode = this.root;

        while (currentNode.Children != null)
		{
			int quadrant = this.GetQuadrant(currentNode, item.Bounds);
			if (quadrant == -1)
			    break;
			currentNode = currentNode.Children[quadrant];
			depth++;
		}

		currentNode.Items.Add(item);
		this.Split(currentNode, depth);
		Count++;
        
		return true;
	}

	private void Split(Node<T> currentNode, int depth)
	{
		// if the node shouldn't split or it has reached the MaxDepth -> stop
		if (currentNode.ShouldSplit || depth == MaxDepth)
			return;
		var leftWidth = currentNode.Bounds.Width / 2;
		var rightWidth = currentNode.Bounds.Width - leftWidth;
		var topHeight = currentNode.Bounds.Height / 2;
		var bottomHeight = currentNode.Bounds.Height - topHeight;

		currentNode.Children = new Node<T>[4];
		currentNode.Children[0] = new Node<T>(currentNode.Bounds.MidX, currentNode.Bounds.MidY, rightWidth, topHeight);
		currentNode.Children[1] = new Node<T>(currentNode.Bounds.X1, currentNode.Bounds.MidY, leftWidth, topHeight);
		currentNode.Children[2] = new Node<T>(currentNode.Bounds.X1, currentNode.Bounds.Y1, leftWidth, bottomHeight);
		currentNode.Children[3] = new Node<T>(currentNode.Bounds.MidX, currentNode.Bounds.Y1, rightWidth, bottomHeight);

		// After spliiting we have to transfer, if they fit, items from our node to one of its children

		// Transfer items from parent to the new nodes

		for (int i = 0; i < currentNode.Items.Count; i++)
		{
			var item = currentNode.Items[i];
			int quadrant = this.GetQuadrant(currentNode, item.Bounds);
			if (quadrant != -1)
			{
				currentNode.Items.RemoveAt(i);
				currentNode.Children[quadrant].Items.Add(item);
				i--;
			}

			// In case all items from currentNode go to only one of its children, attemp to split

			foreach (var child in currentNode.Children)
			{
				this.Split(child, depth + 1);
			}
		}

	}

	private int GetQuadrant(Node<T> currentNode, Rectangle bounds)
	{
		var verticalMid = currentNode.Bounds.MidY;
		var horizontalMid = currentNode.Bounds.MidX;

		bool inFirstQuadrant = verticalMid <= bounds.Y1 && bounds.Y2 <= currentNode.Bounds.Y2
													&& horizontalMid <= bounds.X1 && bounds.X2 <= currentNode.Bounds.X2;
		bool inSecondQuadrant = verticalMid <= bounds.Y1 && bounds.Y2 <= currentNode.Bounds.Y2
													 && horizontalMid >= bounds.X2 && bounds.X1 >= currentNode.Bounds.X1;
		bool inThirdQuadrant = verticalMid >= bounds.Y2 && bounds.Y1 >= currentNode.Bounds.Y1
		                                            && horizontalMid >= bounds.X2 && bounds.X1 >= currentNode.Bounds.X1;
		bool inFourthQuadrant = verticalMid >= bounds.Y2 && bounds.Y1 >= currentNode.Bounds.Y1
		                                             && horizontalMid <= bounds.X1 && bounds.X2 <= currentNode.Bounds.X2;
		if (inFirstQuadrant)
			return 0;
		else if (inSecondQuadrant)
			return 1;
		else if (inThirdQuadrant)
			return 2;
		else if (inFourthQuadrant)
			return 3;
		else return -1;
	}

	public List<T> Report(Rectangle bounds)
    {
		var collisionCandidates = new List<T>();

		this.GetCollisionCandidates(this.root, bounds, collisionCandidates);

		return collisionCandidates;
    }

	private void GetCollisionCandidates(Node<T> root, Rectangle bounds, List<T> collisionCandidates)
	{
		var quadrant = this.GetQuadrant(root, bounds);
		if (quadrant == -1)
			// The bounds do not fit in any quadrant of our root, so we return all items in the root that intersect them
			this.GetSubtreeItems(root, bounds, collisionCandidates);
		else
		{
			if (root.Children != null)
				this.GetCollisionCandidates(root.Children[quadrant], bounds, collisionCandidates);
			foreach (var item in root.Items)
            {
                if (item.Bounds.Intersects(bounds))
                    collisionCandidates.Add(item);
            }
			//collisionCandidates.AddRange(root.Items);
		}
	}

	private void GetSubtreeItems(Node<T> root, Rectangle bounds, List<T> collisionCandidates)
	{
		if (root.Children != null)
		{
            foreach (var child in root.Children)
			{
				if (child.Bounds.Intersects(bounds))
				    this.GetSubtreeItems(child, bounds, collisionCandidates);
			}
		}

		//collisionCandidates.AddRange(root.Items);
        foreach (var item in root.Items)
		{
			if (item.Bounds.Intersects(bounds))
				collisionCandidates.Add(item);
		}
	}

	private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
            return;
       
        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}
