using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class BadSubstring {
        public long howMany(int length) {
            if (length > 0) {
                var dp = new long[length + 1, 3];
                dp[0, 2] = 1;
                dp[1, 0] = 1;
                dp[1, 1] = 1;
                dp[1, 2] = 1;
                for (var len = 2; len <= length; ++len) {
                    dp[len, 0] += dp[len - 1, 0];
                    dp[len, 0] += dp[len - 1, 1];
                    dp[len, 0] += dp[len - 1, 2];

                    dp[len, 1] += dp[len - 1, 1];
                    dp[len, 1] += dp[len - 1, 2];

                    dp[len, 2] += dp[len - 1, 0];
                    dp[len, 2] += dp[len - 1, 1];
                    dp[len, 2] += dp[len - 1, 2];
                }
                var result = 0L;
                for (var last = 0; last < 3; ++last) {
                    result += dp[length, last];
                }
                return result;
            }
            return 1;
        }
    }
}