using System;
namespace _Implement_Array_Based_Stack
{
    public class ArrayStack<T>
    {
        private T[] elements;
        public int Count { get; private set; }
        private const int InitialCapacity = 16;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
            this.Count = 0;
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
            {
                this.Grow();
            }
            this.elements[this.Count] = element;
            this.Count++;
        }
        public T Pop()
        {
            if(Count == 0 )
            {
                throw new InvalidOperationException("ArrayStack is empty!");
            }
            Count--;
            return this.elements[Count];
        }
        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                arr[i] = this.elements[i];
            }
            return arr;
        }

        private void Grow()
        {
            var newElements = new T[this.elements.Length * 2];
            for (int i = 0; i < this.Count; i++)
            {
                newElements[i] = this.elements[i];
            }
            this.elements = newElements;
        }
    }
}
