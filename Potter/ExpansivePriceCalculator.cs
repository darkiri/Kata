using System;
using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    internal class ExpansivePriceCalculator
    {
        private int _maxPiles;

        public double GetPrice(Dictionary<Potter, int> books)
        {
            _maxPiles = books.Count > 0 ? books.Values.Max() : 0;
            var allPriceVariants = GetAllPriceVariants(books, new Variant[0]).ToArray();
            Console.Out.WriteLine("Variants Count for {0} books: {1}", books.Values.Sum(), allPriceVariants.Count());
            return allPriceVariants.Count() > 0 ? allPriceVariants.Min(v=>v.GetPrice()) : 0;
        }

        public IEnumerable<Variant> GetAllPriceVariants(Dictionary<Potter, int> notYetSorted, IEnumerable<Variant> variants)
        {
            if (notYetSorted.Count > 0)
            {
                var aBook = notYetSorted.Keys.First();
                var remain = notYetSorted
                    .Select(b => new { b.Key, Value = b.Key == aBook ? b.Value - 1 : b.Value })
                    .Where(b => b.Value > 0)
                    .ToDictionary(b => b.Key, b => b.Value);

                return GetAllPriceVariants(remain, GenerateVariants(variants, aBook));
            }
            else
            {
                return variants;
            }
        }

        private IEnumerable<Variant> GenerateVariants(IEnumerable<Variant> variants, Potter aBook)
        {
            var newVariants = new List<Variant>();
            foreach (var curVariant in variants)
            {
                foreach (var newVariant in GenerateVariants(curVariant, aBook, newVariants))
                {
                    newVariants.Add(newVariant);
                }
                if (curVariant.Piles.Count() < _maxPiles)
                {
                    var variantWithASignelBook = new Variant(curVariant.Piles);
                    variantWithASignelBook.Add(new Pile(new[] {aBook}));
                    newVariants.Add(variantWithASignelBook);
                }
            }
            if (newVariants.Count() == 0)
            {
                newVariants.Add(new Variant(new[]{new Pile(new[] {aBook})}));
            }
            return newVariants;
        }

        private static IEnumerable<Variant> GenerateVariants(Variant variant, Potter aBook, IEnumerable<Variant> alreadyGenerated)
        {
            return variant.Piles
                .Where(pile => !pile.Contains(aBook))
                .Select(pile => CreateNewVariant(variant, pile, aBook))
                .Where(newVariant => alreadyGenerated.All(v => !v.Equals(newVariant)));
        }

        private static Variant CreateNewVariant(Variant variant, Pile selectedPile, Potter aBook)
        {
            var newPile = new Pile(selectedPile.Books);
            newPile.Add(aBook);
            var newVariant = new Variant(variant.Piles.Where(p => p != selectedPile));
            newVariant.Add(newPile);
            return newVariant;
        }
    }
}