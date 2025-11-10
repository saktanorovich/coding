using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class MovieRating {
        public double calculate(int[] marks, int lowCount, int highCount) {
            var ordered = marks.OrderBy(x => x).AsEnumerable();
            ordered = ordered.Skip(lowCount);
            ordered = ordered.Reverse();
            ordered = ordered.Skip(highCount);
            return ordered.Average();
        }
    }
}
