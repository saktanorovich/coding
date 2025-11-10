using System;

namespace topcoder.algorithm {
      class FormatAmt {
            public string amount(int dollars, int cents) {
                  return amount(dollars + cents / 100.0);
            }

            private string amount(double dollars) {
                  return string.Format("${0:#,0.00}", dollars);
            }
      }
}