using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EnumerableExtensionsTask.Tests")]

namespace EnumerableExtensionsTask
{
    /// <summary>
    /// Implements array helper methods.
    /// </summary>
    internal static class BufferData
    {
        /// <summary>
        /// Creates array on base of enumerable sequence.
        /// </summary>
        /// <param name="source">The enumerable sequence.</param>
        /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
        /// <returns>Single dimension zero based array.</returns>
        internal static (T[] buffer, int count) ToArray<T>(IEnumerable<T> source)
        {
            T[] buffer;
            int count;
            if (source is ICollection<T> sourceCollection)
            {
                count = sourceCollection.Count;
                buffer = new T[count];
                sourceCollection.CopyTo(buffer, 0);
                return (buffer, count);
            }

            buffer = Array.Empty<T>();
            count = 0;
            foreach (T element in source)
            {
                if (buffer.Length == 0)
                {
                    Array.Resize(ref buffer, 4);
                }

                if (count == buffer.Length)
                {
                    Array.Resize(ref buffer, count * 2);
                }

                buffer[count++] = element;
            }

            return (buffer, count);
        }
    }
}
