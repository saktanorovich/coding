using System;

namespace TopCoder.Algorithm {
    public class DinkyFish {
        public int monthsUntilCrowded(int tankVolume, int maleNum, int femaleNum) {
            if (maleNum + femaleNum > 2 * tankVolume) {
                return 0;
            }
            return 1 + monthsUntilCrowded(tankVolume, maleNum + Math.Min(maleNum, femaleNum), femaleNum + Math.Min(maleNum, femaleNum));
        }
    }
}