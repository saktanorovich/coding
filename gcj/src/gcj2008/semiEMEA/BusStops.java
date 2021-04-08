package gcj2008.semiEMEA;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class BusStops {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int k = in.nextInt();
        int p = in.nextInt();
        out.format("Case #%d: %d\n", testCase, get(n, k, p));
    }

    // From the problem statement we know that in each bus station exactly one
    // bus has to stop so we need to cover all stations. The key observation
    // is that we do not need to handle bus id's and it's enough to work only
    // with positions where each bus stops during his route. Here is why.
    // Let's consider an example from problem statement for n=5, k=2, p=3.
    // The initial state is S0=[*,*,-,-,-]. The 1st bus can move to 3 and 4
    // so the next states are S1=[-,*,*,-,-] and S2=[-,*,-*,-]. Because 3 is
    // not covered by the 1st bus in S2 the 2nd bus has to move into 3 so S2
    // is equivalent to S4=[-,-,*,*,-]. Also S4 can be reached from S1 by the
    // 2nd bus. So we can reach S4 in two ways. From the other hand the 2nd
    // bus can move into 3 and 4 resulting to the states S5=[*,-,*,-,-] and
    // S6=[*,-,-,*,-] which are equivalent to the state S4. Because S5 and S2
    // uses the same routes we can reach S4 in 3 steps. From S4 we can reach
    // final state [-,-,-,*,*] only in one step. So the answer is 3.
    private static int get(int n, int k, int p) {
        Profile[] profiles = Profile.enumerate(p, k);
        int[] index = new int[1 << p];
        Arrays.fill(index, -1);
        for (int i = 0; i < profiles.length; ++i) {
            index[profiles[i].value()] = i;
        }
        int P = profiles.length;
        int U = 0;
        int L = 0;
        for (int i = 0; i < k; ++i) {
            U |= 1 << i;
        }
        if (n < 1000) {
            int f[][] = new int[2][P];
            f[0][index[U]] = 1;
            for (int i = k - 1; i + 1 < n; ++i) {
                for (int last = 0; last < P; ++last) {
                    if (f[L][last] > 0) {
                        for (Profile t : profiles[last].next(p, k)) {
                            int next = index[t.value()];
                            f[L ^ 1][next] += f[L][last];
                            f[L ^ 1][next] %= MOD;
                        }
                    }
                }
                Arrays.fill(f[L], 0);
                L ^= 1;
            }
            return f[L][index[U]];
        } else {
            int f[][] = new int[P][P];
            for (int last = 0; last < P; ++last) {
                for (Profile t : profiles[last].next(p, k)) {
                    int next = index[t.value()];
                    f[last][next] = 1;
                }
            }
            int s[] = new int[P];
            s[index[U]] = 1;
            f = pow(f, P, n - k);
            s = mul(f, s, P);
            return s[index[U]];
        }
    }

    private static int[][] pow(int[][] a, int n, int k) {
        if (k == 0) {
            return identity(n);
        } else if (k % 2 == 0) {
            return pow(mul(a, a, n), n, k / 2);
        } else {
            return mul(a, pow(a, n, k - 1), n);
        }
    }

    private static int[][] mul(int[][] a, int[][] b, int n) {
        int[][] c = new int[n][n];
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                for (int k = 0; k < n; ++k) {
                    c[i][j] += a[i][k] * b[k][j];
                    c[i][j] %= MOD;
                }
            }
        }
        return c;
    }

    private static int[] mul(int[][] a, int[] b, int n) {
        int[] c = new int[n];
        for (int i = 0; i < n; ++i) {
            for (int k = 0; k < n; ++k) {
                c[i] += a[i][k] * b[k];
                c[i] %= MOD;
            }
        }
        return c;
    }

    private static int[][] identity(int n) {
        int[][] a = new int[n][n];
        for (int i = 0; i < n; ++i) {
            a[i][i] = 1;
        }
        return a;
    }

    private static final int MOD = 30031;

    // Describes set of buses inside a window of length p. We
    // assume that the buses are placed to the left relatively
    // to the current position.
    private static class Profile {
        private final int value[];

        public Profile(int[] value) {
            this.value = value;
        }

        public int value() {
            int set = 0;
            for (int i = 0; i < value.length; ++i) {
                set |= 1 << value[i];
            }
            return set;
        }

        public List<Profile> next(int p, int k) {
            List<Profile> res = new ArrayList();
            // let's move the window one position to the right
            // and put a bus to the rightmost pos
            for (int b = 0; b < k; ++b) {
                int nt[] = value.clone();
                for (int i = 0; i < k; ++i) {
                    ++nt[i];
                }
                nt[b] = 0;
                Profile pf = Profile.make(nt, p);
                if (pf != null) {
                    res.add(pf);
                }
            }
            return res;
        }

        public static Profile[] enumerate(int p, int k) {
            List<Profile> res = new ArrayList<>();
            for (int set = 0; set < 1 << p; ++set) {
                if (Integer.bitCount(set) == k) {
                    int[] value = new int[k];
                    for (int i = 0, j = 0; i < p; ++i) {
                        if ((set & (1 << i)) != 0) {
                            value[j++] = i;
                        }
                    }
                    res.add(new Profile(value));
                }
            }
            return res.toArray(new Profile[res.size()]);
        }

        private static Profile make(int[] state, int p) {
            for (int i = 0; i < state.length; ++i) {
                if (state[i] >= p) {
                    return null;
                }
            }
            return new Profile(state);
        }
    }
}
