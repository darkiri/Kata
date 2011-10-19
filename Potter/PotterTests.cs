using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Potter 
{
    [TestFixture]
    public class PotterTests {
        private static void AssertCalculatePrice(Dictionary<Potter, int> soldBooks, double expectedPrice) 
        {
            var calc = new ExpansivePriceCalculator();
            Assert.That(calc.GetPrice(soldBooks), Is.EqualTo(expectedPrice).Within(.000001));
        }

        [Test]
        public void NoBooksNoPrice() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int>(), 0);
        }

        [Test]
        public void SingleBookCostEightEuro() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> { { Potter.Book1, 1 } }, 8);
        }

        [Test]
        public void TwoSameCopiesHaveNoDiscount() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> { { Potter.Book1, 2 } }, 8 * 2);
        }

        [Test]
        public void TwoDifferentBooksHaveDiscount_5Percent() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 2},
                {Potter.Book2, 1},
            }, 2 * 8 * .95 + 8);
        }

        [Test]
        public void ThreeDifferentBooksHaveDiscount_10Percent() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 1},
            }, 3 * 8 * .9 + 2 * 8 * .95);
        }

        [Test]
        public void OnlyDifferentBooksHaveDiscount() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 3},
            }, 3 * 8 * .9 + 3 * 8 * .9 + 1 * 8);
        }

        [Test]
        public void FourDifferentBooksHaveDiscount_20Percent() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 4},
                {Potter.Book2, 3},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
            }, 4 * 8 * .8 + 3 * 8 * .9 + 2 * 8 * .95 + 1 * 8);
        }

        [Test]
        public void FiveDifferentBooksHaveDiscount_25Percent() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 4},
                {Potter.Book2, 2},
                {Potter.Book3, 1},
                {Potter.Book4, 1},
                {Potter.Book5, 1},
            }, 5*8*.75 + 2*8*.95 + 2*8);
        }

        [Test]
        public void FourFourCheaperAsFiveThree() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
                {Potter.Book5, 1},
            }, 4*8*.8 + 4*8*.8);
        }

        [Test]
        public void FourFourFourFourCheaperAsFiveThreeFiveThree() 
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 4},
                {Potter.Book2, 4},
                {Potter.Book3, 4},
                {Potter.Book4, 2},
                {Potter.Book5, 2},
            }, 4*8*.8 + 4*8*.8 + 4*8*.8 + 4*8*.8);
        }

        [Test]
        public void UpTo30BooksShouldBeSold()
        {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 7},
                {Potter.Book2, 7},
                {Potter.Book3, 7},
                {Potter.Book4, 4},
                {Potter.Book5, 5},
            }, 2*5*8*.75 + 5*4*8*.8);
        }

        [Test(Description = "Combinatorial explosion with the greedy calculator")]
        public static void UpTo132BooksShouldBeSold()
        {
            AssertCalculatePrice(new Dictionary<Potter, int> 
            {
                {Potter.Book1, 32},
                {Potter.Book2, 30},
                {Potter.Book3, 30},
                {Potter.Book4, 20},
                {Potter.Book5, 20},
            }, 10*5*8*.75 + 20*4*8*.8 + 2*8);
        }
    }

    [Flags]
    public enum Potter 
    {
        Book1,
        Book2,
        Book3,
        Book4,
        Book5
    }
}