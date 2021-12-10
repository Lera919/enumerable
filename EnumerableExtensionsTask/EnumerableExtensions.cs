using System;
using System.Collections;
using System.Collections.Generic;

namespace EnumerableExtensionsTask
{
    /// <summary>
    /// Enumerable Sequences.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Filters a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="predicate">A
        ///     <see>
        ///         <cref>Func{T, bool}</cref>
        ///     </see>
        ///     to test each element of a sequence for a condition.</param>
        /// <returns>An sequence of elements from the source sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source sequence or predicate is null.</exception>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return WhereIterator(source, predicate);

            static IEnumerable<TSource> WhereIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
            {
                foreach (var element in source)
                {
                    if (predicate(element))
                    {
                        yield return element;
                    }
                }
            }
    }

        /// <summary>
        /// Transforms each element of source sequence from one type to another type by some rule.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of result sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">A <see cref="Func{TSource,TResult}"/> that defines the rule of transformation.</param>
        /// <returns>A sequence, each element of which is transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence or transformer is null.</exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return WhereIterator(source, selector);

            static IEnumerable<TResult> WhereIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
            {
                foreach (var element in source)
                {
                    yield return selector.Invoke(element);
                }
            }
        }

        /// <summary>
        /// Creates an array from a IEnumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>An array that contains the elements from the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence or transformer is null.</exception>
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var (buffer, count) = BufferData.ToArray(source);
            Array.Resize(ref buffer, count);
            return buffer;
        }

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        /// </summary>
        /// /// <typeparam name="T">Type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>true if every element of the source sequence passes the test in the specified predicate,
        /// or if the sequence is empty; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// source - Source can not be null.
        /// or
        /// predicate - Predicate can not be null.
        /// </exception>
        public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (var element in source)
            {
                if (!predicate.Invoke(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>An IEnumerable that contains a range of sequential integral numbers.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">count - Count can not be less than zero.</exception>
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return RangeIterator(start, count);
            static IEnumerable<int> RangeIterator(int start, int count)
            {
                int generated = 0;
                while (generated != count)
                {
                    yield return start++;
                    generated++;
                }
            }
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int count = 0;
            foreach (var element in source)
            {
                count++;
            }

            return count;
        }
        
        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// source - Source can not be null.
        /// or
        /// predicate - Predicate can not be null.
        /// </exception>
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int count = 0;
            foreach (var element in source)
            {
                if (predicate.Invoke(element))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <returns>An IOrderedEnumerable whose elements are sorted according to a key.</returns>
        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key)
        {
            Comparer<TKey> comparer = Comparer<TKey>.Default;
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, key, null);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="key">A function to extract a key from an element.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>An IOrderedEnumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// source - Source can not be null.
        /// or
        /// getKey - Get Key can not be null.
        /// </exception>
        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key, IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                comparer = Comparer<TKey>.Default;
            }

            return Sort(source, key, comparer);
            static IEnumerable<TSource> Sort(IEnumerable<TSource> source, Func<TSource, TKey> key, IComparer<TKey> comparer)
            {
                List<TSource> list = new List<TSource>();
                int count = 0;
                foreach (var element in source)
                {
                    if (count == 0)
                    {
                        list.Add(element);
                        count++;
                        continue;
                    }

                    int j = count - 1;
                    var checkedElement = element;
                    while (j >= 0 && comparer.Compare(key.Invoke(list[j]), key.Invoke(checkedElement)) > 0)
                    {
                        j--;
                    }

                    list.Insert(j + 1, element);
                    count++;
                }

                foreach (var element in list)
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Filters the elements of source sequence based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">Type selector to return.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence that contains the elements from source sequence that have type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OfTypeIterator(source);

            static IEnumerable<TResult> OfTypeIterator(IEnumerable source)
            {
                foreach (var element in source)
                {
                    if (element is TResult result)
                    {
                        yield return result;
                    }
                }
            }
        }
        
        /// <summary>
        /// Filters the elements of source sequence based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">Type selector to return.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence that contains the elements from source sequence that have type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var element in source)
            {
                if (!(element is TResult casted || element is null))
                {
                    throw new InvalidCastException(nameof(element));
                }
            }

            return OfTypeIterator(source);
            static IEnumerable<TResult> OfTypeIterator(IEnumerable elements)
            {
                foreach (var element in elements)
                {
                    yield return (TResult)element;
                }
            }
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of sequence.</typeparam>
        /// <param name="source">A sequence of elements to reverse.</param>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        /// <returns>Reversed source.</returns>
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ReverseIterator(source);
            static IEnumerable<TSource> ReverseIterator(IEnumerable<TSource> source)
            {
                (TSource[] array, int count) = BufferData.ToArray(source);
                Array.Resize(ref array, count);

                if (count == 0)
                {
                    throw new ArgumentException("Source array cannot be empty");
                }

                for (int i = 0; i < count; i++)
                {
                    yield return array[count - 1 - i];
                }
            }
        }

        /// <summary>
        /// Swaps two objects.
        /// </summary>
        /// <typeparam name="T">The type of parameters.</typeparam>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        internal static void Swap<T>(ref T left, ref T right)
        {
            T buf = left;
            left = right;
            right = buf;
        }
    }
}
