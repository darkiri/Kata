using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Potter {
    [TestFixture]
    public class PotterTests {
        private void AssertCalculatePrice(Dictionary<Potter, int> soldBooks, double expectedPrice) {
            var calc = new PriceCalculator();
            Assert.That(calc.GetPrice(soldBooks), Is.EqualTo(expectedPrice));
        }

        [Test]
        public void NoBooksNoPrice() {
            AssertCalculatePrice(new Dictionary<Potter, int>(), 0);
        }

        [Test]
        public void SingleBookCostEightEuro() {
            AssertCalculatePrice(new Dictionary<Potter, int> { { Potter.Book1, 1 } }, 8);
        }

        [Test]
        public void TwoSameCopiesHaveNoDiscount() {
            AssertCalculatePrice(new Dictionary<Potter, int> { { Potter.Book1, 2 } }, 8 * 2);
        }

        [Test]
        public void TwoDifferentBooksHaveDiscount_5Percent() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 2},
                {Potter.Book2, 1},
            }, 2 * 8 * .95 + 8);
        }

        [Test]
        public void ThreeDifferentBooksHaveDiscount_10Percent() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 1},
            }, 3 * 8 * .9 + 2 * 8 * .95);
        }

        [Test]
        public void OnlyDifferentBooksHaveDiscount() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 2},
                {Potter.Book2, 2},
                {Potter.Book3, 3},
            }, 3 * 8 * .9 + 3 * 8 * .9 + 1 * 8);
        }

        [Test]
        public void FourDifferentBooksHaveDiscount_20Percent() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 4},
                {Potter.Book2, 3},
                {Potter.Book3, 2},
                {Potter.Book4, 1},
            }, 4 * 8 * .8 + 3 * 8 * .9 + 2 * 8 * .95 + 1 * 8);
        }

        [Test]
        public void FiveDifferentBooksHaveDiscount_25Percent() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
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
        public void FourFourFourFourCheaperAsFiveThreeFiveThree() {
            AssertCalculatePrice(new Dictionary<Potter, int> {
                {Potter.Book1, 4},
                {Potter.Book2, 4},
                {Potter.Book3, 4},
                {Potter.Book4, 2},
                {Potter.Book5, 2},
            }, 4*8*.8 + 4*8*.8 + 4*8*.8 + 4*8*.8);
        }
    }

    public enum Potter {
        Book1,
        Book2,
        Book3,
        Book4,
        Book5
    }

    public class PriceCalculator {
        private const double PRICE_PER_ITEM = 8;

        public double GetPrice(Dictionary<Potter, int> books) {
            var sortedPerDiscount = SortBooks(books, new List<Dictionary<Potter, int>>());
            return OptimizeDiscounts(sortedPerDiscount).Sum(s => PricePerPile(s));
        }

        private IEnumerable<Dictionary<Potter, int>> OptimizeDiscounts(IEnumerable<Dictionary<Potter, int>> books) {
            return books.Any(pile => pile.Count == 5) &&
                   books.Any(pile => pile.Count == 3)
                       ? OptimizeDiscounts(Optimize5And3Piles(books))
                       : books;
        }

        private IEnumerable<Dictionary<Potter, int>> Optimize5And3Piles(IEnumerable<Dictionary<Potter, int>> books) {
            var fiveBooksPile = books.First(b => b.Count == 5);
            var threeBooksPile = books.First(b => b.Count == 3);
            var bookToMove = fiveBooksPile.First(b => !threeBooksPile.Keys.Contains(b.Key));
            fiveBooksPile.Remove(bookToMove.Key);
            threeBooksPile[bookToMove.Key] = bookToMove.Value;
            return books;
        }

        private double PricePerPile(Dictionary<Potter, int> books) {
            return books.Sum(b => b.Value*PRICE_PER_ITEM)*GetDiscount(books.Count());
        }

        public IEnumerable<Dictionary<Potter, int>> SortBooks(Dictionary<Potter, int> notYetSorted,
                                                              List<Dictionary<Potter, int>> sortedPerDiscount) {
            var newPile = notYetSorted.ToDictionary(b => b.Key, b => 1);
            var remain = notYetSorted.Where(b => b.Value > 1).ToDictionary(b => b.Key, b => b.Value - 1);

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