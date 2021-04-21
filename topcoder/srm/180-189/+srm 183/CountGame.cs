using System;

namespace TopCoder.Algorithm {
    public class CountGame {
        public int howMany(int maxAdd, int goal, int next) {
            if (goal >= next) {
                if (goal - next + 1 <= maxAdd) {
                    return goal - next + 1;
                }
                return howMany(maxAdd, goal - (maxAdd + 1), next);
            }
            return -1;
        }
    }
}
