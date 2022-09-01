using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Studio3.Toolkit
{
    public static class CollectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ContractAnnotation("items:null => halt")]
        [ContractAnnotation("itemAction:null => halt")]
        public static void ForEach<T>(
            [NotNull] this IEnumerable<T> items,
            [NotNull] Action<T> itemAction)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (itemAction == null)
                throw new ArgumentNullException(nameof(itemAction));

            foreach (var item in items)
            {
                itemAction(item);
            }
        }

        [ContractAnnotation("collection:null => halt")]
        [ContractAnnotation("items:null => halt")]
        public static void AddRange<T>(
            [NotNull] this ICollection<T> collection,
            [NotNull] IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
        }

        [NotNull]
        public static IEnumerable<T> AsEnumerable<T>([CanBeNull] this T item)
        {
            yield return item;
        }

        [NotNull]
        [ContractAnnotation("source:null => halt")]
        [ContractAnnotation("selector:null => halt")]
        public static TResult[] SelectArray<TSource, TResult>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToArray();
        }

        [NotNull]
        public static T[] AsArray<T>([CanBeNull] this T item)
        {
            return new[] {item};
        }

        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<TSource>(
            [CanBeNull] this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<TSource>(
            [CanBeNull] this IReadOnlyCollection<TSource> source)
        {
            return source == null || source.Count == 0;
        }

        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<TSource>(
            [CanBeNull] this TSource[] source)
        {
            return source == null || source.Length == 0;
        }

        [NotNull]
        [ContractAnnotation("enumerable:null => halt")]
        public static IEnumerable<T> ExceptItem<T>(
            [NotNull] this IEnumerable<T> enumerable,
            [CanBeNull] T item)
        {
            return enumerable.Where(i => !i.Equals(item));
        }

        [NotNull]
        [ContractAnnotation("enumerable:null => halt")]
        public static IEnumerable<T> WithItem<T>(
            [NotNull] this IEnumerable<T> enumerable,
            [CanBeNull] T item)
        {
            foreach (var element in enumerable)
            {
                yield return element;
            }

            yield return item;
        }

        [ContractAnnotation("enumerable:null => halt")]
        [ContractAnnotation("random:null => halt")]
        public static T RandomElement<T>(
            [NotNull] this IReadOnlyCollection<T> enumerable,
            [NotNull] Random random)
        {
            return enumerable.ElementAt(random.Next(enumerable.Count));
        }
        
        [ContractAnnotation("enumerable:null => halt")]
        public static T RandomElement<T>(
            [NotNull] this IReadOnlyList<T> enumerable)
        {
            return enumerable[UnityEngine.Random.Range(0, enumerable.Count)];
        }
        
        [ContractAnnotation("enumerable:null => halt")]
        public static T RandomElement<T>(
            [NotNull] this IEnumerable<T> enumerable)
        {
            var count = enumerable.Count();
            var randomIndex = UnityEngine.Random.Range(0, count);

            var i = 0;
            foreach (var elem in enumerable)
            {
                if (i++ == randomIndex)
                {
                    return elem;
                }
            }

            return default(T);
        }

        [CanBeNull]
        [ContractAnnotation("enumerable:null => halt")]
        [ContractAnnotation("random:null => halt")]
        public static T RandomElement<T>(
            [NotNull] this List<T> enumerable,
            [NotNull] Random random)
        {
            return enumerable[random.Next(enumerable.Count)];
        }

        [ContractAnnotation("collection:null => halt")]
        [ContractAnnotation("elementToFind:null => halt")]
        public static int IndexOf<T>(
            [NotNull] this IEnumerable<T> collection,
            [NotNull] T elementToFind)
        {
            var i = 0;
            foreach (var element in collection)
            {
                if (Equals(element, elementToFind))
                    return i;
                i++;
            }

            return -1;
        }
        
        [ContractAnnotation("enumerable:null => halt")]
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
        
        
        [ContractAnnotation("list:null => halt")]
        public static void AddUnique<T>(this List<T> list, T newElement)
        {
            if (list.Contains(newElement))
                return;
            list.Add(newElement);
        }
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        [ContractAnnotation("source:null => halt")]
        [ContractAnnotation("rng:null => halt")]
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            return source.ShuffleIterator(rng);
        }

        [ContractAnnotation("source:null => halt")]
        [ContractAnnotation("rng:null => halt")]
        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, 
            Random rng)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
        
        [ContractAnnotation("collection:null => halt")]
        public static T FirstOfType<T>([NotNull] this IEnumerable collection)
        {
            return collection.OfType<T>().FirstOrDefault();
        }
        
        #region HashSet

        /// <summary>
        /// Добавить коллекцию элементов к хеш-сету
        /// </summary>
        /// <param name="hashSet"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        [ContractAnnotation("hashSet:null => halt")]
        [ContractAnnotation("collection:null => halt")]
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                hashSet.Add(item);
            }
        }

        #endregion HashSet
    }
}