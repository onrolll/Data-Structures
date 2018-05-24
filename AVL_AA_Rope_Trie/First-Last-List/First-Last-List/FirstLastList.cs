using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{

    public List<T> items { get; set; }

    public SortedDictionary<T, List<int>> dict { get; set; }

    public List<T> itemsNoRemove { get; set; }

    public FirstLastList()
    {
        this.items = new List<T>();
        this.dict = new SortedDictionary<T, List<int>>();
        this.itemsNoRemove = new List<T>();
    }
    public int Count
    {
        get
        {
            return this.items.Count;
        }
    }

    public void Add(T element)
    {
        this.items.Add(element);
        this.itemsNoRemove.Add(element);
        var index = this.itemsNoRemove.LastIndexOf(element);
        if (!dict.ContainsKey(element))
        {
            dict[element] = new List<int>() { 1, index };
        }
        else
        {
            dict[element][0]++;
            dict[element].Add(index);
        }

    }

    public void Clear()
    {
        this.items = new List<T>();
        this.dict.Clear();
        this.itemsNoRemove.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (count > this.items.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        var q = new Queue<T>();
        for (int i = 0; i < count; i++)
        {
            q.Enqueue(this.items[i]);
        }
        return q;
    }

    public IEnumerable<T> Last(int count)
    {
        if (count > this.items.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        var q = new Queue<T>();
        for (int i = this.items.Count - 1; i > this.items.Count - 1 - count; i--)
        {
            q.Enqueue(this.items[i]);
        }
        return q;
    }

    //public IEnumerable<T> Max(int count)
    //{
    //    if (count > this.items.Count)
    //    {
    //        throw new ArgumentOutOfRangeException();
    //    }
    //    return this.items.OrderByDescending(x => x).Take(count);
    //}

    //public IEnumerable<T> Min(int count)
    //{
    //    if (count > this.items.Count)
    //    {
    //        throw new ArgumentOutOfRangeException();
    //    }
    //    return this.items.OrderBy(x => x).Take(count);
    //}
    //public IEnumerable<T> Min(int count)
    //{
    //    if (count > this.items.Count)
    //    {
    //        throw new ArgumentOutOfRangeException();
    //    }
    //    var result = this.dict.Take(count);
    //    var finalQ = new Queue<T>();
    //    var proceed = true;
    //    foreach (var kvp in result)
    //    {
    //        if (!proceed)
    //            break;
    //        for (int i = 0; i < kvp.Value[0]; i++)
    //        {
    //            finalQ.Enqueue(kvp.Key);
    //            if (finalQ.Count == count)
    //            {
    //                proceed = false;
    //                break;
    //            }
    //        }
    //    }
    //    return finalQ;
    //}

    public IEnumerable<T> Max(int count)
    {
        if (count > this.items.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var indexesToBeRetrieved = new List<int>();

        var overEstimateForMaxVal = this.dict.Skip(this.dict.Count-count).Take(count).Reverse();

        bool proceed = true;

        foreach (var kvp in overEstimateForMaxVal)
        {
            if (!proceed) break;

            for (int i = 0; i < kvp.Value[0]; i++)
            {
                indexesToBeRetrieved.Add(kvp.Value[i + 1]);
                if (indexesToBeRetrieved.Count == count)
                {
                    proceed = false;
                    break;
                }
            }
        }
        foreach (var index in indexesToBeRetrieved)
        {
            yield return this.itemsNoRemove[index];
        }

    }
    public IEnumerable<T> Min(int count)
    {
        if (count > this.items.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        var indexesToBeRetrieved = new List<int>();

        var overEstimateForMinVal = this.dict.Take(count);
       
        var proceed = true;
        foreach (var kvp in overEstimateForMinVal)
        {
            if (!proceed)
                break;
            for (int i = 0; i < kvp.Value[0]; i++)
            {
                indexesToBeRetrieved.Add(kvp.Value[i + 1]);
                if (indexesToBeRetrieved.Count == count)
                {
                    proceed = false;
                    break;
                }
            }
        }

        foreach (var index in indexesToBeRetrieved)
        {
            yield return this.itemsNoRemove[index];
        }
    }

    //public int RemoveAll(T element)
    //{
    //    this.itemsNoRemove.Where(x => x.CompareTo(element) == 0).Select(x=> x = default(T));
    //    this.items.RemoveAll(x => x.CompareTo(element) == 0);
    //    if (!this.dict.ContainsKey(element))
    //    {
    //        return 0;
    //    }
    //    var removedCount = this.dict[element][0];
    //    this.dict.Remove(element);
    //    return removedCount;
    //}

    public int RemoveAll(T element)
    {
        if (!this.dict.ContainsKey(element))
            return 0;
       // var indexesToBeRemoved = this.dict[element].Skip(1).ToList();
        var removedCount = this.dict[element][0];
        return removedCount;
        //foreach (var index in indexesToBeRemoved)
        //{
        //    this.items
        //}
    }
}
