using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        if (n == 1) return;

        for (int i = n/2 - 1; i >= 0; i--)
        {
            Down(arr,n, i);
        }

        for (int i = n-1; i >= 0; i--)
        {
           
            Swap(arr, 0, i);
            Down(arr, i, 0);
        }
    }

    private static void Down(T[] arr, int sizeHeap, int parentIndex)
    {
        int largest = parentIndex;
        int left = 2 * parentIndex + 1;
        int right = 2 * parentIndex + 2;

        if (left < sizeHeap && arr[left].CompareTo(arr[largest]) > 0)
        {
            largest = left;
        }
        if (right < sizeHeap && arr[right].CompareTo(arr[largest]) > 0)
        {
            largest = right;
        }

        if (largest != parentIndex)
        {
            Swap(arr, largest, parentIndex);
            Down(arr, sizeHeap, largest);
        }

    }

    private static void HeapifyDown(T[] arr, int parentIndex)
    {
        while (parentIndex < arr.Length / 2)
        {
            int childIndex = parentIndex * 2 + 1;
            if (HasRight(arr, childIndex) && IsLess(arr ,childIndex, childIndex + 1))
            {
                childIndex++;
            }

            if (IsLess(arr,childIndex, parentIndex)) break;
            Swap(arr, childIndex, parentIndex);
            parentIndex = childIndex;
        }
    }

    private static void Swap(T[] arr, int childIndex, int parentIndex)
    {
        T child = arr[childIndex];
        arr[childIndex] = arr[parentIndex];
        arr[parentIndex] = child;
    }

    private static bool IsLess(T[] arr, int a, int b)
    {
        if (a<arr.Length && b<arr.Length && arr[a].CompareTo(arr[b]) < 0) return true;
        return false;
    }

    private static bool HasRight(T[] arr, int childIndex)
    {
            if (childIndex / 2 != (childIndex + 1) / 2) return false;
            return true;   
    }
}
