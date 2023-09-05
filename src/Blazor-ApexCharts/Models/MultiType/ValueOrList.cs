using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ApexCharts
{
    /// <summary>
    /// Base class to transfer single value or array based data to ApexCharts.js
    /// </summary>
    /// <typeparam name="TProperty">The data type of the property</typeparam>
    public abstract class ValueOrList<TProperty> : IList<TProperty>
    {
        /// <summary>
        /// The underlying collection of items
        /// </summary>
        protected readonly List<TProperty> values = new();

        /// <summary>
        /// Returns the collection as a list
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator List<TProperty>(ValueOrList<TProperty> source)
        {
            return new List<TProperty>(source.values);
        }

#pragma warning disable CS1591 // Primarily for internal use
        public ValueOrList(params TProperty[] list)
        {
            if (list.Any())
            {
                values.AddRange(list);
            }
        }

        public ValueOrList(IEnumerable<TProperty> list)
        {
            if (list != null)
            {
                values.AddRange(list);
            }
        }
#pragma warning restore CS1591

        /// <inheritdoc/>
        public TProperty this[int index] { get => values[index]; set => values[index] = value; }

        /// <inheritdoc/>
        public int Count => values.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(TProperty item)
        {
            values.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            values.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(TProperty item)
        {
            return values.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(TProperty[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<TProperty> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(TProperty item)
        {
            return values.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, TProperty item)
        {
            values.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(TProperty item)
        {
            return values.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            values.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}
