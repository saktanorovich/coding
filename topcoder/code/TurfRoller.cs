using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class TurfRoller {
        public int stripNum(int lawnWidth, int lawnHeight, int stripAngle, int stripLength, int stripBreadth) {
            return stripNumImpl(lawnWidth, lawnHeight, stripAngle, stripLength, stripBreadth);
        }

        private static int stripNumImpl(int w, int h, int a, int l, int b) {
            if (a > 0) {
                if (a < 90) {
                    return stripNumImpl(new Rect(w, h).rotate(-1 * toRadians(a)), l, b);
                }
                return stripNumImpl(h, w, 0, l, b);
            }
            int perW = (w + l - 1) / l;
            int perH = (h + b - 1) / b;
            return perW * perH;
        }

        private static int stripNumImpl(Rect lawn, int l, int b) {
            return 0;
        }

        private static double toRadians(double a) {
            return Math.PI* a / 180;
        }

        private class Rect {
            public readonly Point[] p ;

            private Rect(Point[] p) {
                this.p = p;
            }

            public Rect(int w, int h) {
                this.p = new[] {
                    new Point(0, 0),
                    new Point(0, h),
                    new Point(w, h),
                    new Point(w, 0),
                };
            }

            public Rect rotate(double angel) {
                Point[] r = new Point[4];
                for (int i = 0; i < 4; ++i) {
                    r[i] = p[i].rotate(angel);
                }
                return new Rect(r);
            }
        }

        private class Point {
            public readonly double x;
            public readonly double y;

            public Point(double x, double y) {
                this.x = x;
                this.y = y;
            }

            public Point rotate(double angel) {
                return new Point(
                    x * Math.Cos(angel) - y * Math.Sin(angel),
                    x * Math.Sin(angel) + y * Math.Cos(angel));
            }
        }
    }
}
