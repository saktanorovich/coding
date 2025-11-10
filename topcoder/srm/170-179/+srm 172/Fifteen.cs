using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Fifteen {
        public string outcome(int[] moves) {
            foreach (var next in possible(moves)) {
                if (playerwin(join(moves, next))) {
                    return string.Format("WIN {0}", next);
                }
            }
            return "LOSE";
        }

        private bool playerwin(IList<int> moves) {
            if (fifteen(moves, 1)) {
                return true;
            }
            if (moves.Count + 1 < 9) {
                foreach (var next in possible(moves)) {
                    if (dialerwin(join(moves, next))) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool dialerwin(IList<int> moves) {
            if (fifteen(moves, 0)) {
                return true;
            }
            if (moves.Count + 1 < 9) {
                foreach (var next in possible(moves)) {
                    if (playerwin(join(moves, next))) {
                        return false;
                    }
                }
            }
            return true;
        }

        private static IEnumerable<int> possible(IList<int> moves) {
            var result = new List<int>();
            for (var next = 1; next <= 9; ++next) {
                if (!moves.Contains(next)) {
                    result.Add(next);
                }
            }
            return result;
        }

        private static IList<int> join(IEnumerable<int> list, int item) {
            var result = new List<int>(list);
            result.Add(item);
            return result;
        }

        private static bool fifteen(IList<int> moves, int ix) {
            var coin = new List<int>();
            for (var i = ix; i < moves.Count; i += 2) {
                coin.Add(moves[i]);
            }
            for (var i = 0; i < coin.Count; ++i)
                for (var j = i + 1; j < coin.Count; ++j)
                    for (var k = j + 1; k < coin.Count; ++k)
                        if (coin[i] + coin[j] + coin[k] == 15) {
                            return true;
                        }
            return false;
        }
    }
}