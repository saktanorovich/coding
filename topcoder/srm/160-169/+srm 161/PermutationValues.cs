using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class PermutationValues {
        public int[] getValues(int[] lows, int[] highs, string lexPos, string[] retInts) {
            return getValues(convert(lows), convert(highs), long.Parse(lexPos),
                Array.ConvertAll(retInts, delegate(string s) {
                    return long.Parse(s);
            }));
        }

        private int[] getValues(long[] lows, long[] highs, long lexPos, long[] retInts) {
            Array.Sort(lows, highs);
            long total = 0;
            for (int i = 0; i < lows.Length; ++i) {
                total += highs[i] - lows[i] + 1;
            }
            /* because lexPos ≤ 2^63-1 it is enough to analyze only the last 21 elements.. */
            if (total < 21) {
                if (lexPos > f(total)) {
                    lexPos %= f(total);
                }
            }
            int[] result = new int[retInts.Length];
            for (int k = 0; k < retInts.Length; ++k) {
                long element = getElement(total, lexPos + 1, retInts[k]);
                for (int i = 0; i < lows.Length; ++i) {
                    long count = highs[i] - lows[i] + 1;
                    if (element >= count) {
                        element -= count;
                    } else {
                        result[k] = (int)(lows[i] + element);
                        break;
                    }
                }
            }
            return result;
        }

        private long getElement(long total, long lexPos, long position) {
            List<long> elements = new List<long>();
            for (long num = total - 1; elements.Count != 21; ) {
                elements.Insert(0, num);
                num = num - 1;
                if (num < 0) {
                    break;
                }
            }
            if (position < elements[0]) {
                return position;
            }
            position -= elements[0];
            List<long> permutation = new List<long>();
            while (lexPos > 1) {
                for (int k = 0; k < elements.Count; ++k) {
                    long count = f(elements.Count - 1);
                    if (lexPos > count) {
                        lexPos -= count;
                    } else {
                        permutation.Add(elements[k]);
                        elements.Remove(elements[k]);
                        break;
                    }
                }
            }
            permutation.AddRange(elements);
            return permutation[(int)position];
        }

        private long[] convert(int[] a) {
            return Array.ConvertAll(a, delegate(int x) {
                return (long)x;
            });
        }

        private long f(long n) {
            long result = 1;
            for (long i = 2; i <= n; ++i) {
                result *= i;
            }
            return result;
        }
    }
}