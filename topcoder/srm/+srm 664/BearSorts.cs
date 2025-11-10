using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BearSorts {
        public int index(int[] seq) {
            return index(seq, seq.Length);
        }

        private int index(IList<int> seq, int n) {
            c = new long[n + 1, n + 1];
            for (var i = 0; i <= n; ++i) {
                c[i, 0] = c[i, i] = 1;
                for (var j = 1; j < i; ++j) {
                    c[i, j] = (c[i - 1, j] + c[i - 1, j - 1]) % modulo;
                }
            }

            var result = 1L;
            var me = mergeSort(seq, 1, n + 1);

            // calculate number of the most probable permutations per comparisons
            var prefix = new int[n];
            var prefixSummary = summary(prefix, 1, n + 1);
            for (var prob = 0; prob < me; ++prob) {
                result += prefixSummary[prob];
                result %= modulo;
            }

            // iterate over each prefix defined by seq
            for (var i = 0; i < n; ++i) {
                var taken = prefix.Take(i).ToList();
                for (prefix[i] = 1; prefix[i] < seq[i]; ++prefix[i]) {
                    if (taken.Contains(prefix[i])) {
                        continue;
                    }
                    prefixSummary = summary(prefix, 1, n + 1);
                    if (me < prefixSummary.Length) {
                        result += prefixSummary[me];
                        result %= modulo;
                    }
                }
            }
            return (int)result;
        }

        private long[] summary(IList<int> prefix, int lo, int hi) {
            if (lo + 1 >= hi) {
                return new[] { 1L };
            }
            var loPart = new List<int>();
            var hiPart = new List<int>();
            var mid = (lo + hi) / 2;
            var ilo = 0;
            var ihi = 0;
            for (var pos = 1; pos <= prefix.Count; ++pos) {
                var element = prefix[pos - 1];
                if (lo <= element && element < mid) {
                    loPart.Add(element);
                    ilo = pos;
                }
                if (mid <= element && element < hi) {
                    hiPart.Add(element);
                    ihi = pos;
                }
            }
            // we need to combine parts into a single one
            var loTail = mid - lo - loPart.Count;
            var hiTail = hi - mid - hiPart.Count;
            var result = choose(hi - lo, loTail, hiTail, ilo, ihi);
            result = combine(result, summary(loPart, lo, mid));
            result = combine(result, summary(hiPart, mid, hi));
            return result;
        }

        private long[] choose(int nsize, int loTail, int hiTail, int ilo, int ihi) {
            var summary = new long[nsize];
            if (loTail == 0 || hiTail == 0) {
                // for full parts we have only one possible way for
                // mergeSort (do not take factorial because we have
                // predifined order during sort)
                if (loTail == 0 && hiTail == 0)
                    summary[Math.Min(ilo, ihi)] = 1;
                else if (loTail == 0)
                    summary[ilo] = 1;
                else if (hiTail == 0)
                    summary[ihi] = 1;
            }
            else {
                // we nee to iterate over each possible configuration
                var ntail = loTail + hiTail;

                // reserve last p positions for the elements from hiPart
                for (var p = 1; p <= hiTail; ++p) {
                    summary[nsize - p] += c[ntail - p - 1, loTail - 1];
                    summary[nsize - p] %= modulo;
                }

                // reserve last p positions for the elements from loPart
                for (var p = 1; p <= loTail; ++p) {
                    summary[nsize - p] += c[ntail - p - 1, hiTail - 1];
                    summary[nsize - p] %= modulo;
                }
            }
            return summary;
        }

        private static long[] combine(long[] a, long[] b) {
            var result = new long[a.Length + b.Length - 1];
            for (var i = 0; i < a.Length; ++i) {
                for (var j = 0; j < b.Length; ++j) {
                    result[i + j] = (result[i + j] + a[i] * b[j]) % modulo;
                }
            }
            return result;
        }

        // Calculate number of comparisons to get a[] from initial 1,..,n. The
        // number of comparisons denotes such position pos that a[1,..,pos] is
        // filled by mergeSort comparison between lo and hi parts.
        private static int mergeSort(IList<int> a, int lo, int hi) {
            if (a.Count == 1) {
                return 0;
            }
            // find largest occupied position for both parts
            var loPart = new List<int>();
            var hiPart = new List<int>();
            var mid = (lo + hi) / 2;
            var ilo = 0;
            var ihi = 0;
            for (var pos = 1; pos <= a.Count; ++pos) {
                var element = a[pos - 1];
                if (element < mid) {
                    loPart.Add(element);
                    ilo = pos;
                }
                else {
                    hiPart.Add(element);
                    ihi = pos;
                }
            }
            // take minimum position because minimum denotes that
            // we have taken all elements from a part
            var result = Math.Min(ilo, ihi);
            result += mergeSort(loPart, lo, mid);
            result += mergeSort(hiPart, mid, hi);
            return result;
        }

        private const long modulo = (long)1e9 + 7;
        private long[,] c;
    }
}
