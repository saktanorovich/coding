import java.util.*;

    public class SumOfSquareRoots {
        public int[] shortestList(int[] a) {
            int[] s = new int[MAX + 1];
            for (int x : a) {
                int p = 1;
                for (int d = 2; d * d <= x; ++d) {
                    while (x % (d * d) == 0) {
                        p *= d;
                        x /= d * d;
                    }
                }
                s[x] += p;
            }            
            List<Integer> b = new ArrayList<>();
            for (int x = 1; x <= MAX; ++x) {
                if (s[x] > 0) {
                    b.add(s[x] * s[x] * x);
                }
            }
            Collections.sort(b);
            int[] res = new int[b.size()];
            for (int i = 0; i < b.size(); ++i) {
                res[i] = b.get(i);
            }
            return res;
        }

        private static final int MAX = 1000;
    }