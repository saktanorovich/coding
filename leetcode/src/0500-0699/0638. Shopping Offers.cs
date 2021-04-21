using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0638 {
        public int ShoppingOffers(IList<int> price, IList<IList<int>> special, IList<int> needs) {
            return new Solver(price, special).solve(needs);
        }

        private class Solver {
            private readonly Dictionary<IList<int>, int> best;
            private readonly IList<IList<int>> special;
            private readonly IList<int> price;

            public Solver(IList<int> price, IList<IList<int>> special) {
                this.price = price;
                this.special = special;
                best = new Dictionary<IList<int>, int>(new EqualityComparer()) {
                    { new int[price.Count], 0 }
                };
            }

            public int solve(IList<int> needs) {
                if (best.TryGetValue(needs, out var result)) {
                    return result;
                } else {
                    result = 0;
                    for (var i = 0; i < needs.Count; ++i) {
                        result += needs[i] * price[i];
                    }
                    best[needs] = result;
                    foreach (var offer in special) {
                        var next = new int[needs.Count];
                        for (var i = 0; i < needs.Count; ++i) {
                            next[i] = needs[i] - offer[i];
                            if (next[i] < 0) {
                                goto skip;
                            }
                        }
                        result = Math.Min(result, solve(next) + offer[needs.Count]);
                        skip: ;
                    }
                }
                return result;
            }

            private class EqualityComparer : IEqualityComparer<IList<int>> {
                public bool Equals(IList<int> x, IList<int> y) {
                    for (var i = 0; i < x.Count; ++i) {
                        if (x[i] != y[i]) {
                            return false;
                        }
                    }
                    return true;
                }

                public int GetHashCode(IList<int> a) {
                    var hash = 17;
                    foreach (var x in a) {
                        hash = hash * 31 + x;
                    }
                    return hash;
                }
            }
        }
    }
}
