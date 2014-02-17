/* 
 * Written by Matt Nedrich
 * mattnedrich@gmail.com
 * www.mattnedrich.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mattnedrich.algorithms.sorting
{
    public class Quicksorter<T> where T : IComparable<T>
    {
        protected Random random;

        public Quicksorter()
        {
            random = new Random();
        }

        public void Sort(IList<T> items)
        {
            Partition(items, 0, items.Count-1);
        }

        private void Partition(IList<T> items, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
                return;

            int pivotIndex = ChoosePivotIndex(items, startIndex, endIndex);
            T pivot = items[pivotIndex];
            Swap(items, startIndex, pivotIndex);
            int partitionIndex = startIndex + 1;
            for (int frontierIndex = partitionIndex; frontierIndex <= endIndex; frontierIndex++)
            {
                if (items[frontierIndex].CompareTo(pivot) < 0)
                {
                    Swap(items, frontierIndex, partitionIndex);
                    partitionIndex++;
                }
            }
            // put pivot back
            items[startIndex] = items[partitionIndex - 1];
            items[partitionIndex - 1] = pivot;

            // recursively sort left half
            Partition(items, startIndex, partitionIndex - 2);
            // recursively sort right half
            Partition(items, partitionIndex, endIndex);
        }

        protected virtual int ChoosePivotIndex(IList<T> items, int startIndex, int endIndex)
        {
            return random.Next(startIndex, endIndex);
        }

        private void Swap(IList<T> items, int aIndex, int bIndex)
        {
            T temp = items[aIndex];
            items[aIndex] = items[bIndex];
            items[bIndex] = temp;
        }
    }
}
