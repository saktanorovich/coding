using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using yandex.utils;

namespace yandex.blitz2017.qual {
    public class Problem05 {
        public bool process(int testCase, StreamReader reader, StreamWriter writer) {
            var n = int.Parse(reader.ReadLine());
            var p = new Point3[n];
            for (var i = 0; i < n; ++i) {
                var s = reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var x = int.Parse(s[0]);
                var y = int.Parse(s[1]);
                var z = int.Parse(s[2]);
                p[i] = new Point3(x, y, z);
            }
            var e = new int[n];
            for (var i = 0; i < n; ++i) {
                e[i] = i;
            }
            foreach (var o in MathUtils.Permute(e)) {
                if (inv(o) % 2 == 0) {
                    continue;
                }
                if (accept(p, o)) {
                    for (var i = 0; i < n; ++i) {
                        writer.Write(o[i] + 1);
                        if (i + 1 < n) {
                            writer.Write(" ");
                        }
                    }
                    writer.WriteLine();
                    return true;
                }
            }
            throw new InvalidOperationException();
        }

        private static bool accept(Point3[] p, int[] o) {
            var res = false;
            res |= simple(p.Select(a => new Point2(a.x, a.y)).ToArray(), o) == false;
            res |= simple(p.Select(a => new Point2(a.x, a.z)).ToArray(), o) == false;
            res |= simple(p.Select(a => new Point2(a.y, a.z)).ToArray(), o) == false;
            return res;
        }

        private static bool simple(Point2[] p, int[] o) {
            var n = p.Length - 1;
            if (inc(p, p[o[0]], o)) {
                return true;
            }
            if (dec(p, p[o[n]], o)) {
                return true;
            }
            return false;
        }

        private static bool inc(Point2[] p, Point2 b, int[] o) {
            for (var i = 1; i < p.Length; ++i) {
                if (dist(p[o[i]], b) < dist(p[o[i - 1]], b)) {
                    return false;
                }
            }
            return true;
        }

        private static bool dec(Point2[] p, Point2 b, int[] o) {
            for (var i = 1; i < p.Length; ++i) {
                if (dist(p[o[i]], b) > dist(p[o[i - 1]], b)) {
                    return false;
                }
            }
            return true;
        }

        private static int inv(int[] o) {
            var res = 0;
            for (var i = 0; i < o.Length; ++i) {
                for (var j = i + 1; j < o.Length; ++j) {
                    if (o[i] > o[j]) {
                        ++res;
                    }
                }
            }
            return res;
        }

        private static long dist(Point2 a, Point2 b) {
            long x = a.x - b.x;
            long y = a.y - b.y;
            return x * x + y * y;
        }

        private struct Point3 {
            public readonly int x;
            public readonly int y;
            public readonly int z;

            public Point3(int x, int y, int z) {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        private struct Point2 {
            public readonly int x;
            public readonly int y;

            public Point2(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        private static class MathUtils {
            public static IEnumerable<T[]> Permute<T>(T[] list) {
                if (list.Length == 0) {
                    yield return new T[0];
                }
                else {
                    for (var i = 0; i < list.Length; ++i) {
                        foreach (var res in Permute(Remove(list, i))) {
                            yield return Concat(list[i], res);
                        }
                    }
                }
            }

            private static T[] Concat<T>(T item, IEnumerable<T> list) {
                var result = new List<T>(new[] { item });
                result.AddRange(list);
                return result.ToArray();
            }

            private static T[] Remove<T>(IEnumerable<T> list, int index) {
                var result = new List<T>(list);
                result.RemoveAt(index);
                return result.ToArray();
            }
        }

        public void generate(StreamWriter writer, int testCases) {
            bool contains(IEnumerable<Point2> ts, Point2 p) {
                foreach (var t in ts) {
                    if (t.x == p.x && t.y == p.y) {
                        return true;
                    }
                }
                return false;
            }

            var rand = new Random(50847534);
            for (var testCase = 1; testCase <= testCases; ++testCase) {
                var n = rand.Next(1000) + 1;
                if (n < 3) {
                    n = 3;
                }
                writer.WriteLine(n);
                var pxy = new List<Point2>();
                var pxz = new List<Point2>();
                var pyz = new List<Point2>();
                for (var i = 0; i < n;) {
                    var x = rand.Next(10000);
                    var y = rand.Next(10000);
                    var z = rand.Next(10000);

                    if (contains(pxy, new Point2(x, y))) continue;
                    if (contains(pxz, new Point2(x, z))) continue;
                    if (contains(pyz, new Point2(y, z))) continue;

                    writer.WriteLine(String.Format("{0} {1} {2}", x, y, z));
                    pxy.Add(new Point2(x, y));
                    pxz.Add(new Point2(x, z));
                    pyz.Add(new Point2(y, z));
                    i = i + 1;
                }
            }
        }
    }
}
