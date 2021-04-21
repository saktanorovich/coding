import java.util.*;

    public class Histogram {
        public String[] draw(String[] title, int[] value) {
            int numOfRows = 1;
            int[] pos = new int[title.length];
            int[] mid = new int[title.length];
            for (int i = 0; i < title.length; ++i) {
                mid[i] = (title[i].length() - 1) / 2;
                numOfRows = Math.max(numOfRows, value[i] + 1);
            }
            int gap = 0;
            for (int i = 1; i < title.length; ++i) {
                pos[i] = pos[i - 1] + title[i - 1].length() + 1;
                gap = Math.max(gap, pos[i] + mid[i] - pos[i - 1] - mid[i - 1]);
            }
            for (int i = 1; i < title.length; ++i) {
                pos[i] = gap + pos[i - 1] + mid[i - 1] - mid[i];
            }
            char[][] histogram = new char[numOfRows][MAX];
            for (int i = 0; i < numOfRows; ++i) {
                for (int j = 0; j < MAX; ++j) {
                    histogram[i][j] = ' ';
                }
            }
            for (int i = 0; i < title.length; ++i) {
                for (int j = 0; j < title[i].length(); ++j) {
                    histogram[0][pos[i] + j] = title[i].charAt(j);
                }
                for (int j = 1; j <= value[i]; ++j) {
                    histogram[j][pos[i] + mid[i]] = 'X';
                }
            }
            String[] res = new String[numOfRows];
            for (int i = 0; i < numOfRows; ++i) {
                res[numOfRows - 1 - i] = ("#" + new String(histogram[i])).trim().substring(1);
            }
            return res;
        }

        private static final int MAX = 256;
    }