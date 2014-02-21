/*
 *  Matt Nedrich
 *  mattnedrich@gmail.com
 *  www.mattnedrich.com
 *  
 *  Fisher-Yates shuffle implementation based on: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mattnedrich.algorithms.randomization
{
    public class FisherYatesShuffler
    {
        private Random random;
        public FisherYatesShuffler(Random random)
        {
            this.random = random;
        }

        public void Shuffle<T>(IList<T> items)
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i);
                T temp = items[randomIndex];
                items[randomIndex] = items[i];
                items[i] = temp;
            }
        }
    }
}