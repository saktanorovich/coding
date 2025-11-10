using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Pricing {
            public int maxSales(int[] price) {
                  return maxSales(price, price.Length, 4);
            }

            private int maxSales(int[] price, int numOfCustomers, int maxNumOfGroups) {
                  Array.Sort(price);
                  int maximumSalesRevenue = 0;
                  int[,] f = new int[numOfCustomers + 1, maxNumOfGroups + 1];
                  for (int numOfGroups = 1; numOfGroups <= maxNumOfGroups; ++numOfGroups) {
                        for (int customer = 1; customer <= numOfCustomers; ++customer) {
                              for (int last = customer; last >= numOfGroups; --last) {
                                    f[customer, numOfGroups] = Math.Max(f[customer, numOfGroups], maxSalesIn(price, last, customer) + f[last - 1, numOfGroups - 1]);
                                    maximumSalesRevenue = Math.Max(maximumSalesRevenue, f[customer, numOfGroups]);
                              }
                        }
                  }
                  return maximumSalesRevenue;
            }

            private int maxSalesIn(int[] price, int begOfGroup, int endOfGroup) {
                  int result = 0;
                  for (int last = begOfGroup; last <= endOfGroup; ++last) {
                        result = Math.Max(result, price[last - 1] * (endOfGroup - last + 1));
                  }
                  return result;
            }
      }
}