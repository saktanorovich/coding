using System;

namespace TopCoder.Algorithm {
    public class TwoTrains {
        public int passengerNewVasyuki(int t1, int t2, int[] times) {
            var train = new int[MaxTime + 1];
            for (var k = 0; k * t2 <= MaxTime; ++k) {
                train[k * t2] = 2;
            }
            for (var k = 0; k * t1 <= MaxTime; ++k) {
                train[k * t1] = 1;
            }
            for (var t = MaxTime - 1; t >= 0; --t) {
                if (train[t] == 0) {
                    train[t] = train[t + 1];
                }
            }
            var result = 0;
            foreach (var time in times) {
                if (train[time] == 1) {
                    ++result;
                }
            }
            return result;
        }

        private const int MaxTime = 20000;
    }
}