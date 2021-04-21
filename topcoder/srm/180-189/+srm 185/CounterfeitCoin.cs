using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class CounterfeitCoin {
        public string nextWeighing(int n, string[] left, string[] right, string result) {
            var weighings = new List<Weighing>();
            for (var i = 0; i < result.Length; ++i) {
                weighings.Add(Weighing.Parse(left[i], right[i], result[i]));
            }
            return nextWeighing(n, weighings);
        }

        private static string nextWeighing(int numOfCoins, IEnumerable<Weighing> weighings) {
            /* The main issue is that we do not know whether counterfeit coin is heavier or
             * lighter than the good coin. So we devide all coins into separate classes:
             *   L(0) = "suspected lighter coins"
             *   H(1) = "suspected heavier coins"
             *   U(2) = "unknown coins"
             *   N(3) = "normall coins"
             * The coins from the first two classes could not be moved from one to another
             * but can be moved to the class N (normall).
             */
            var lighter = new List<int>();
            var heavier = new List<int>();
            var unknown = new List<int>();
            var normall = new List<int>();
            for (var coin = 0; coin < numOfCoins; ++coin) {
                append(unknown, coin);
            }
            foreach (var weighing in weighings) {
                remove(unknown, weighing.Lighter);
                remove(unknown, weighing.Heavier);
                if (weighing.Balanced) {
                    append(normall, weighing.Lighter);
                    append(normall, weighing.Heavier);
                    continue;
                }
                /* change weight of some coins i.e. move them from one class into another.. */
                change(heavier, lighter, normall, weighing.Lighter);
                change(lighter, heavier, normall, weighing.Heavier);
                for (var coin = 0; coin < numOfCoins; ++coin) {
                    if (weighing.Lighter.Contains(coin) ||
                        weighing.Heavier.Contains(coin)) {
                        continue;
                    }
                    remove(unknown, coin);
                    append(normall, coin);
                }
            }
            foreach (var coin in normall) {
                remove(lighter, coin);
                remove(heavier, coin);
            }
            if (normall.Count < numOfCoins) {
                lighter.Sort();
                heavier.Sort();
                unknown.Sort();
                normall.Sort();
                return nextWeighing(numOfCoins, lighter, heavier, unknown, normall);
            }
            return "error";
        }

        private static string nextWeighing(int numOfCoins, IList<int> lighter, IList<int> heavier, IList<int> unknown, IList<int> normall) {
            var result = string.Empty;
            if (lighter.Count + heavier.Count != 1) {
                var memo = new int[numOfCoins + 1, numOfCoins + 1, numOfCoins + 1];
                for (var l = 0; l <= numOfCoins; ++l)
                    for (var h = 0; h <= numOfCoins; ++h)
                        for (var u = 0; u <= numOfCoins; ++u) {
                            memo[l, h, u] = -1;
                        }
                memo[0, 0, 0] = 0;
                memo[1, 0, 0] = 0;
                memo[0, 1, 0] = 0;
                IList<WeighingStack>[] stack = {
                    new List<WeighingStack>(),
                    new List<WeighingStack>()
                };
                nextWeighing(memo, numOfCoins, lighter.Count, heavier.Count, unknown.Count, stack);
                for (var i = 0; i < stack[0].Count; ++i) {
                    var stack0 = stack[0][i];
                    var stack1 = stack[1][i];
                    var coins0 = new List<int>();
                    var coins1 = new List<int>();
                    coins0.AddRange(lighter.Take(stack0.Lighter)); coins1.AddRange(lighter.Skip(stack0.Lighter).Take(stack1.Lighter));
                    coins0.AddRange(heavier.Take(stack0.Heavier)); coins1.AddRange(heavier.Skip(stack0.Heavier).Take(stack1.Heavier));
                    coins0.AddRange(unknown.Take(stack0.Unknown)); coins1.AddRange(unknown.Skip(stack0.Unknown).Take(stack1.Unknown));

                    var total = Math.Max(stack0, stack1);
                    coins0.AddRange(normall.Take(total - stack0));
                    coins1.AddRange(normall.Skip(total - stack0).Take(total - stack1));

                    coins0.Sort();
                    coins1.Sort();

                    result = relax(result, string.Format("{0}-{1}",
                        new string(coins0.Select(coin => (char)(coin + 'A')).ToArray()),
                        new string(coins1.Select(coin => (char)(coin + 'A')).ToArray())));
                }
            }
            return result;
        }

        private static string relax(string result, string weighing) {
            if (weighing.Length < result.Length || string.IsNullOrEmpty(result)) {
                return weighing;
            }
            if (weighing.Length == result.Length)
                if (weighing.CompareTo(result) < 0) {
                    return weighing;
                }
            return result;
        }

        private static void nextWeighing(int[, ,] memo, int nn, int ll, int hh, int uu, IList<WeighingStack>[] stack) {
            var best = nextWeighing(memo, nn, ll, hh, uu);
            for (var l0 = 0; l0 <= ll; ++l0)
            for (var h0 = 0; h0 <= hh; ++h0)
            for (var u0 = 0; u0 <= uu; ++u0)
                for (var l1 = 0; l0 + l1 <= ll; ++l1)
                for (var h1 = 0; h0 + h1 <= hh; ++h1)
                for (var u1 = 0; u0 + u1 <= uu; ++u1) {
                    var t0 = Math.Min(l0 + h0 + u0, l1 + h1 + u1);
                    var t1 = Math.Max(l0 + h0 + u0, l1 + h1 + u1);
                    if (t1 - t0 <= nn - ll - hh - uu) {
                        var result = 1 + max(
                            nextWeighing(memo, nn, l0 + u0, h1 + u1, 0),
                            nextWeighing(memo, nn, l1 + u1, h0 + u0, 0),
                            nextWeighing(memo, nn, ll - l0 - l1, hh - h0 - h1, uu - u0 - u1));
                        if (result == best) {
                            stack[0].Add(new WeighingStack(l0, h0, u0));
                            stack[1].Add(new WeighingStack(l1, h1, u1));
                        }
                    }
                }
        }

        private static int nextWeighing(int[,,] memo, int nn, int ll, int hh, int uu) {
            /* this code assumes several optimizations which can be omitted now due to the constraints.. */
            if (memo[ll, hh, uu] == -1) {
                memo[ll, hh, uu] = (int)1e+6;
                for (var l0 = 0; l0 <= ll; ++l0)
                for (var h0 = 0; h0 <= hh; ++h0)
                for (var u0 = 0; u0 <= uu; ++u0)
                    for (var l1 = 0; l0 + l1 <= ll; ++l1)
                    for (var h1 = 0; h0 + h1 <= hh; ++h1)
                    for (var u1 = 0; u0 + u1 <= uu; ++u1) {
                        var t0 = Math.Min(l0 + h0 + u0, l1 + h1 + u1);
                        var t1 = Math.Max(l0 + h0 + u0, l1 + h1 + u1);
                        if (t1 - t0 <= nn - ll - hh - uu) {
                            memo[ll, hh, uu] = min(memo[ll, hh, uu], 1 + max(
                                nextWeighing(memo, nn, l0 + u0, h1 + u1, 0),
                                nextWeighing(memo, nn, l1 + u1, h0 + u0, 0),
                                nextWeighing(memo, nn, ll - l0 - l1, hh - h0 - h1, uu - u0 - u1)));
                        }
                    }
            }
            return memo[ll, hh, uu];
        }

        private static int min(params int[] a) { return a.Min(); }
        private static int max(params int[] a) { return a.Max(); }

        private struct WeighingStack {
            public readonly int Lighter;
            public readonly int Heavier;
            public readonly int Unknown;

            public WeighingStack(int lighter, int heavier, int unknown) {
                Lighter = lighter;
                Heavier = heavier;
                Unknown = unknown;
            }

            public static implicit operator int(WeighingStack weighingStack) {
                return weighingStack.Lighter + weighingStack.Heavier + weighingStack.Unknown;
            }
        }

        private struct Weighing {
            public readonly int[] Lighter;
            public readonly int[] Heavier;
            public readonly bool Balanced;

            private Weighing(int[] lighter, int[] heavier, bool balanced) {
                Lighter = lighter;
                Heavier = heavier;
                Balanced = balanced;
            }

            public static Weighing Parse(string le, string ri, char res) {
                var lestack = Array.ConvertAll(le.ToCharArray(), c => c - 'A');
                var ristack = Array.ConvertAll(ri.ToCharArray(), c => c - 'A');
                switch (res) {
                    case 'L': return new Weighing(ristack, lestack, false);
                    case 'R': return new Weighing(lestack, ristack, false);
                }
                return new Weighing(lestack, ristack, true);
            }
        }

        private static void change(IList<int> curr, IList<int> next, IList<int> norm, IEnumerable<int> coins) {
            foreach (var coin in coins) {
                if (curr.Contains(coin)) {
                    remove(curr, coin);
                    append(norm, coin);
                }
                else {
                    append(next, coin);
                }
            }
        }

        private static void append(IList<int> src, IEnumerable<int> a) {
            foreach (var x in a) {
                append(src, x);
            }
        }

        private static void remove(IList<int> src, IEnumerable<int> a) {
            foreach (var x in a) {
                remove(src, x);
            }
        }

        private static void append(IList<int> src, int x) {
            if (!src.Contains(x)) {
                src.Add(x);
            }
        }

        private static void remove(IList<int> src, int x) {
            src.Remove(x);
        }
    }
}