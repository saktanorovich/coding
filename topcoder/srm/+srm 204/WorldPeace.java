import java.util.*;

    public class WorldPeace {
        public long numGroups(int k, int[] countries) {
            long lo = 0, hi = k - 1;
            for (int population : countries){
                hi += population;
            }
            hi /= k;
            while (lo < hi) {
                long groups = (lo + hi + 1) / 2;
                long people = 0;
                for (int population : countries) {
                    people += Math.min(population, groups);
                }
                if (people < k * groups) {
                    hi = groups - 1;
                } else {
                    lo = groups;
                }
            }
            return lo;
        }
    }
