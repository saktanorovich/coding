using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class LuckyTree {
        public int[] construct(int favorite) {
            /* the maximum number of paths is equal to 625 on 51 vertices because optimal
             * tree is simple chain with 4/7 alternation..
                    var f = new int[60];
                    for (var i = 2; i < f.Length; ++i) {
                        f[i] = f[i - 1] + (i - 1) / 2;
                        Console.WriteLine("n = {0}, p = {1}", i, f[i]);
                    }
             */
            if (favorite > 625) {
                return new int[0];
            }
            for (var n = 3; n < 52; ++n) {
                for (var n4 = 1; n4 < n; ++n4) {
                    if (n4 * (n - 1 - n4) == favorite) {
                        var tree = construct(n, n4, n - 1 - n4);
                        if (numOfBalancedPaths(tree, n) == favorite) {
                            return convert(tree, n);
                        }
                    }
                }
            }
            return chain(favorite);
        }

        private static int[] chain(int favorite) {
            var prnt = new int[52];
            var edge = new int[52];
            for (int n = 2, have = 0; n <= 51; ++n) {
                var extra = (n - 1) / 2;
                if (have + extra < favorite) {
                    prnt[n - 1] = n - 2;
                    edge[n - 1] = n % 2 == 0 ? 4 : 7;
                    have += extra;
                }
                else {
                    /* we need to add favotite-have additional paths.. */
                    var p = 2 * (favorite - have) + 1;
                    prnt[n - 1] = p - 1;
                    edge[n - 1] = 4;
                    var tree = new int[n, n];
                    for (var i = 0; i < n; ++i) {
                        tree[prnt[i], i] = edge[i];
                        tree[i, prnt[i]] = edge[i];
                    }
                    if (numOfBalancedPaths(tree, n) == favorite) {
                        return convert(tree, n);
                    }
                    throw new InvalidOperationException();
                }
            }
            return new int[0];
        }

        private static int[,] construct(int n, int n4, int n7) {
            var tree = new int[n, n];
            for (var x = 1; x <= n4 + n7; ++x) {
                if (x <= n4)
                    tree[0, x] = tree[x, 0] = 4;
                else
                    tree[0, x] = tree[x, 0] = 7;
            }
            return tree;
        }

        private static int[] convert(int[,] tree, int n) {
            var result = new List<int> { n };
            for (var i = 0; i < n; ++i) {
                for (var j = i + 1; j < n; ++j) {
                    if (tree[i, j] > 0) {
                        result.AddRange(new[] { i, j, tree[i, j] });
                    }
                }
            }
            return result.ToArray();
        }

        private static int numOfBalancedPaths(int[,] tree, int n) {
            var dist = new int[n, n];
            for (var i = 0; i < n; ++i) {
                for (var j = 0; j < n; ++j) {
                    dist[i, j] = int.MaxValue / 2;
                    if (tree[i, j] > 0) {
                        dist[i, j] = 99 * (tree[i, j] / 5) + 1;
                    }
                }
            }
            for (var k = 0; k < n; ++k) {
                for (var i = 0; i < n; ++i) {
                    for (var j = 0; j < n; ++j) {
                        dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                    }
                }
            }
            var result = 0;
            for (var i = 0; i < n; ++i) {
                for (var j = i + 1; j < n; ++j) {
                    if (dist[i, j] / 100 == dist[i, j] % 100) {
                        ++result;
                    }
                }
            }
            return result;
        }
    }
}