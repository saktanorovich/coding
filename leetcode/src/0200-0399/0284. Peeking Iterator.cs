using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0284 {
        public class PeekingIterator {
            private readonly IEnumerator<int> iterator;
            private bool hasNext;
            private bool hasElem;
            private int next;

            // Creates an instance of PeekingIterator.
            public PeekingIterator(IEnumerator<int> iterator) {
                this.iterator = iterator;
                next = iterator.Current;
                hasNext = iterator.MoveNext();
                hasElem = hasNext;
            }
            
            // Returns the next element in the iteration without advancing the iterator.
            public int Peek() {
                return next;
            }
            
            // Returns the next element in the iteration and advances the iterator.
            public int Next() {
                var elem = next;
                next = iterator.Current;
                hasElem = hasNext;
                hasNext = iterator.MoveNext();
                return elem;
            }
            
            // Returns false if the iterator is refering to the end of the array of true otherwise.
            public bool HasNext() {
                return hasElem;
            }
        }
    }
}
