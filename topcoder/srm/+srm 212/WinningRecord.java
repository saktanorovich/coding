import java.util.*;

    public class WinningRecord {
        public int[] getBestAndWorst(String games) {
            int[] score = new int[games.length()];
            for (int i = 0; i < games.length(); ++i) {
                if (games.charAt(i) == 'W') {
                    score[i] = 1;
                }
            }
            return getBestAndWorst(score);
        }

        private static int[] getBestAndWorst(int[] score) {
            int[] res = new int[2];
            int[] opt = new int[2];
            res[0] = 3;
            res[1] = 3;
            opt[0] = score[0] + score[1] + score[2];
            opt[1] = score[0] + score[1] + score[2];
            for (int take = 4, have = opt[0]; take <= score.length; ++take) {
                have += score[take - 1];
                if (res[0] * have >= opt[0] * take) {
                    res[0] = take;
                    opt[0] = have;
                }
                if (res[1] * have <= opt[1] * take) {
                    res[1] = take;
                    opt[1] = have;
                }
            }
            return res;
        }
    }