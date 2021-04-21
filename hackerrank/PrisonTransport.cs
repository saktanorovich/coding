/**
There are N inmates numbered between 1, N in a prison. These inmates
have superhuman strength because they have drunk a special concoction
made by Dr. Evil. They have to be transported by some buses to a new
facility. But they are bound by special chains which are made from
strong carbon fibres. Each inmate is either chained alone or is
chained in a group along with one or more inmates. A group of inmates
are those who are directly or indirectly connected to each other.
Only one group can be transported per bus.

There are buses which will charge fixed amount bucks for transferring
inmates. Charges are directly proportional to the capacity of bus.
If a bus charge K bucks then it can carry upto K^2 inmates at one time.
Buses are available for all positive integral cost ranging from [1,2,3,..].
A bus can be used multiple times, and each time it will charge. Note
that a bus can also transfer less number of inmates than it's capacity.

Find the minimal cost to transport all the inmates.

Input: The first line contains N representing the number of inmates.
The second line contains another integer M - number of pairs of inmates
who are handcuffed together. Then follows M lines. Each of these
lines contains two integers P Q, which means inmate numbered P is
handcuffed to inmate numbered Q.

Output: For the given arrangement print the minimal cost which can be
incurred while transferring inmates.

Constraints:
  2 ≤ N ≤ 100'000
  1 ≤ M ≤ min(N*(N-1)/2, 100'000)
  1 ≤ P, Q ≤ N
  P ≠ Q

Sample Input
4
2
1 2
1 4

Sample Output
3
 * 
Explanation 
Inmates #1, #2, #4 are connected to each other (1--2--4) so they lies in
a single group. So a bus of cost 2 (with capacity 2^2 = 4) is required to
carry them. Inmate #3 is not handcuffed with anyother. So he can be
transported in a bus of cost 1 (with capacity 1^2 = 1).
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace interview.hackerrank {
    public class PrisonTransport {
        public int minimalCost(int numOfInmates, string[] handcuffed) {
            var dss = new DisjointSetsSystem<int>(numOfInmates);
            for (var inmate = 1; inmate <= numOfInmates; ++inmate) {
                dss.Add(inmate);
            }
            foreach (var pair in handcuffed) {
                var inmates = pair.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var inmate0 = int.Parse(inmates[0]);
                var inmate1 = int.Parse(inmates[1]);
                dss.Union(inmate0, inmate1);
            }
            return dss.Sum(group => getTransferCost(group.Count()));
        }

        private int getTransferCost(int group) {
            var result = 1;
            while (result * result < group) {
                ++result;
            }
            return result;
        }

        private sealed class DisjointSetsSystem<T> : IEnumerable<IEnumerable<T>> {
            #region Fields

            private readonly SortedDictionary<T, int> _idByElement = new SortedDictionary<T, int>();
            private readonly T[] _elementById;
            private readonly int[] _leader;
            private readonly List<int>[] _lists;
            private int _count;
            private int _setsCount;

            #endregion

            #region Ctors

            public DisjointSetsSystem(int capacity) {
                _leader = new int[capacity + 1];
                _lists = new List<int>[capacity + 1];
                _elementById = new T[capacity + 1];
                for (var i = 0; i <= capacity; ++i) {
                    _lists[i] = new List<int>();
                }
            }

            #endregion

            #region Properties

            public int Count {
                get {
                    return _setsCount;
                }
            }

            #endregion

            #region Public Methods

            public void Add(T element) {
                if (!_idByElement.ContainsKey(element)) {
                    var id = Register(element);
                    _leader[id] = id;
                    _lists[id].Add(id);
                    ++_setsCount;
                }
            }

            public int GetLeader(T element) {
                int elementId;
                if (_idByElement.TryGetValue(element, out elementId)) {
                    return GetLeader(_idByElement[element]);
                }
                throw new InvalidOperationException();
            }

            public void Union(T element1, T element2) {
                var leader1 = GetLeader(element1);
                var leader2 = GetLeader(element2);
                if (leader1 != leader2) {
                    Union(leader1, leader2);
                    --_setsCount;
                }
            }

            #endregion

            #region Private Methods

            private int Register(T element) {
                ++_count;
                _idByElement.Add(element, _count);
                _elementById[_count] = element;
                return _count;
            }

            private int GetLeader(int element) {
                return _leader[element];
            }

            private void Union(int element1, int element2) {
                if (_lists[element1].Count >= _lists[element2].Count) {
                    foreach (var element in _lists[element2]) {
                        _leader[element] = element1;
                        _lists[element1].Add(element);
                    }
                    _lists[element2].Clear();
                }
                else {
                    Union(element2, element1);
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator<IEnumerable<T>> GetEnumerator() {
                foreach (var list in _lists) {
                    if (list.Count > 0) {
                        yield return list.Select(id => _elementById[id]);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            #endregion
        }
    }
}
