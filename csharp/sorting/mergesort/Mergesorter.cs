/*
 * Written by Matt Nedrich 
 * mattnedrich@gmail.com
 * www.mattnedrich.com
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace mattnedrich.algorithms.sorting
{
    public class Mergesorter<T> where T : IComparable<T>
    {
        public IEnumerable<T> Sort(IList<T> items)
        {
            if (items.Count <= 1)
                return items;
            else
            {
                Tuple<IList<T>, IList<T>> splitItems = SplitItems(items);
                IEnumerable<T> sortedFirstHalf = sortedFirstHalf = Sort(splitItems.Item1);
                IEnumerable<T> sortedSecondHalf = sortedSecondHalf = Sort(splitItems.Item2);
                // merge lists
                return Merge(sortedFirstHalf, sortedSecondHalf);
            }
        }

        private Tuple<IList<T>, IList<T>> SplitItems(IList<T> items)
        {
            if (items.Count == 0)
                return new Tuple<IList<T>, IList<T>>(new List<T>(), new List<T>());
            else if (items.Count == 1)
                return new Tuple<IList<T>, IList<T>>(items, new List<T>());
            else // split items
            {
                int midPoint = items.Count / 2;
                List<T> firstHalf = items.Take(midPoint).ToList();
                List<T> secondHalf = items.Skip(midPoint).Take(items.Count - midPoint).ToList();
                return new Tuple<IList<T>, IList<T>>(secondHalf, firstHalf);
            }
        }

        private IEnumerable<T> Merge(IEnumerable<T> listA, IEnumerable<T> listB)
        {
            IEnumerator<T> listAEnumerator = listA.GetEnumerator();
            IEnumerator<T> listBEnumerator = listB.GetEnumerator();
            bool listAHasElements = listAEnumerator.MoveNext();
            bool listBHasElements = listBEnumerator.MoveNext();
            while (listAHasElements || listBHasElements)
            {
                if (!listAHasElements)
                {
                    yield return listBEnumerator.Current;
                    listBHasElements = listBEnumerator.MoveNext();
                }
                else if (!listBHasElements)
                {
                    yield return listAEnumerator.Current;
                    listAHasElements = listAEnumerator.MoveNext();
                }
                else // return the smallest element in the two lists
                {
                    if (listAEnumerator.Current.CompareTo(listBEnumerator.Current) < 0)
                    {
                        yield return listAEnumerator.Current;
                        listAHasElements = listAEnumerator.MoveNext();
                    }
                    else
                    {
                        yield return listBEnumerator.Current;
                        listBHasElements = listBEnumerator.MoveNext();
                    }
                }
            }
        }
    }
}