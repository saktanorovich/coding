package gcj2008.semiAMER;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class King {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int m = in.nextInt();
        char a[][] = new char[n][];
        for (int i = 0; i < n; ++i) {
            a[i] = in.next().toCharArray();
        }
        Game game;
        if (n * m <= 16) {
            game = new GameSmall(a, n, m);
        } else {
            game = new GameLarge(a, n, m);
        }
        out.format("Case #%d: %s\n", testCase, "AB".charAt(game.play()));
    }

    private abstract class Game {
        protected char[][] a;
        protected int n;
        protected int m;

        public Game(char[][] a, int n, int m) {
            this.a = a;
            this.n = n;
            this.m = m;
        }

        public int play() {
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    if (a[i][j] == 'K') {
                        return play(i, j);
                    }
                }
            }
            throw new RuntimeException();
        }

        protected abstract int play(int x, int y);
    }

    private class GameSmall extends Game {
        private int[][] memo;

        public GameSmall(char[][] a, int n, int m) {
            super(a, n, m);
            this.memo = new int[n * m][1 << (n * m)];
            for (int x = 0; x < n * m; ++x) {
                Arrays.fill(memo[x], -1);
            }
        }

        @Override
        protected int play(int x, int y) {
            int state = 0;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    if (a[i][j] != '.') {
                        state |= 1 << (i * m + j);
                    }
                }
            }
            return 1 - F(state, x, y);
        }

        private int F(int state, int x, int y) {
            if (memo[E(x, y)][state] == -1) {
                int optimum = 0;
                for (int dx = -1; dx <= +1; ++dx) {
                    for (int dy = -1; dy <= +1; ++dy) {
                        if (dx != 0 || dy != 0) {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (0 <= nx && nx < n && 0 <= ny && ny < m) {
                                if (a[nx][ny] == '.') {
                                    a[nx][ny] = '#';
                                    state |= 1 << E(nx, ny);
                                    optimum = Math.max(optimum, 1 - F(state, nx, ny));
                                    state ^= 1 << E(nx, ny);
                                    a[nx][ny] = '.';
                                }
                            }
                        }
                    }
                }
                memo[E(x, y)][state] = optimum;
            }
            return memo[E(x, y)][state];
        }

        private int E(int x, int y) {
            return x * m + y;
        }
    }

    private class GameLarge extends Game {
        public GameLarge(char[][] a, int n, int m) {
            super(a, n, m);
        }

        @Override
        protected int play(int x, int y) {
            return 0;
        }
    }
}
