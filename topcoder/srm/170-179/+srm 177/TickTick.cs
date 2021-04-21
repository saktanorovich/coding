using System;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class TickTick {
        public int count(string[] events) {
            return count(Array.ConvertAll(events, double.Parse));
        }

        private static int count(double[] events) {
            /* the problem ask to determine a number of distinct sequences produced by the
             * system assuming that the first tick can occur at any time from [0, 1].. */
            var hashSet = new HashSet<string>();
            foreach (var ev in events) {
                for (var i = 0; i < 2; ++i) {
                    var tick = ev + delta[i];
                    hashSet.Add(count(events, tick - (int)tick));
                }
            }
            return hashSet.Count;
        }

        private static string count(double[] events, double tickAtTime) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(compare(0, events[0], tickAtTime));
            for (var i = 1; i < events.Length; ++i) {
                stringBuilder.Append(compare(events[i], events[i - 1], tickAtTime));
            }
            return stringBuilder.ToString();
        }

        private static char compare(double a, double b, double tickAtTime) {
            var atick = (int)(a - tickAtTime + 1);
            var btick = (int)(b - tickAtTime + 1);
            if (atick != btick)
                return 'D';
            return 'S';
        }

        private static readonly double[] delta = { -1e-9, +1e-9 };
    }
}