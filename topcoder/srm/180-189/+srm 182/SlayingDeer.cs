using System;

namespace TopCoder.Algorithm {
    public class SlayingDeer {
        public int getTime(int wild, int deer, int behind) {
            /* in the worst case the distance is decreased every 45 minutes during 15 minutes.. */
            for (int time = 0, running = 0; time <= 2 * 300 * 1000; ++time) {
                if (running >= behind) {
                    return time;
                }
                running += wild;
                if (time % 45 < 30) {
                    behind += deer;
                }
            }
            return -1;
        }
    }
}