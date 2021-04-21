using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class CombinationLock {
        public double degreesTurned(int[] combo, int size, int start) {
            return degreesTurned(new List<int>(combo), size, start, +1) / size;
        }

        private static double degreesTurned(IList<int> combo, int size, int start, int direction) {
            var result = 0.0;
            if (combo.Count > 0) {
                result += combo.Count * 360 * size;
                while (start != combo[0]) {
                    start += direction;
                    start += size;
                    start %= size;
                    result += 360;
                }
                combo.RemoveAt(0);
                result += degreesTurned(combo, size, start, -direction);
            }
            return result;
        }
    }
}