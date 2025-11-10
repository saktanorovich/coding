using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Collision {
            public double probability(int[] assignments, int ids) {
                  int total = 0;
                  foreach (int assign in assignments) {
                        total += assign;
                  }
                  return calc(assignments, total, ids, 1) - calc(assignments, total, ids, 0);
            }

            /* returns probability that there will not be a collision... */
            private double calc(int[] assignments, int total, int ids, int memory) {
                  if (total <= ids) {
                        double prob = 1, assigned = 0;
                        foreach (int assign in assignments) {
                              for (int request = 0; request < assign; ++request, ++assigned) {
                                    prob *= (ids - assigned) / (ids - memory * request);
                              }
                        }
                        return prob;
                  }
                  return 0;
            }
      }
}