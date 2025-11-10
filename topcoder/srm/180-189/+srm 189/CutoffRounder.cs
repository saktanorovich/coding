using System;

namespace TopCoder.Algorithm {
    public class CutoffRounder {
        public int round(string num, string cutoff) {
            var pos = num.IndexOf('.');
            if (pos >= 0) {
                if (pos > 0) {
                    return round(int.Parse(num.Substring(0, pos)), fraction(num), fraction(cutoff));
                }
                return round(0, fraction(num), fraction(cutoff));
            }
            return int.Parse(num);
        }

        private static int round(int num, string fra, string cut) {
            var len = Math.Max(fra.Length, cut.Length);
            if (fra.Length != cut.Length) {
                return round(num, fra.PadRight(len, '0'), cut.PadRight(len, '0'));
            }
            for (var i = 0; i < len; ++i) {
                if (fra[i] > cut[i])
                    return num + 1;
                if (fra[i] < cut[i])
                    break;
            }
            return num;
        }

        private static string fraction(string num) {
            return num.Substring(num.IndexOf('.') + 1);
        }
    }
}