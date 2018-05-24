using System.Collections.Generic;
using System;

namespace Hierarchy.Core
{
    public interface IHierarchy<T> : IEnumerable<T> where T : IComparable<T>
    {
        int Count { get; }

        void Add(T element, T child);

        void Remove(T element);

        IEnumerable<T> GetChildren(T element);

        T GetParent(T element);

        bool Contains(T element);

        IEnumerable<T> GetCommonElements(Hierarchy<T> other);
    }
}
