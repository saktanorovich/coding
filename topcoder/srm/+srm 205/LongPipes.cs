using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class LongPipes {
        public int fewestWelds(string[] segments, string desiredLength) {
            return fewestWelds(segments.Select(segment => parse(segment)), segments.Length, parse(desiredLength));
        }

        private static int fewestWelds(IEnumerable<long> segments, int n, long length) {
            return fewestWelds(segments.Take((n - 1) / 2).ToArray(), segments.Skip((n - 1)/ 2).ToArray(), length);
        }

        private static int fewestWelds(long[] s1, long[] s2, long length) {
            var map = new Dictionary<long, int>();
            iterate(s1, (len, cnt) => {
                if (map.ContainsKey(len) == false || map[len] > cnt) {
                    map[len] = cnt;
                }
            });
            var res = int.MaxValue;
            iterate(s2, (len, cnt) => {
                if (map.ContainsKey(length - len)) {
                    res = Math.Min(res, cnt + map[length - len]);
                }
            });
            if (res < int.MaxValue) {
                return res - 1;
            }
            return -1;
        }

        private static void iterate(long[] s, Action apply) {
            for (var set = 0; set < 1 << s.Length; ++set) {
                var len = 0L;
                var cnt = 0;
                for (var i = 0; i < s.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        len += s[i];
                        cnt += 1;
                    }
                }
                apply(len, cnt);
            }
        }

        private static long parse(string length) {
            length = length.Replace(".", "");
            return long.Parse(length);
        }

        private delegate void Action(long len, int cnt);
    }
}
