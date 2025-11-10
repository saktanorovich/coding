using System;

namespace TopCoder.Algorithm {
    public class BadClock {
        public double nextAgreement(string trueTime, string skewTime, int hourlyGain) {
            return nextAgreement(parse(trueTime), parse(skewTime), hourlyGain);
        }

        private static double nextAgreement(int trueTime, int skewTime, int hourlyGain) {
            var diff = trueTime - skewTime;
            if (hourlyGain < 0) {
                diff = skewTime - trueTime;
                hourlyGain = -hourlyGain;
            }
            if (diff < 0) {
                diff += 43200;
            }
            return 1.0 * diff / hourlyGain;
        }

        private static int parse(string time) {
            var data = time.Split(':');
            var hh = int.Parse(data[0]);
            var mm = int.Parse(data[1]);
            var ss = int.Parse(data[2]);
            return hh * 60 * 60 + mm * 60 + ss;
        }
    }
}