using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Potter
{
    [TestFixture]
    public class PotterTests
    {
        [TestCaseSource("_booksAndPrices")]
        public void CalculatePriceTest(Dictionary<Potter, int> books, double price) {
            var calc = new PriceCalculater();
            Assert.That(calc.GetPrice(books), Is.EqualTo(price));
        }

        private static object[] _booksAndPrices = {
            new object[] {new Dictionary<Potter, int>(), 0},
            new object[] {new Dictionary<Potter, int>{{Potter.Book1, 1}}, 8},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
            }, 8*2},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
                {Potter.Book2, 1},
            }, 2*8*.95 + 8},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
                {Potter.Book2, 2},
            }, 2*8*.95 + 2*8*.95},            
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 1},
            }, 3*8*.9 + 2*8*.95},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 3},
            }, 3*8*.9 + 3*8*.9 + 1*8},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 4},
                {Potter.Book2, 3},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
            }, 4*8*.8 + 3*8*.9 + 2*8*.95 + 1*8},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 4},
                {Potter.Book2, 3},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
                {Potter.Book5, 1},
            }, 5*8*.75 + 3*8*.9 + 2*8*.95 + 1*8},
            new object[] {new Dictionary<Potter, int>{
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
                {Potter.Book5, 1},
            }, 5*8*.75+3*8*.9},
        };
    }

    public enum Potter {
        Book1,
        Book2,
        Book3,
        Book4,
        Book5
    }

    public class PriceCalculater
    {
        private const double PRICE_PER_ITEM = 8;

        public double GetPrice(Dictionary<Potter, int> books) {
            var sortedPerDiscount = SortBooks(books, new List<Dictionary<Potter, int>>());
            return sortedPerDiscount.Sum(s => PricePerPile(s));
        }

        private double PricePerPile(Dictionary<Potter, int> books) {
            return books.Sum(b => b.Value * PRICE_PER_ITEM) * GetDiscount(books.Count());
        }

        public IEnumerable<Dictionary<Potter, int>> SortBooks(Dictionary<Potter, int> notYetSorted, 
                                                              List<Dictionary<Potter, int>> sortedPerDiscount) {
            var newPile = notYetSorted.ToDictionary(b => b.Key, b => 1);
            var remain = notYetSorted.Where(b => b.Value > 1).ToDictionary(b => b.Key, b => b.Value-1);

            sortedPerDiscount.Add(newPile);
            return remain.Count > 0
                ? SortBooks(remain, sortedPerDiscount)
                : sortedPerDiscount;
        }

        public double GetDiscount(int bookCount) {
            if (bookCount == 5) {
                return .75;
            } else if (bookCount == 4) {
                return .80;
            } else if (bookCount == 3) {
                return .90;
            } else if (bookCount == 2) {
                return .95;
            } else {
                return 1;
            }
        }
    }
}
