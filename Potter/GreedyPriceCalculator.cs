using System;
using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    internal class GreedyPriceCalculator
    {
        public double GetPrice(Dictionary<Potter, int> books)
        {
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

                variants = GenerateVariants(variants, aBook);

                return GetAllPriceVariants(remain, variants);
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
                foreach (var curPile in curVariant.Piles)
                {
                    if (curPile.ShouldAdd(aBook))
                    {
                        var newVariant = CreateNewVariant(curVariant, curPile, aBook);

                        if (newVariants.All(v => !v.Equivalent(newVariant)))
                        {
                            newVariants.Add(newVariant);                            
                        }
                    }
                }
                if (newVariants.All(v => v.Piles.All(p => p.Books.Count(b => b != aBook) > 1)))
                {
                    var variantWithANewSmallPile = new Variant(curVariant.Piles);
                    variantWithANewSmallPile.Add(new Pile(new[] {aBook}));
                    newVariants.Add(variantWithANewSmallPile);
                }
            }
            if (newVariants.Count() == 0)
            {
                newVariants.Add(new Variant(new[]{new Pile(new[] {aBook})}));
            }
            return newVariants;
        }

        private static Variant CreateNewVariant(Variant curVariant, Pile pile2Add, Potter aBook)
        {
            var newPile = new Pile(pile2Add.Books);
            newPile.Add(aBook);
            var newVariant = new Variant(curVariant.Piles.Where(p => p != pile2Add));
            newVariant.Add(newPile);
            return newVariant;
        }
    }
}