using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    public class Variant
    {
        private readonly List<Pile> _piles;
        private int[] _hashes;

        public Variant(IEnumerable<Pile> piles)
        {
            _piles = piles.ToList();
            UpdateHashes();
        }

        public Variant()
        {
            _piles = new List<Pile>();
            UpdateHashes();
        }

        public IEnumerable<Pile> Piles {get { return _piles; }}

        public void Add(Pile newPile)
        {
            _piles.Add(newPile);
            UpdateHashes();
        }

        public double GetPrice()
        {
            return Piles.Sum(s => s.GetPrice());
        }

        public bool Equals(Variant variant)
        {
            if (Piles.Count() != variant.Piles.Count())
            {
                return false;
            }
            
            for (var i = 0; i < _hashes.Count(); i++)
            {
                if (_hashes[i] != variant._hashes[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateHashes()
        {
            _hashes = Piles
                                 .Select(p => p.GetVolatileHash())
                                 .OrderBy(h => h)
                                 .ToArray();
        }
    }
}