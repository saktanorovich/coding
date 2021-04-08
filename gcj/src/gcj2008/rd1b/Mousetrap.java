package gcj2008.rd1b;

import utils.*;
import utils.io.*;
import java.io.*;
import java.util.*;

// Problem C
public class Mousetrap {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int K = in.nextInt();
        int n = in.nextInt();
        Integer[] deck = doit(K);
        out.format("Case #%d:", testCase);
        for (int i = 0; i < n; ++i) {
            out.format(" %d", deck[in.nextInt() - 1]);
        }
        out.println();
    }

    private Integer[] doit(int k) {
        if (k <= 1000) {
            return l2f(k);
        }
        if (k <= 2000) {
            return f2l(k);
        }
        // cyclic shift is more difficult operation than removing
        // an object at the specified index, however both operations
        // can be efficiently implemented using Cartesian tree (Treap)
        return get(k);
    }

    // simple simulation from the last to the first card
    private Integer[] l2f(int k) {
        LinkedList<Integer> deck = new LinkedList<>();
        for (int c = k; c >= 1; --c) {
            deck.addFirst(c);
            int m = (c - 1) % (k - c + 1);
            for (int i = 0; i < m; ++i) {
                deck.addFirst(deck.removeLast());
            }
        }
        return deck.toArray(new Integer[k]);
    }

    // simple simulation from the first to the last card
    private Integer[] f2l(int k) {
        LinkedList<Integer> deck = new LinkedList<>();
        for (int c = 0; c < k; ++c) {
            deck.add(c);
        }
        Integer[] res = new Integer[k];
        for (int c = 0, p = 0; c < k; ++c) {
            p = (p + c) % (k - c);
            res[deck.remove(p)] = c + 1;
        }
        return res;
    }

    private Integer[] get(int k) {
        Array<Integer> deck = new Array<>();
        for (int c = 0; c < k; ++c) {
            deck.add(c);
        }
        Integer[] res = new Integer[k];
        for (int c = 0, p = 0; c < k; ++c) {
            p = (p + c) % (k - c);
            res[deck.remove(p)] = c + 1;
        }
        return res;
    }

    // Array implementation based on Treap with implicit keys.
    private static class Array<T> {
        private Treap<T> root = Treap.EMPTY;

        public void add(T value) {
            root = Treap.merge(root, new Treap<>(value));
        }

        public T remove(int index) {
            Pair<Treap<T>> pair = root.split(index);
            Treap<T> L = pair.item1;
            pair = pair.item2.split(1);
            Treap<T> R = pair.item2;
            root = Treap.merge(L, R);
            return pair.item1.value;
        }
    }

    private static class Treap<T> {
        public static final Treap EMPTY = new Treap();

        private final double prio;
        private final T value;
        private final Treap<T> le;
        private final Treap<T> ri;
        private int size;

        private Treap() {
            this.value = null;
            this.le = null;
            this.ri = null;
            this.prio = 0;
            this.size = 0;
        }

        private Treap(T value, double prio, Treap<T> le, Treap<T> ri) {
            this.value = value;
            this.prio = prio;
            this.le = le;
            this.ri = ri;
            this.size = 1 + le.size + ri.size;
        }

        public Treap(T value) {
            this.prio = Math.random();
            this.value = value;
            this.size = 1;
            this.le = EMPTY;
            this.ri = EMPTY;
        }

        // Split the tree into two trees with keys [0, key) and [key..n)
        // where n is a number of elements in the tree.
        public Pair<Treap<T>> split(int key) {
            if (this == EMPTY) {
                if (key == 0) {
                    return new Pair<Treap<T>>(EMPTY, EMPTY);
                }
                throw new RuntimeException();
            }
            int index = le.size + 1;
            if (index <= key) {
                Pair<Treap<T>> pair = ri.split(key - index);
                pair.item1 = new Treap<>(value, prio, le, pair.item1);
                return pair;
            } else {
                Pair<Treap<T>> pair = le.split(key);
                pair.item2 = new Treap<>(value, prio, pair.item2, ri);
                return pair;
            }
        }

        // Merge two trees into one.
        public static <T> Treap<T> merge(Treap<T> le, Treap<T> ri) {
            if (le == EMPTY) return ri;
            if (ri == EMPTY) return le;
            if (le.prio > ri.prio) {
                return new Treap<T>(le.value, le.prio, le.le, merge(le.ri, ri));
            } else {
                return new Treap<T>(ri.value, ri.prio, merge(le, ri.le), ri.ri);
            }
        }
    }
}
