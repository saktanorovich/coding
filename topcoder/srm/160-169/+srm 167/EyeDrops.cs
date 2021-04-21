using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class EyeDrops {
        public double closest(int sleepTime, int k) {
            if (k * sleepTime <= 24) {
                return 60.0 * 24.0 / k;
            }
            return 60.0 * (24.0 - sleepTime) / (k - 1);
        }
    }
}