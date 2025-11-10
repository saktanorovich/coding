using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class ElevatorLimit {
        public int[] getRange(int[] enter, int[] exit, int physicalLimit) {
            int min = 0, max = 0, elevator = 0;
            for (var i = 0; i < enter.Length; ++i) {
                elevator -= exit[i];
                min = Math.Min(min, elevator);
                elevator += enter[i];
                max = Math.Max(max, elevator);
            }
            if (-min <= physicalLimit - max) {
                return new[] { -min, physicalLimit - max };
            }
            return new int[0];
        }
    }
}