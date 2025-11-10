using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_17 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.a = new HashSet<int>();
            for (var i = 0; i < n; ++i) {
                a.Add(reader.NextInt());
            }
            var res = false;
            res = res || has(1021 , 1031 , 1033);
            res = res || has(1021 * 1031 , 1033);
            res = res || has(1021 , 1031 * 1033);
            res = res || has(1021 * 1033 , 1031);
            res = res || has(1021 * 1031 * 1033);
            if (res) {
                writer.WriteLine("YES");
            } else {
                writer.WriteLine("NO");
            }
            return true;
        }

        private bool has(params int[] f) {
            foreach (var x in f) {
                if (a.Contains(x) == false) {
                    return false;
                }
            }
            return true;
        }

        private HashSet<int> a;
        private int n;
    }
}
