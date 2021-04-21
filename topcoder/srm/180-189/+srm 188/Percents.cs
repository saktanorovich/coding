using System;

namespace TopCoder.Algorithm {
    public class Percents {
        public int minSamples(string percent) {
            return minSamples(Convert.ToInt32(double.Parse(percent.Substring(0, 5)) * 100));
        }

        private static int minSamples(int percent) {
            for (var people = 1; people <= 10000; ++people) {
                for (var yes = 0; yes <= people; ++yes) {
                    if ((int)(100.0 * 100 * yes / people + 0.5) == percent) {
                        return people;
                    }
                }
            }
            throw new Exception();
        }
    }
}