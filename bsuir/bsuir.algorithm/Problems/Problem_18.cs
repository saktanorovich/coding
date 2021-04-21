using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_18 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            var year = reader.NextInt();
            if (isLeapYear(year)) {
                writer.WriteLine("YES");
            } else {
                writer.WriteLine("NO");
            }
            return true;
        }

        private bool isLeapYear(int year) {
            if (year % 4 != 0) {
                return false;
            }
            if (year % 100 == 0) {
                return year % 400 == 0;
            }
            return true;
        }
    }
}
