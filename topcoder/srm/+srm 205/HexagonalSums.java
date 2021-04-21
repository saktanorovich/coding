import java.util.*;

    public class HexagonalSums {
        public int minLength(int n) {
            List<Integer> hex = new ArrayList<>();
            for (int k = 0, h = 1; h <= n; ++k, h += 4 * k + 1) {
                hex.add(h);
            }
            int[] mini = new int[n + 1];
            Arrays.fill(mini, n);
            for (int h : hex) {
                mini[h] = 1;
            }
            int rounds = eval(n);
            for (int rd = 1; rd <= rounds; ++rd) {
                for (int h : hex) {
                    for (int x = n - h; x > 0; --x) {
                        mini[x + h] = Math.min(mini[x + h], mini[x] + 1);
                    }
                }
            }
            return mini[n];
        }

        private static int eval(int n) {
            if (n <= 26)     return 6;
            if (n <= 130)    return 5;
            if (n <= 146858) return 4;

            return 3;
        }
    }
