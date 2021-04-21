using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class FryingHamburgers {
            public int howLong(int panSize, int hamburgers) {
                  if (hamburgers > 0) {
                        if (panSize >= hamburgers) {
                              return 10;
                        }
                        else {
                              int time = 2 * hamburgers / panSize * 5;
                              if (2 * hamburgers % panSize != 0) {
                                    time = time + 5;
                              }
                              return time;
                        }
                  }
                  return 0;
            }
      }
}