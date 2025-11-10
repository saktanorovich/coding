using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_19 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var n = reader.NextInt();
            var a = new int[n];
            for (var i = 0; i < n; ++i) {
                a[i] = reader.NextInt();
            }
            writer.WriteLine(doit(a));
            return true;
        }

        private long doit(int[] A) {
            if (A.Length > 1) {
                var mid = A.Length / 2;
                var L = A.Take(mid).ToArray();
                var R = A.Skip(mid).ToArray();
                var S = 0L;
                S += doit(L);
                S += doit(R);
                var a = 0;
                var l = 0;
                var r = 0;
                while (l < L.Length && r < R.Length) {
                    if (L[l] < R[r]) {
                        A[a] = L[l++];
                    } else {
                        A[a] = R[r++];
                        S += L.Length - l;
                    }
                    ++a;
                }
                while (l < L.Length) A[a++] = L[l++];
                while (r < R.Length) A[a++] = R[r++];
                return S;
            }
            return 0;
        }
    }
}
