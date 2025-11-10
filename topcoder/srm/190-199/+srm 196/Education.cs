using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Education {
        public int minimize(string desire, int[] tests) {
            for (var lowest = 0; lowest <= 100; ++lowest) {
                if (min(desire) * (tests.Length + 1) <= tests.Sum() + lowest)
                    return lowest;
            }
            return -1;
        }

        private static int min(string grade) {
            if (grade == "A") return 90;
            if (grade == "B") return 80;
            if (grade == "C") return 70;
            if (grade == "D") return 60;
            throw new Exception();
        }
    }
}