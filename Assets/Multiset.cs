using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class Multiset<T> : IEnumerable<KeyValuePair<T, uint>>
    {
        private SortedDictionary<T, uint> set;

        // Constructor
        public Multiset()
        {
            SortedDictionary<T, uint> newSet = new SortedDictionary<T, uint>();
        }

        public void AddCards(T card, uint copies)
        {
            if (set.ContainsKey(card))
                set[card] += copies;
            else
                set.Add(card, copies);
        }

        public bool RemoveCards(T card, uint copies)
        {
            if (set.ContainsKey(card))
            {
                set[card] -= copies;
                if (set[card] <= 0)
                    set.Remove(card);
                return true;
            }
            else
                return false;
        }

        public uint GetCopies(T card)
        {
            if (set.ContainsKey(card))
                return set[card];
            else
                return 0;
        }

        public bool Contains(T t)
        {
            return set.ContainsKey(t);
        }

        public uint this[T t]
        {
            get { return set[t]; }
            set { GetCopies(t); }
        }

        public IEnumerator GetEnumerator()
        {
            return set.GetEnumerator();
        }
        IEnumerator<KeyValuePair<T, uint>> IEnumerable<KeyValuePair<T, uint>>.GetEnumerator()
        {
            return set.GetEnumerator();
        }
    }
}