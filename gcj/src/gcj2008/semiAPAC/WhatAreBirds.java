package gcj2008.semiAPAC;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem A
public class WhatAreBirds {
    public void process(int testCase, InputReader in, PrintWriter out) {
        this.n = in.nextInt();
        this.T = new ArrayList<>();
        this.F = new ArrayList<>();
        this.X = new ArrayList<>();
        for (int i = 0; i < n; ++i) {
            String s = in.nextLine();
            String t[] = s.split(" ");
            int h = Integer.parseInt(t[0]);
            int w = Integer.parseInt(t[1]);
            if (t[2].equals("BIRD")) {
                T.add(new Animal(h, w));
            } else {
                F.add(new Animal(h, w));
            }
        }
        this.m = in.nextInt();
        for (int i = 0; i < m; ++i) {
            int h = in.nextInt();
            int w = in.nextInt();
            X.add(new Animal(h, w));
        }
        out.format("Case #%d:\n", testCase);
        if (T.size() > 0) {
            AnimalRange r = new AnimalRange();
            for (Animal t : T) {
                r = r.apply(t);
            }
            for (Animal x : X) {
                out.println(VERDICT[def(r, x)]);
            }
        } else {
            for (Animal x : X) {
                if (has(F, x)) {
                    out.println(VERDICT[1]);
                } else {
                    out.println(VERDICT[2]);
                }
            }
        }
    }

    private int def(AnimalRange r, Animal x) {
        if (r.has(x)) {
            return 0;
        }
        r = r.apply(x);
        for (Animal f : F) {
            if (r.has(f)) {
                return 1;
            }
        }
        return 2;
    }

    private boolean has(List<Animal> L, Animal x) {
        for (Animal a : L) {
            if (x.equals(a)) {
                return true;
            }
        }
        return false;
    }

    private List<Animal> T;
    private List<Animal> F;
    private List<Animal> X;
    private int n;
    private int m;

    private class Animal {
        public final int H;
        public final int W;

        public Animal(int h, int w) {
            H = h;
            W = w;
        }

        @Override
        public boolean equals(Object obj) {
            Animal a = (Animal)obj;
            if (H == a.H && W == a.W) {
                return true;
            }
            return false;
        }
    }

    private class AnimalRange {
        private int Hmin;
        private int Hmax;
        private int Wmin;
        private int Wmax;

        public AnimalRange() {
            this.Hmin = Integer.MAX_VALUE;
            this.Hmax = Integer.MIN_VALUE;
            this.Wmin = Integer.MAX_VALUE;
            this.Wmax = Integer.MIN_VALUE;
        }

        public boolean has(Animal x) {
            boolean h = Hmin <= x.H && x.H <= Hmax;
            boolean w = Wmin <= x.W && x.W <= Wmax;
            if (h & w) {
                return true;
            }
            return false;
        }

        public AnimalRange apply(Animal x) {
            AnimalRange res = new AnimalRange();
            res.Hmin = Math.min(Hmin, x.H);
            res.Hmax = Math.max(Hmax, x.H);
            res.Wmin = Math.min(Wmin, x.W);
            res.Wmax = Math.max(Wmax, x.W);
            return res;
        }
    }

    private static final String VERDICT[] = {
        "BIRD", "NOT BIRD", "UNKNOWN"
    };
}
