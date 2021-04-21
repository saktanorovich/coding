using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Deranged {
        public long numDerangements(int[] nums) {
            var result = count(nums, nums.Length);
            for (var set = 1; set < 1 << nums.Length; ++set) {
                var subset = new List<int>();
                for (var x = 0; x < nums.Length; ++x) {
                    if ((set & (1 << x)) == 0) {
                        subset.Add(nums[x]);
                    }
                }
                if ((bits(set) & 1) == 1)
                    result -= count(subset, nums.Length);
                else
                    result += count(subset, nums.Length);
            }
            return result;
        }

        private static long count(IList<int> nums, int n) {
            var cumul = new int[n];
            foreach (var num in nums) {
                ++cumul[num];
            }
            var result = f(nums.Count);
            foreach (var count in cumul) {
                result /= f(count);
            }
            return result;
        }

        private static long f(int n) {
            if (n > 1) {
                return n * f(n - 1);
            }
            return 1;
        }

        private static int bits(int x) {
            if (x > 0) {
                return 1 + bits(x & (x - 1));
            }
            return 0;
        }
    }
}