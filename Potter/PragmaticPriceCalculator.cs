using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    public class PragmaticPriceCalculator 
    {

        public double GetPrice(Dictionary<Potter, int> books) 
        {
            var sortedPerDiscount = SortBooks(books, new Variant());
            return OptimizeDiscounts(sortedPerDiscount).GetPrice();
        }

        private Variant OptimizeDiscounts(Variant variant) 
        {
            return variant.Piles.Any(pile => pile.Count == 5) &&
                   variant.Piles.Any(pile => pile.Count == 3)
                       ? OptimizeDiscounts(Optimize5And3Piles(variant))
                       : variant;
        }

        private Variant Optimize5And3Piles(Variant variant) 
        {
            var fiveBooksPile = variant.Piles.First(b => b.Count == 5);
            var threeBooksPile = variant.Piles.First(b => b.Count == 3);
            var bookToMove = fiveBooksPile.Books.First(b => !threeBooksPile.Books.Contains(b));
            fiveBooksPile.Remove(bookToMove);
            threeBooksPile.Add(bookToMove);
            return variant;
        }


        public Variant SortBooks(Dictionary<Potter, int> notYetSorted, Variant sortedPerDiscount) 
        {
            var newPile = new Pile(notYetSorted.Select(b => b.Key));
            var remain = notYetSorted.Where(b => b.Value > 1).ToDictionary(b => b.Key, b => b.Value - 1);

            sortedPerDiscount.Add(newPile);
            return remain.Count > 0
                       ? SortBooks(remain, sortedPerDiscount)
                       : sortedPerDiscount;
        }
    }
}