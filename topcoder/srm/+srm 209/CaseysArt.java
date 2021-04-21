import java.util.*;

    public class CaseysArt {
        public double howManyWays(int length, int width) {
            if (length < width) {
                return howManyWaysImpl(width, length);
            } else {
                return howManyWaysImpl(length, width);
            }
        }

        private double howManyWaysImpl(int n, int m) {
            double[][][] f = new double[2][m][1 << (m + 1)]; f[0][0][0] = 1;
            for (int r = 0; r < n; ++r) {
                int a = r & 1;
                int b = 1 - a;
                f[b] = new double[m][1 << (m + 1)];
                for (int c = 0; c < m; ++c) {
                    for (int state = 0; state < 1 << (m + 1); ++state) {
                        for (int next : enumerate(n, m, r, c, state, (1 << c + 1) - 1)) {
                            if (c + 1 < m) {
                                f[a][c + 1][next] += f[a][c][state];
                                continue;
                            }
                            f[b][0][next << 1] += f[a][c][state];
                        }
                    }
                }
            }
            return f[n & 1][0][0];
        }

        private static List<Integer> enumerate(int n, int m, int row, int col, int state, int mask) {
            int curr = (state & ~mask) >> 1;
            int next = (state & +mask);
            List<Integer> result = new ArrayList<>();
            int bit = get(curr, col);
            if (bit == 1) {
                result.add(pack(next, curr, col));
            }
            if (row + 1 < n && col + 1 < m) {
                for (int i = 0; i < 4; ++i) {
                    boolean match = true;
                    match &= get(curr, col + 0) + shapes[i][0] == 1;
                    match &= get(curr, col + 1) + shapes[i][1] <= 1;
                    match &= get(next, col + 0) + shapes[i][2] <= 1;
                    match &= get(next, col + 1) + shapes[i][3] <= 1;
                    if (match) {
                        curr = set(curr, col + 0, shapes[i][0]);
                        curr = set(curr, col + 1, shapes[i][1]);
                        next = set(next, col + 0, shapes[i][2]);
                        next = set(next, col + 1, shapes[i][3]);
                        result.add(pack(next, curr, col));
                        curr = set(curr, col + 0, shapes[i][0]);
                        curr = set(curr, col + 1, shapes[i][1]);
                        next = set(next, col + 0, shapes[i][2]);
                        next = set(next, col + 1, shapes[i][3]);
                    }
                }
            }
            return result;
        }

        private static int pack(int next, int curr, int col) {
            curr >>= col + 1;
            curr <<= col + 2;
            return next | curr;
        }

        private static int get(int mask, int pos) {
            return (mask >> pos) & 1;
        }

        private static int set(int mask, int pos, int b) {
            return mask ^ (b << pos);
        }

        private static int[][] shapes = {
            { 0, 1, 1, 1 },
            { 1, 0, 1, 1 },
            { 1, 1, 0, 1 },
            { 1, 1, 1, 0 },
        };
    }
