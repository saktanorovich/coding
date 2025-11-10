using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_22 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var m = reader.NextInt();
            var w = new string[n];
            for (var i = 0; i < n; ++i) {
                w[i] = reader.Next();
            }
            writer.WriteLine(get(w, m));
            return true;
        }

        private int get(string[] w, int n) {
            var T = new AhoCorasick(w.Select(s => s.Length).Sum() + 1);
            foreach (var s in w) {
                T.add(s);
            }
            var g = new List<int>[T.Count];
            for (var s = 0; s < T.Count; ++s) {
                g[s] = new List<int>();
                for (var c = 0; c < 26; ++c) {
                    var t = T.next(s, c);
                    if (T.has(t) == false) {
                        g[s].Add(t);
                    }
                }
            }
            var curr = new int[T.Count];
            var next = new int[T.Count];
            curr[0] = 1;
            for (var i = 1; i <= n; ++i) {
                for (var s = 0; s < T.Count; ++s) {
                    if (curr[s] > 0) {
                        foreach (var t in g[s]) {
                            next[t] += curr[s];
                            if (next[t] >= MOD) {
                                next[t] -= MOD;
                            }
                        }
                    }
                }
                for (var s = 0; s < T.Count; ++s) {
                    curr[s] = next[s];
                    next[s] = 0;
                }
            }
            var res = 0;
            for (var i = 0; i < T.Count; ++i) {
                if (curr[i] > 0) {
                    res += curr[i];
                    if (res >= MOD) {
                        res -= MOD;
                    }
                }
            }
            return res;
        }

        private static readonly int MOD = (int)1e9 + 7;

        private class AhoCorasick {
            private readonly Node[] nodes;
            private int n;

            public int Count => n;

            public AhoCorasick(int capacity) {
                nodes = new Node[capacity];
                nodes[0] = new Node(-1, '\t');
                nodes[0].suffLink = 0;
                nodes[0].wordLink = 0;
                n = 1;
            }

            public void add(string w) {
                var s = 0;
                foreach (var x in w) {
                    var c = x - 'a';
                    if (nodes[s].descendant[c] == -1) {
                        nodes[n] = new Node(s, c);
                        nodes[s].descendant[c] = n;
                        n++;
                    }
                    s = nodes[s].descendant[c];
                }
                nodes[s].wordLink = s;
                nodes[s].terminal = true;
            }

            public bool has(int s) {
                return nodes[wordLink(s)].terminal;
            }

            public int next(int s, int c) {
                var node = nodes[s];
                if (node.transition[c] == -1) {
                    if (node.descendant[c] != -1) {
                        node.transition[c] = node.descendant[c];
                    } else {
                        node.transition[c] = 0;
                        if (s > 0) {
                            node.transition[c] = next(suffLink(s), c);
                        }
                    }
                }
                return node.transition[c];
            }

            private int suffLink(int s) {
                var node = nodes[s];
                if (node.suffLink == -1) {
                    node.suffLink = 0;
                    if (node.parent > 0) {
                        node.suffLink = next(suffLink(node.parent), node.symbol);
                    }
                }
                return node.suffLink;
            }

            private int wordLink(int s) {
                var node = nodes[s];
                if (node.wordLink == -1) {
                    node.wordLink = wordLink(suffLink(s));
                }
                return node.wordLink;
            }

            private struct Node {
                public readonly int[] descendant;
                public readonly int[] transition;
                public readonly int parent;
                public readonly int symbol;
                public int suffLink;
                public int wordLink;
                public bool terminal;

                public Node(int parent, int symbol) {
                    this.descendant = new int[26];
                    this.transition = new int[26];
                    for (var i = 0; i < 26; ++i) {
                        descendant[i] = -1;
                        transition[i] = -1;
                    }
                    this.parent = parent;
                    this.symbol = symbol;
                    this.suffLink = -1;
                    this.wordLink = -1;
                    this.terminal = false;
                }
            }
        }
    }
}
