using System;

namespace TopCoder.Algorithm {
    public class Swimmers {
        public int[] getSwimTimes(int[] distances, int[] speeds, int current) {
            var result = new int[distances.Length];
            for (var i = 0; i < result.Length; ++i) {
                result[i] = -1;
                if (speeds[i] > current) {
                    result[i] = (int)(1.0 * distances[i] / (speeds[i] + current) + 1.0 * distances[i] / (speeds[i] - current));
                }
                else if (distances[i] == 0) {
                    result[i] = 0;
                }
            }
            return result;
        }
    }
}