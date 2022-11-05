using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace app.business.util
{
    /// <summary>
    /// Represents a fixed size last-in-first-out (LIFO) collection of instances of the same specified type.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the stack. </typeparam>
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class LimitedStack<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        /// <summary>
        /// A list used internally to actually hold the items. 
        /// </summary>
        private List<T> _items;

        /// <summary>
        /// Accesses items of the stack by index. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        private int _capacity;
        /// <summary>
        /// The maximum capacity of the stack. Any items added past this threshold, will result in 
        /// oldest entries being silently discarded. 
        /// <br></br>
        /// Negative values are not allowed and cause an exception to be thrown. 
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if the value is set to a negative number. </exception>
        public int Capacity
        {
            get { return _capacity; }
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                
                if (value < _capacity && Count > 0)
                {
                    // Discard old entries, as necessary. 
                    int numberToDiscard = Math.Min(_capacity - value, Count);
                    for (int i = 0; i < numberToDiscard; i++)
                    {
                        _items.RemoveAt(0);
                    }
                }

                _capacity = value;
            }
        }

        /// <summary>
        /// Returns the current count of items in the stack. Read-only. 
        /// </summary>
        public int Count {
            get { return _items.Count; }
            private set { /* Count is read-only. */ }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity">The maximum capacity of the stack. </param>
        /// <exception cref="ArgumentException">Thrown, if the capacity is set to a negative number. </exception>
        public LimitedStack(int capacity)
        {
            _items = new List<T>();
            _capacity = capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Adds a new item to the top of the stack. 
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            // Ensure limit is not exceeded. 
            if (Count + 1 > Capacity)
                _items.RemoveAt(0);

            _items.Add(item);
        }

        /// <summary>
        /// Returns the last added item and removes it from the stack. 
        /// If there is no item, returns default, instead. 
        /// </summary>
        /// <returns>The last added item or default. </returns>
        public T Pop()
        {
            if (Count == 0)
                return default(T);

            T popped = _items.Last();
            _items.RemoveAt(_items.Count - 1);

            return popped;
        }

        /// <summary>
        /// Returns the last added item without removing it from the stack. 
        /// If there is no item, returns default, instead. 
        /// </summary>
        /// <returns>The last added item or default. </returns>
        public T Peek()
        {
            return _items.Last();
        }

        /// <summary>
        /// Clears the stack. 
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
    }
}
