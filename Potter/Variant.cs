using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    public class Variant
    {
        private readonly List<Pile> _piles;

        public Variant(IEnumerable<Pile> piles)
        {
            _piles = piles.ToList();
        }

        public Variant()
        {
            _piles = new List<Pile>();
        }

        public IEnumerable<Pile> Piles {get { return _piles; }}

        public void Add(Pile newPile)
        {
            _piles.Add(newPile);
        }

        public double GetPrice()
        {
            return Piles.Sum(s => s.GetPrice());
        }

        public bool Equivalent(Variant variant)
        {
            if (Piles.Count() != variant.Piles.Count())
            {
                return false;
            }
            var myHashes = Piles
                .Select(p => p.GetHash())
                .OrderBy(h => h)
                .ToArray();
            var otherHashes = variant.Piles
                .Select(p => p.GetHash())
                .OrderBy(h => h)
                .ToArray();
            for (var i = 0; i < myHashes.Count(); i++)
            {
                if (myHashes[i] != otherHashes[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}