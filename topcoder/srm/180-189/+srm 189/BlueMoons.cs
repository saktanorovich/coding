using System;

namespace TopCoder.Algorithm {
    public class BlueMoons {
        public int count(string interval, string fullMoon) {
            var data = interval.Split(new [] { " to " }, StringSplitOptions.RemoveEmptyEntries);
            var beg = parse(data[0], 0, 00, 00, 00);
            var end = parse(data[1], 1, 23, 59, 59);
            return count(beg, end, parse(fullMoon));
        }

        private int count(DateTime beg, DateTime end, DateTime fullMoon) {
            var moons = new int[10000 * 12];
            for (var current = fullMoon; current <= end; current += delta) {
                ++moons[encode(current)];
                if (current > DateTime.MaxValue - delta) {
                    break;
                }
            }
            for (var current = fullMoon; current >= beg; current -= delta) {
                ++moons[current.Year * 12 + current.Month - 1];
                if (current < DateTime.MinValue + delta) {
                    break;
                }
            }
            --moons[fullMoon.Year * 12 + fullMoon.Month - 1];
            var blueMoons = 0;
            for (var date = encode(beg); date <= encode(end); ++date)
                if (moons[date] > 1) {
                    ++blueMoons;
                }
            return blueMoons;
        }

        private static int encode(DateTime dateTime) {
            return dateTime.Year * 12 + dateTime.Month - 1;
        }

        private static DateTime parse(string s) {
            var data = s.Split(new[] { '.', '/' });
            var dd = int.Parse(data[0]);
            var ff = int.Parse(data[1]);
            var mm = int.Parse(data[2]);
            var yy = int.Parse(data[3]);
            return new DateTime(yy, mm, dd, 0, 0, 0) + TimeSpan.FromSeconds(864 * ff);
        }

        private static DateTime parse(string s, int last, int hh, int mi, int ss) {
            var data = s.Split('/');
            var mm = int.Parse(data[0]);
            var yy = int.Parse(data[1]);
            return new DateTime(yy, mm, 1 + last * (DateTime.DaysInMonth(yy, mm) - 1), hh, mi, ss);
        }

        private static readonly TimeSpan delta = TimeSpan.FromSeconds(29.53 * 24 * 60 * 60);
    }
}