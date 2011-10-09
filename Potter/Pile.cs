using System.Collections.Generic;
using System.Linq;

namespace Potter
{
    public class Pile
    {
        private const double PRICE_PER_ITEM = 8;

        public Pile(IEnumerable<Potter> books)
        {
            _books = books.ToList();
            UpdateHash();
        }

        private void UpdateHash()
        {
            _hash = (int) Books.OrderBy(b => b).Aggregate((Potter) 0, (a, b) => a | b);
        }

        private readonly List<Potter> _books;
        private int _hash;

        public IEnumerable<Potter> Books
        {
            get { return _books; }
        }

        public int Count
        {
            get { return Books.Count(); }
        }

        public void Add(Potter book)
        {
            _books.Add(book);
            UpdateHash();
        }

        public void Remove(Potter book)
        {
            _books.Remove(book);
            UpdateHash();
        }

        public double GetPrice()
        {
            return Books.Sum(b => PRICE_PER_ITEM) * GetDiscount(Count);
        }

        public double GetDiscount(int bookCount)
        {
            if (bookCount == 5)
            {
                return .75;
            }
            else if (bookCount == 4)
            {
                return .80;
            }
            else if (bookCount == 3)
            {
                return .90;
            }
            else if (bookCount == 2)
            {
                return .95;
            }
            else
            {
                return 1;
            }
        }

        public bool Contains(Potter aBook)
        {
            return Books.Contains(aBook);
        }

        public int GetVolatileHash()
        {
            return _hash;
        }
    }
}