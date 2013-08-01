using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ArraySort
{
    [TestFixture]
    public class ArraySortTests
    {
        [TestCase(new int[0], "0")]
        [TestCase(new[] {1}, "1")]
        [TestCase(new[] {2, 1}, "2")]
        [TestCase(new[] {1, 2, 3}, "3")]
        [TestCase(new[] {2, 1, 3}, "3.1")]
        [TestCase(new[] {1, 3, 2}, "3.2")]
        [TestCase(new[] {3, 2, 1}, "3.3")]
        [TestCase(new[] {3, 1, 2}, "3.4")]
        [TestCase(new[] {2, 3, 1}, "3.5")]
        [TestCase(new[] {1, 2, 3, 4}, "4")]
        [TestCase(new[] {4, 3, 1, 2}, "4.1")]
        [TestCase(new[] {4, 3, 2, 1}, "4.2")]
        public void SortTest(int[] array, string dummy)
        {
            var expected = array.OrderBy(i => i).ToArray();
            Assert.AreEqual(Sort1(array), expected);
            //Assert.AreEqual(Sort2(array), expected);
        }

        private int[] Sort1(int[] array)
        {
            var end = array.Length - 1;
            while (end > 0)
            {
                var lastSwap = 0;
                for (var i = 0; i < end; i++)
                {
                    if (i < end && array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        lastSwap = i;
                    }
                }
                end = lastSwap;
            }
            return array;
        }

        private void Swap(int[] array, int index0, int index1)
        {
            var tmp = array[index0];
            array[index0] = array[index1];
            array[index1] = tmp;
        }

        private IEnumerable<int> Sort2(IEnumerable<int> array)
        {
            if (!array.Any())
            {
                return array;
            }
            var cur = array.First();
            var mid = new List<int>();
            var head = new List<int>();
            var tail = new List<int>();

            foreach (var i in array)
            {
                if (i < cur)
                {
                    head.Add(i);
                }
                else if (i > cur)
                {
                    tail.Add(i);
                }
                else
                {
                    mid.Add(i);
                }
            }

            return Sort2(head).Union(mid).Union(Sort2(tail));
        }
    }
}
