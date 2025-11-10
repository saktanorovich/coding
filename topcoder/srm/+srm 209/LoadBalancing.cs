using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class LoadBalancing {
        public int minTime(int[] chunkSizes) {
            return 1024 * minTime(chunkSizes.Select(chunk => chunk / 1024).OrderBy(chunk => chunk).ToArray(), chunkSizes.Length);
        }

        private static int minTime(int[] chunkSizes, int n) {
            var possible = new bool[chunkSizes.Sum() + 1]; possible[0] = true;
            var total = 0;
            for (var chunk = 0; chunk < n; ++chunk) {
                total += chunkSizes[chunk];
                for (var sum = possible.Length - 1; sum >= 0; --sum) {
                    if (possible[sum]) {
                        possible[sum + chunkSizes[chunk]] = true;
                    }
                }
            }
            for (var sum = (total + 1) / 2; sum < possible.Length; ++sum) {
                if (possible[sum]) {
                    return sum;
                }
            }
            throw new Exception();
        }
    }
}
