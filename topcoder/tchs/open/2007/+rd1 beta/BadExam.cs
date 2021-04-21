using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BadExam {
        public double newAverage(int[] marks) {
            return marks.Select(x => 100.0 * x / marks.Max()).Average();
        }
    }
}
