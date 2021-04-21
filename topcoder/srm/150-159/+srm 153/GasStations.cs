using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class GasStations {
            public double tripCost(int[] dist, int[] price, int milesPerGallon, int tankSize, int tripLength) {
                  /* Consider a mile from p to p + 1. It is clear that in order to minimize the total trip cost
                   * it is enough to minimize a cost from p to p + 1 for each p from 0 to L-1... */
                  int[] minCost = new int[tripLength];
                  for (int mile = 0; mile < tripLength; ++mile) {
                        minCost[mile] = int.MaxValue;
                        if (mile < milesPerGallon * tankSize) {
                              minCost[mile] = 0;
                        }
                  }
                  for (int station = 0; station < dist.Length; ++station) {
                        for (int miles = 0; miles < milesPerGallon * tankSize; ++miles) {
                              if (dist[station] + miles < tripLength) {
                                    minCost[dist[station] + miles] = Math.Min(minCost[dist[station] + miles], price[station]);
                              }
                        }
                  }
                  int total = 0;
                  for (int mile = 0; mile < tripLength; ++mile) {
                        total += minCost[mile];
                  }
                  return 1.0 * total / milesPerGallon;
            }
      }
}