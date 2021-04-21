import java.util.*;

    public class Sortitaire {
        public int turns(int[] tableau, int[] stock) {
            for (int take = 0; take <= stock.length; ++take) {
                if (possible(tableau.clone(), Arrays.copyOf(stock, take))) {
                    return take;
                }
            }
            return -1;
        }

        private static boolean possible(int[] tableau, int[] stock) {
            Arrays.sort(stock);
            for (int i = 0, h = 0, p = 0; i < tableau.length && h < stock.length;) {
                if (tableau[i] < p) {
                    tableau[i] = Integer.MAX_VALUE;
                }
                if (tableau[i] > stock[h]) {
                    tableau[i] = stock[h];
                    i = i + 1;
                    h = h + 1;
                } else {
                    i = i + 1;
                }
                p = tableau[i - 1];
            }
            for (int i = 0; i + 1 < tableau.length; ++i) {
                if (tableau[i] > tableau[i + 1]) {
                    return false;
                }
            }
            return true;
        }
    }
