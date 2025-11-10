using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BearAttacks {
        public int expectedValue(int n, int r0, int a, int b, int m, int low, int high) {
            return expectedValue(generate(n, r0, a, b, m, low, high, 1000000000), n);
        }

        /* note: the solution can be found from the following relation
         *  (1 - u) * a + u * (b + 1) ^ 2 = a + u * (1 + 2 * b + sum(xi * xj, i != j)
         * where a = sum(xi^2), b = sum(xi)
         */
        private static int expectedValue(List<int>[] tree, int n) {
            var inv = new long[n + 1]; // in order to find inv[i] look at inv[modulo % i]
            inv[1] = 1;
            for (var i = 2; i <= n; ++i) {
                inv[i] = modulo - modulo / i * inv[modulo % i] % modulo;
            }
            var result = dfs(tree, 0, inv, new bool[n], new long[n], new long[n]);
            for (var i = 2; i <= n; ++i) {
                result *= i;
                result %= modulo;
            }
            return (int)result;
        }

        private static long dfs(List<int>[] tree, int root, long[] inv, bool[] visited, long[] expectedSum, long[] expectedSqr) {
            var stack = new Stack<int>();
            for (stack.Push(root); stack.Count > 0;) {
                root = stack.Pop();
                if (!visited[root]) {
                    stack.Push(root);
                    visited[root] = true;
                    foreach (var next in tree[root]) {
                        stack.Push(next);
                    }
                    continue;
                }

                foreach (var next in tree[root]) {
                    expectedSum[root] += expectedSum[next];
                    expectedSum[root] %= modulo;

                    expectedSqr[root] += expectedSqr[next];
                    expectedSqr[root] %= modulo;
                }

                var tmp = 1L;
                foreach (var next in tree[root]) {
                    var sum = 0L;
                    sum += expectedSum[root] - expectedSum[next];
                    sum += modulo;
                    sum *= expectedSum[next];
                    sum %= modulo;
                    tmp += sum;
                    tmp %= modulo;
                }
                tmp += 2 * expectedSum[root] % modulo;
                tmp %= modulo;

                expectedSum[root] += 1;
                expectedSum[root] *= inv[root + 1];
                expectedSum[root] %= modulo;

                expectedSqr[root] += inv[root + 1] * tmp;
                expectedSqr[root] %= modulo;
            }
            return expectedSqr[root];
        }

        private static List<int>[] generate(int n, long r, long a, long b, long m, long low, long high, long billion) {
            var tree = new List<int>[n];
            for (var i = 0; i < n; ++i) {
                tree[i] = new List<int>();
            }
            for (var i = 1; i < n; ++i) {
                r = (a * r + b) % m;
                var min = (i - 1) * low / billion;
                var max = (i - 1) * high / billion;
                tree[(int)(min + (r % (max - min + 1)))].Add(i);
            }
            return tree;
        }

        private const long modulo = (long)1e9 + 7;
    }
}
