package gcj2008.rd3;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem B
public class Portal {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt() + 1;
        this.m = in.nextInt() + 1;
        this.grid = new char[n][m];
        this.shot = new short[n][m][4][2];
        this.best = new short[n][m][n][m][n][m];
        State state = null;
        for (int i = 1; i < n; ++i) {
            String s = in.nextLine();
            for (int j = 1; j < m; ++j) {
                grid[i][j] = s.charAt(j - 1);
                if (grid[i][j] == 'O') {
                    state = new State(i, j);
                }
            }
        }
        for (int i = 1; i < n; ++i) {
            for (int j = 1; j < m; ++j) {
                for (int k = 0; k < 4; ++k) {
                    int x = i;
                    int y = j;
                    while (true) {
                        int nx = x + dx[k];
                        int ny = y + dy[k];
                        if (0 < nx && nx < n && 0 < ny && ny < m) {
                            if (grid[nx][ny] != '#') {
                                x = nx;
                                y = ny;
                                continue;
                            }
                        }
                        break;
                    }
                    shot[i][j][k][0] = (short)x;
                    shot[i][j][k][1] = (short)y;
                }
            }
        }
        int res = bfs(state);
        if (res != -1) {
            out.format("Case #%d: %d\n", testCase, res);
        } else {
            out.format("Case #%d: %s\n", testCase, "THE CAKE IS A LIE");
        }
    }

    private int bfs(State state) {
        for (int x = 1; x < n; ++x) {
            for (int y = 1; y < m; ++y) {
                for (int x0 = 0; x0 < n; ++x0) {
                    for (int y0 = 0; y0 < m; ++y0) {
                        for (int x1 = 0; x1 < n; ++x1) {
                            for (int y1 = 0; y1 < m; ++y1) {
                                best[x][y][x0][y0][x1][y1] = 255;
                            }
                        }
                    }
                }
            }
        }
        Deque<State> q = new LinkedList<>();
        state.set(best, (short)0);
        for (q.add(state); q.size() > 0;) {
            state = q.poll();
            if (grid[state.x][state.y] == 'X') {
                return state.get(best);
            }
            relax(q, state, state.port(0), 1);
            relax(q, state, state.port(1), 1);
            for (int k = 0; k < 4; ++k) {
                short[] wall = shot[state.x][state.y][k];
                relax(q, state, state.fire(0, wall[0], wall[1]), 0);
                relax(q, state, state.fire(1, wall[0], wall[1]), 0);
                relax(q, state, state.move(n, m, k), 1);
            }
        }
        return -1;
    }

    private void relax(Deque<State> q, State state, State next, int stp) {
        short val = (short)(state.get(best) + stp);
        if (next != null && grid[next.x][next.y] != '#') {
            if (next.get(best) > val) {
                next.set(best, val);
                if (stp > 0) {
                    q.addLast(next);
                } else {
                    q.addFirst(next);
                }
            }
        }
    }

    private short best[][][][][][];
    private short shot[][][][];
    private char grid[][];
    private int n;
    private int m;

    private static final int[] dx = { -1,  0, +1,  0 };
    private static final int[] dy = {  0, -1,  0, +1 };

    private static class State {
        public final int x;
        public final int y;
        public final int[] px;
        public final int[] py;

        public State(int x, int y) {
            this.x = x;
            this.y = y;
            this.px = new int[2];
            this.py = new int[2];
        }

        public State(int x, int y, int[] px, int[] py) {
            this.x = x;
            this.y = y;
            this.px = new int[] { px[0], px[1] };
            this.py = new int[] { py[0], py[1] };
        }

        public State move(int n, int m, int k) {
            int nx = x + dx[k];
            int ny = y + dy[k];
            if (0 < nx && nx < n && 0 < ny && ny < m) {
                return new State(nx, ny, px, py);
            }
            return null;
        }

        public State port(int p) {
            int q = 1 - p;
            if (px[p] == x && py[p] == y) {
                if (px[q] > 0 && py[q] > 0) {
                    return new State(px[q], py[q], px, py);
                }
            }
            return null;
        }

        public State fire(int p, int px, int py) {
            State state = new State(x, y, this.px, this.py);
            state.px[p] = px;
            state.py[p] = py;
            return state;
        }

        public short get(short space[][][][][][]) {
            return space[x][y][px[0]][py[0]][px[1]][py[1]];
        }

        public void set(short space[][][][][][], short value) {
            space[x][y][px[0]][py[0]][px[1]][py[1]] = value;
        }
    }
}
