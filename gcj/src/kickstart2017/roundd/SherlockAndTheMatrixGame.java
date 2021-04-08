package kickstart2017.roundd;

import utils.io.*;
import java.io.*;
import java.util.*;
import java.util.function.*;

// Problem B
public class SherlockAndTheMatrixGame {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        int k = in.nextInt();
        int A = in.nextInt();
        int B = in.nextInt();
        int C = in.nextInt();
        int D = in.nextInt();
        int P = in.nextInt();
        int Q = in.nextInt();
        int F = in.nextInt();
        int x0, x1, y0, y1;
        int r0, r1, s0, s1;
        x1 = A;
        y1 = B;
        r1 = 0;
        s1 = 0;
        int a[] = new int[n];
        int b[] = new int[n];
        for (int i = 0; i < n; ++i) {
            x0 = x1;
            y0 = y1;
            r0 = r1;
            s0 = s1;
            a[i] = r0 == 0 ? +x0 : -x0;
            b[i] = s0 == 0 ? +y0 : -y0;
            x1 = (C * x0 + D * y0 + P) % F;
            y1 = (D * x0 + C * y0 + Q) % F;
            r1 = (C * r0 + D * s0 + P) % 2;
            s1 = (D * r0 + C * s0 + Q) % 2;
        }
        out.format("Case #%d: %d\n", testCase, get(a, b, n, k));
    }

    private long get(int[] a, int[] b, int n, int k) {
        Subset as;
        Subset bs;
        if (n <= 200) {
            as = new SubsetSmall(a);
            bs = new SubsetSmall(b);
        } else {
            as = new SubsetLarge(a);
            bs = new SubsetLarge(b);
        }
        return get(as.get(k), bs.get(k), k);
    }

    private Long get(Long[] pa, Long[] pb, int k) {
        Arrays.sort(pa);
        Arrays.sort(pb);
        int a = pa.length - 1;
        int b = pb.length - 1;
        PriorityQueue<State> q = new PriorityQueue<>();
        for (int i = 0; i <= a; ++i) {
            if (pa[i] < 0) {
                q.add(new State(i, 0, pa[i] * pb[0]));
            } else {
                q.add(new State(i, b, pa[i] * pb[b]));
            }
        }
        for (int i = 0; i < k - 1; ++i) {
            State s = q.poll();
            Integer x = s.a;
            Integer y = s.b;
            if (pa[x] < 0) {
                if (y + 1 <= b) q.add(new State(x, y + 1, pa[x] * pb[y + 1]));
            } else {
                if (y - 1 >= 0) q.add(new State(x, y - 1, pa[x] * pb[y - 1]));
            }
        }
        return q.peek().s;
    }

    private interface Subset {
        Long[] get(int k);
    }

    private class SubsetSmall implements Subset {
        private final List<Long> sums;

        public SubsetSmall(int[] a) {
            this.sums = new ArrayList<>();
            for (int i = 0; i < a.length; ++i) {
                Long s = 0L;
                for (int j = i; j < a.length; ++j) {
                    s += a[j];
                    sums.add(s);
                }
            }
        }

        public Long[] get(int k) {
            List<Long> res = new ArrayList<>();
            sums.sort(Comparator.naturalOrder());
            for (int i = 0; i < Math.min(sums.size(), k); ++i) {
                Long x = sums.get(i);
                if (x < 0) {
                    res.add(x);
                }
            }
            sums.sort(Comparator.reverseOrder());
            for (int i = 0; i < Math.min(sums.size(), k); ++i) {
                Long x = sums.get(i);
                if (x >= 0) {
                    res.add(x);
                }
            }
            return res.toArray(new Long[res.size()]);
        }
    }

    private class SubsetLarge implements Subset  {
        private final PSum[] sum;

        public SubsetLarge(int[] a) {
            long[] s = new long[a.length + 1];
            for (int i = 0; i < a.length; ++i) {
                s[i + 1] = s[i] + a[i];
            }
            this.sum = new PSum[s.length];
            for (int i = 0; i < s.length; ++i) {
                sum[i] = new PSum(i, s[i]);
            }
        }

        public Long[] get(int k) {
            List<Long> res = new ArrayList<>();
            res.addAll(get(Comparator.naturalOrder(), Comparator.reverseOrder(), x -> x != 0 && x < 0, k));
            res.addAll(get(Comparator.reverseOrder(), Comparator.naturalOrder(), x -> x == 0 || x > 0, k));
            return res.toArray(new Long[res.size()]);
        }

        private Collection<Long> get(Comparator<PSum> cmp, Comparator<Long> pri, Function<Long, Boolean> accept, int k) {
            Arrays.sort(sum, cmp);
            PriorityQueue<Long> q = new PriorityQueue<>(pri);
            for (int i = 0; i < sum.length; ++i) {
                for (int j = sum.length - 1; j > i; --j) {
                    if (sum[j].index < sum[i].index) {
                        long value = sum[i].value - sum[j].value;
                        if (accept.apply(value)) {
                            if (q.size() >= k && pri.compare(value, q.peek()) <= 0) {
                                break;
                            }
                            q.add(value);
                            while (q.size() > k) {
                                q.poll();
                            }
                        } else {
                            break;
                        }
                    }
                }
            }
            return q;
        }

        private class PSum implements Comparable<PSum> {
            public final Integer index;
            public final Long value;

            public PSum(int index, long value) {
                this.index = index;
                this.value = value;
            }

            @Override
            public int compareTo(PSum o) {
                if (value.compareTo(o.value) != 0) {
                    return value.compareTo(o.value);
                }
                return index.compareTo(o.index);
            }
        }
    }

    private class State implements Comparable<State> {
        public final Integer a;
        public final Integer b;
        public final Long s;

        public State(Integer a, Integer b, Long s) {
            this.a = a;
            this.b = b;
            this.s = s;
        }

        @Override
        public int compareTo(State o) {
            return -1 * s.compareTo(o.s);
        }
    }
}
