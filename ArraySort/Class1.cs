using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ArraySort
{
    [TestFixture]
    public class ArraySortTests
    {
        [TestCase(new int[0], new int[0], "0")]
        [TestCase(new[] {1}, new[] {1}, "1")]
        [TestCase(new[] {2, 1}, new[] {1, 2}, "2")]
        [TestCase(new[] {1, 2, 3}, new[] {1, 2, 3}, "3")]
        [TestCase(new[] {2, 1, 3}, new[] {1, 2, 3}, "3.1")]
        [TestCase(new[] {1, 3, 2}, new[] {1, 2, 3}, "3.2")]
        [TestCase(new[] {3, 2, 1}, new[] {1, 2, 3}, "3.3")]
        [TestCase(new[] {3, 1, 2}, new[] {1, 2, 3}, "3.4")]
        [TestCase(new[] {2, 3, 1}, new[] {1, 2, 3}, "3.5")]
        [TestCase(new[] {1, 2, 3, 4}, new[] {1, 2, 3, 4}, "4")]
        [TestCase(new[] {1, 2, 4, 3}, new[] {1, 2, 3, 4}, "4.1")]
        [TestCase(new[] {2, 1, 3, 4}, new[] {1, 2, 3, 4}, "4.2")]
        [TestCase(new[] {2, 1, 4, 3}, new[] {1, 2, 3, 4}, "4.3")]
        [TestCase(new[] {1, 3, 2, 4}, new[] {1, 2, 3, 4}, "4.4")]
        [TestCase(new[] {1, 3, 4, 2}, new[] {1, 2, 3, 4}, "4.5")]
        [TestCase(new[] {3, 1, 2, 4}, new[] {1, 2, 3, 4}, "4.6")]
        [TestCase(new[] {3, 1, 4, 2}, new[] {1, 2, 3, 4}, "4.7")]
        [TestCase(new[] {1, 4, 2, 3}, new[] {1, 2, 3, 4}, "4.8")]
        [TestCase(new[] {1, 4, 3, 2}, new[] {1, 2, 3, 4}, "4.9")]
        [TestCase(new[] {4, 1, 2, 3}, new[] {1, 2, 3, 4}, "4.10")]
        [TestCase(new[] {4, 1, 3, 2}, new[] {1, 2, 3, 4}, "4.11")]
        [TestCase(new[] {2, 3, 1, 4}, new[] {1, 2, 3, 4}, "4.12")]
        [TestCase(new[] {2, 3, 4, 1}, new[] {1, 2, 3, 4}, "4.13")]
        [TestCase(new[] {3, 2, 1, 4}, new[] {1, 2, 3, 4}, "4.14")]
        [TestCase(new[] {3, 2, 4, 1}, new[] {1, 2, 3, 4}, "4.15")]
        [TestCase(new[] {2, 4, 1, 3}, new[] {1, 2, 3, 4}, "4.16")]
        [TestCase(new[] {2, 4, 3, 1}, new[] {1, 2, 3, 4}, "4.17")]
        [TestCase(new[] {4, 2, 1, 3}, new[] {1, 2, 3, 4}, "4.18")]
        [TestCase(new[] {4, 2, 3, 1}, new[] {1, 2, 3, 4}, "4.19")]
        [TestCase(new[] {3, 4, 1, 2}, new[] {1, 2, 3, 4}, "4.20")]
        [TestCase(new[] {3, 4, 2, 1}, new[] {1, 2, 3, 4}, "4.21")]
        [TestCase(new[] {4, 3, 1, 2}, new[] {1, 2, 3, 4}, "4.22")]
        [TestCase(new[] {4, 3, 2, 1}, new[] {1, 2, 3, 4}, "4.23")]
        public void SortTest(int[] array, int[] expected, string dummy)
        {
            counter = 0;
            Assert.AreEqual(Sort1(array, 0, array.Length - 1), expected);
            //Assert.AreEqual(Sort2(array), expected);
            Console.Out.WriteLine(counter);
        }

        private const int N = 10000;
        [Test]
        public void LongTest1()
        {
            var array = MakeBigArray(N);
            var expected = array.OrderBy(i => i).ToArray();
            var t = Stopwatch.StartNew();
            Assert.That(Sort1(array, 0, array.Length-1), Is.EqualTo(expected));
            Console.Out.WriteLine(t.ElapsedMilliseconds);
        }

        [Test]
        public void LongTest2()
        {
            var array = MakeBigArray(N);
            var expected = array.OrderBy(i => i).ToArray();
            var t = Stopwatch.StartNew();
            Assert.That(Sort2(array), Is.EqualTo(expected));
            Console.Out.WriteLine(t.ElapsedMilliseconds);
        }

        private int[] MakeBigArray(int size)
        {
            var r = new Random();
            return Enumerable.Range(0, size).Select(_ => r.Next()).ToArray();
        }

        private static int counter;
        private int[] Sort1(int[] array, int start, int end)
        {
            while (end > start)
            {
                for (var i = start; i <= end; i++)
                {
                    if (i < end && array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                    }
                    counter++;
                }
                end--;
            }
            return array;
        }

        private void Swap(int[] array, int index0, int index1)
        {
            var tmp = array[index0];
            array[index0] = array[index1];
            array[index1] = tmp;
        }

        private int[] Sort2(int[] array)
        {
            if (array.Length < 1)
            {
                return array;
            }
            var cur = array[0];
            var mid = new List<int>();
            var head = new List<int>();
            var tail = new List<int>();

            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] < cur)
                {
                    head.Add(array[i]);
                }
                else if (array[i] > cur)
                {
                    tail.Add(array[i]);
                }
                else
                {
                    mid.Add(array[i]);
                }
                counter++;
            }

            return Sort2(head.ToArray()).Union(mid).Union(Sort2(tail.ToArray())).ToArray();
        }
    }
}
