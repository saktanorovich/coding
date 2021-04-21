using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Removal {
        public int finalPos(int n, int k, string[] remove) {
            return finalPos(new Range(1, n), Array.ConvertAll(remove, Range.Parse), k);
        }

        private int finalPos(Range origin, Range[] remove, int final) {
            var ranges = new List<Range> { origin };
            foreach (var toremove in remove) {
                var next = new List<Range>();
                var lpos = toremove.Lo;
                var hpos = toremove.Hi;
                for (var i = 0; i < ranges.Count; ++i) {
                    if (lpos > ranges[i].Length()) {
                        lpos = lpos - ranges[i].Length();
                        hpos = hpos - ranges[i].Length();
                        next.Add(ranges[i]);
                        continue;
                    }
                    add(next, ranges[i].Lo, ranges[i].At(lpos - 1));
                    for (; i < ranges.Count; ++i) {
                        if (hpos > ranges[i].Length()) {
                            hpos = hpos - ranges[i].Length();
                            continue;
                        }
                        add(next, ranges[i].At(hpos + 1), ranges[i].Hi);
                        for (++i; i < ranges.Count; ++i) {
                            next.Add(ranges[i]);
                        }
                    }
                    break;
                }
                ranges = next;
            }
            foreach (var range in ranges) {
                if (final > range.Length()) {
                    final = final - range.Length();
                    continue;
                }
                return range.At(final);
            }
            return -1;
        }

        private static void add(IList<Range> list, int lo, int hi) {
            if (lo <= hi) {
                list.Add(new Range(lo, hi));
            }
        }

        private struct Range {
            public readonly int Lo;
            public readonly int Hi;

            public Range(int lo, int hi) {
                Lo = lo;
                Hi = hi;
            }

            public int Length() {
                return Hi - Lo + 1;
            }

            public int At(int pos) {
                return Lo + pos - 1;
            }

            public static Range Parse(string s) {
                var data = s.Split('-');
                var lo = int.Parse(data[0]);
                var hi = int.Parse(data[1]);
                return new Range(lo, hi);
            }
        }
    }
}