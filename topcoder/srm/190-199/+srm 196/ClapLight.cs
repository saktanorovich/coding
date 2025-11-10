using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ClapLight {
        public int threshold(int[] background) {
            for (var lowest = 0; lowest <= 1001; ++lowest) {
                if (satisfy(background, lowest)) {
                    return lowest;
                }
            }
            throw new Exception();
        }

        private static bool satisfy(int[] background, int lowest) {
            var halfLow = 0;
            foreach (var reading in background) {
                if (reading < lowest) {
                    ++halfLow;
                }
            }
            if (2 * halfLow > background.Length) {
                for (var i = 0; i + 4 <= background.Length; ++i) {
                    if (background[i + 0] < lowest &&
                        background[i + 3] < lowest) {
                        if (background[i + 1] >= lowest &&
                            background[i + 2] >= lowest) {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}