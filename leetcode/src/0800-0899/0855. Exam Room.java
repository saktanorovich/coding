class ExamRoom {
    private final TreeSet<Integer> taken;
    private final TreeSet<Segment> seats;
    private final int n;

    public ExamRoom(int n) {
        this.n = n - 1;
        this.taken = new TreeSet<>();
        this.seats = new TreeSet<>((a, b) -> {
            var da = eval(a);
            var db = eval(b);
            if (db == da) {
                return a.l - b.l;
            }
            return -(da - db);
        });
        this.seats.add(new Segment(0, n - 1));
    }

    public int seat() {
        var segm = seats.pollFirst();
        var indx = indx(segm);
        if ((indx - 1) - segm.l >= 0) {
            seats.add(new Segment(segm.l, indx - 1));
        }
        if (segm.r - (indx + 1) >= 0) {
            seats.add(new Segment(indx + 1, segm.r));
        }
        taken.add(indx);
        return indx;
    }

    public void leave(int p) {
        taken.remove(p);
        var l = taken.lower(p);
        if (l == null) {
            l = 0 - 1;
        }
        var r = taken.higher(p);
        if (r == null) {
            r = n + 1;
        }
        seats.remove(new Segment(l + 1, p - 1));
        seats.remove(new Segment(p + 1, r - 1));
        seats.add(new Segment(l + 1, r - 1));
    }

    private int indx(Segment s) {
        if (s.l == 0) { return 0; }
        if (s.r == n) { return n; }
        return (s.l + s.r) / 2;
    }

    private int eval(Segment s) {
        var d = s.r - s.l;
        if (s.l == 0) return d;
        if (s.r == n) return d;
        return d / 2;
    }

    private final class Segment {
        public final int l;
        public final int r;

        public Segment(int l, int r) {
            this.l = l;
            this.r = r;
        }
    }
}