using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(ParentIndex(index), index))
        {
            this.Swap(index, ParentIndex(index));
            index = ParentIndex(index);
        }
    }

    private void Swap(int index, int parentIndex)
    {
        var child = this.heap[index];
        this.heap[index] = this.heap[parentIndex];
        this.heap[parentIndex] = child;
    }

    private int ParentIndex(int index)
    {
        if (index == 0)
        {
            throw new InvalidOperationException("heap[0] has no parent!");
        }
        return index / 2;
    }

    private bool IsLess(int parentIndex, int index)
    {
        if (this.heap[parentIndex].CompareTo(this.heap[index]) < 0) return true;
        return false;
    }

    public T Peek()
    {
        if (this.heap.Count!=0)
        {
            return this.heap[0];
        }
        throw new InvalidOperationException("BinaryHeap is empty!");

    }

    public T Pull()
    {
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException("BinaryHeap is empty!");
        }

        T item = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);
        return item;


    }

    private void HeapifyDown(int parentIndex)
    {
        while (parentIndex < this.heap.Count/2)
        {
            int childIndex = 2 * parentIndex + 1;

            //. checks if parent has a right child, and if so whether its bigger than the left one
            if (HasRight(childIndex) && IsLess(childIndex, childIndex + 1))
            {
                childIndex++;
            }

            if (IsLess(childIndex, parentIndex)) break;
            this.Swap(childIndex, parentIndex);
            parentIndex = childIndex;
        }
    }

    private bool HasRight(int childIndex)
    {
        if (childIndex / 2 != (childIndex + 1) / 2) return false;
        return true;
    }
}
