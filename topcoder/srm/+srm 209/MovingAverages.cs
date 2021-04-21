using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class MovingAverages {
        public int[] calculate(string[] times, int n) {
            return calculate(times.Select(TimeSpan.Parse).ToArray(), n);
        }

        private static int[] calculate(TimeSpan[] dateTime, int n) {
            var result = new List<int>();
            for (var i = 0; i + n <= dateTime.Length; ++i) {
                var seconds = dateTime
                    .Skip(i).Take(n)
                    .Select(time => time.TotalSeconds).Sum();
                result.Add((int)seconds / n);
            }
            return result.ToArray();
        }
    }
}
