using System;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class BinaryQuipu {
        public int fewestKnots(string[] inventory) {
            return fewestKnots(new State(inventory), 0);
        }

        /* returns number of required nodes assuming we are currently at node b or s.. */
        private int fewestKnots(State state, int node) {
            if (_memo[node].ContainsKey(state)) {
                /* visited states do not affect nodes count.. */
                return 0;
            }
            var result = 1;
            for (var c = 0; c < 2; ++c) {
                var next = state.Next("bs"[c]);
                if (next != null) {
                    result += fewestKnots(next, c);
                }
            }
            _memo[node][state] = result;
            return result;
        }

        private readonly IDictionary<State, int>[] _memo = {
                new SortedDictionary<State, int>(),
                new SortedDictionary<State, int>() };

        private class State : IComparable<State> {
            private readonly string[] _inventory;

            public State(string[] inventory) {
                _inventory = inventory;
                Array.Sort(_inventory);
            }

            public override int GetHashCode() {
                return ToString().GetHashCode();
            }

            public override string ToString() {
                var stringBuilder = new StringBuilder();
                foreach (var item in _inventory) {
                    stringBuilder.AppendFormat("[{0}]", item);
                }
                return stringBuilder.ToString();
            }

            public State Next(char signal) {
                var result = new List<string>();
                foreach (var item in _inventory) {
                    if (item.Length > 0) {
                        if (item[0] == signal) {
                            result.Add(item.Remove(0, 1));
                        }
                    }
                }
                if (result.Count > 0) {
                    return new State(result.ToArray());
                }
                return null;
            }

            public int CompareTo(State other) {
                return ToString().CompareTo(other.ToString());
            }
        }
    }
}