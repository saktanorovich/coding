using System;

namespace TopCoder.Algorithm {
    public class GolfScore {
        public int tally(int[] parValues, string[] scoreSheet) {
            var res = 0;
            for (var i = 0; i < 18; ++i) {
                switch (scoreSheet[i]) {
                    case "triple bogey": res += parValues[i] + 3; break;
                    case "double bogey": res += parValues[i] + 2; break;
                    case "bogey":        res += parValues[i] + 1; break;
                    case "par":          res += parValues[i] + 0; break;
                    case "birdie":       res += parValues[i] - 1; break;
                    case "eagle":        res += parValues[i] - 2; break;
                    case "albatross":    res += parValues[i] - 3; break;
                    case "hole in one":  res += 1; break;
                }
            }
            return res;
        }
    }
}