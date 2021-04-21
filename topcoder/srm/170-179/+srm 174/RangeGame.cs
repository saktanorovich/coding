using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class RangeGame {
        public int[] bestDoors(string[] possible, string[] hints) {
            Converter<string, Range[]> parse = doors => {
                return Array.ConvertAll(doors.Split(' '), Range.Parse);
            };
            return bestDoors(Array.ConvertAll(possible, parse), Array.ConvertAll(hints, parse));
        }

        private int[] bestDoors(IList<Range[]> possible, IList<Range[]> hints) {
            var result = new List<int>();
            result.Add(getBestDoor(possible));
            for (var i = 0; i < hints.Count; ++i) {
                possible = apply(possible, hints[i]);
                result.Add(getBestDoor(possible));
            }
            return result.ToArray();
        }

        private static IList<Range[]> apply(IList<Range[]> possible, Range[] hint) {
            var result = new List<Range[]>();
            foreach (var doors in possible) {
                var list = new List<Range>();
                foreach (var range in doors) {
                    foreach (var open in hint) {
                        if (range.HasPoint(open)) {
                            goto skip;
                        }
                    }
                }
                result.Add(doors); skip:;
            }
            return result;
        }

        private static int getBestDoor(IList<Range[]> possible) {
            var cumul = new Dictionary<int, int>();
            foreach (var scan in possible) {
                foreach (var doors in scan) {
                    cumul[doors.Lo] = 0;
                    foreach (var compare in possible) {
                        foreach (var range in compare) {
                            if (range.Lo <= doors.Lo && doors.Lo <= range.Hi) {
                                ++cumul[doors.Lo];
                            }
                        }
                    }
                }
            }
            var maxFrequence = 1;
            foreach (var frequence in cumul.Values) {
                maxFrequence = Math.Max(maxFrequence, frequence);
            }
            var bestDoor = (int)2e9;
            foreach (var entry in cumul) {
                if (entry.Value == maxFrequence) {
                    bestDoor = Math.Min(bestDoor, entry.Key);
                }
            }
            return bestDoor;
        }

        private class Range {
            public readonly int Lo;
            public readonly int Hi;

            public Range(int lo, int hi) {
                Lo = lo;
                Hi = hi;
            }

            public bool HasPoint(Range other) {
                if (Hi <= other.Hi) {
                    return other.Lo <= Hi;
                }
                return other.HasPoint(this);
            }

            public static Range Parse(string s) {
                if (s.IndexOf('-') >= 0) {
                    var doors = s.Split('-');
                    return new Range(int.Parse(doors[0]), int.Parse(doors[1]));
                }
                return new Range(int.Parse(s), int.Parse(s));
            }
        }
    }
}