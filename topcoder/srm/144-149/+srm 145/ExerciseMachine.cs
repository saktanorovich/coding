using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ExerciseMachine {
            public int getPercentages(string time) {
                  return getPercentages(TimeSpan.Parse(time));
            }

            private int getPercentages(TimeSpan timeSpan) {
                  int result = 0;
                  for (int second = 1; second <= timeSpan.TotalSeconds; ++second) {
                        if ((100 * second) % timeSpan.TotalSeconds == 0) {
                              result = result + 1;
                        }
                  }
                  return result - 1;
            }
      }
}