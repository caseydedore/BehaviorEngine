using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviorEngine.Utilities
{
    public class SequenceBuilder
    {
        private static readonly Random random = new Random();


        public int[] GetSequence(int start, int end, bool sortAscending = false)
        {
            List<int> sequence = new List<int>();
            bool isFlipped = start > end;

            if(isFlipped)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            for (; start <= end; start++)
            {
                sequence.Add(start);
            }

            if (isFlipped && !sortAscending) sequence.Reverse();

            return sequence.ToArray();
        }

        public int[] GetRandomSequence(int start, int end)
        {
            int[] sequence = GetSequence(start, end, true);

            for (int i = 0; i < sequence.Length; i++)
                SwapValuesInCollection(sequence, i, random.Next(i, sequence.Length));

            return sequence;
        }

        public void SwapValuesInCollection<T>(IList<T> collection, int first, int second)
        {
            T temp = collection[first];
            collection[first] = collection[second];
            collection[second] = temp;
        }
    }
}
