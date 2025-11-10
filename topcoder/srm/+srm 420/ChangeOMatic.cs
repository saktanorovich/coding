using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ChangeOMatic {
            public long howManyRounds(int[] coins, long inputValue) {
                  Array.Sort(coins);
                  int maxCoinNominal = getMaxCoinNominal(coins);
                  int memoizationLimit = maxCoinNominal * (maxCoinNominal - 1);
                  int[] bestPay = new int[memoizationLimit + 1]; /* the best way to pay required sum with given set of coins. */
                  int[] bestPayCoin = new int[memoizationLimit + 1];
                  int[] bestXChange = new int[memoizationLimit + 1]; /* the best way to pay required sum with at least two coins from the given set. */
                  int[] bestXChangeCoin = new int[memoizationLimit + 1];
                  for (int total = 1; total <= memoizationLimit; ++total) {
                        bestPay[total] = int.MaxValue;
                        foreach (int coin in coins) {
                              if (total - coin >= 0) {
                                    if (bestPay[total] >= bestPay[total - coin] + 1) {
                                          bestPay[total] = bestPay[total - coin] + 1;
                                          bestPayCoin[total] = coin;
                                    }
                              }
                        }
                        bestXChange[total] = int.MaxValue;
                        foreach (int coin in coins) {
                              if (total - coin >= 0) {
                                    /* the machine should output a set of at least two coins. */
                                    if (bestPay[total - coin] + 1 > 1) {
                                          if (bestXChange[total] >= bestPay[total - coin] + 1) {
                                                bestXChange[total] = bestPay[total - coin] + 1;
                                                bestXChangeCoin[total] = coin;
                                          }
                                    }
                              }
                        }
                  }
                  long[] rounds = new long[memoizationLimit + 1];
                  if (inputValue <= memoizationLimit) {
                        return getNumberOfRounds(Convert.ToInt32(inputValue), bestPayCoin, bestXChangeCoin, rounds);
                  }
                  else {
                        if (inputValue % maxCoinNominal == 0) {
                              return getNumberOfRounds(maxCoinNominal, bestPayCoin, bestXChangeCoin, rounds) * (inputValue / maxCoinNominal) + 1;
                        }
                        else {
                              long times = (inputValue - memoizationLimit) / maxCoinNominal + 1;
                              return getNumberOfRounds(maxCoinNominal, bestPayCoin, bestXChangeCoin, rounds) * times +
                                          getNumberOfRounds(Convert.ToInt32(inputValue - times * maxCoinNominal), bestPayCoin, bestXChangeCoin, rounds);
                        }
                  }
            }

            private long getNumberOfRounds(int inputValue, int[] bestPayCoin, int[] bestXChangeCoin, long[] rounds) {
                  if (inputValue > 1) {
                        if (rounds[inputValue] == 0) {
                              rounds[inputValue] = 1 + getNumberOfRounds(bestXChangeCoin[inputValue], bestPayCoin, bestXChangeCoin, rounds);
                              for (int input = inputValue - bestXChangeCoin[inputValue]; input > 0; ) {
                                    rounds[inputValue] += getNumberOfRounds(bestPayCoin[input], bestPayCoin, bestXChangeCoin, rounds);
                                    input -= bestPayCoin[input];
                              }
                        }
                        return rounds[inputValue];
                  }
                  return 0;
            }

            private int getMaxCoinNominal(int[] coins) {
                  /* The c * c value should be represented as c coins with c nominal where c is the greatest coin. Now proof
                   * that any value L > c * (c - 1) is required c in the best presentation. Let L = c1 + c2 + ... cm, where
                   * 1 ≤ c1 ≤ c2 ≤ ... ≤ cm < c, m is a number of coins. Because L is greater than c * (c - 1) and cm < c
                   * we can conclude that m ≥ c. It is can be easily shown that there is exists a subset of at least two coins that can be
                   * divisible by c. So replacing this subset by c resulting to the best solution than the original one.*/
                  int maxCoinNominal = 1;
                  foreach (int coin in coins) {
                        maxCoinNominal = Math.Max(maxCoinNominal, coin);
                  }
                  return maxCoinNominal;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2 }, 1000000000000000));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1 }, 101));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 10 }, 101));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 5, 10 }, 21));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 33, 90, 91, 92, 93, 94, 95, 96, 97, 98 }, 99));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 30, 60 }, 50));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 30, 60, 90 }, 60));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 8, 9, 11, 12, 100 }, 120));

                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 50));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 99));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 73));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 134));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 137));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 997));
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 5, 10 }, 4));

                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] { 1, 2 }, 987654321098765)); // 493827160549383
                  Console.WriteLine(new ChangeOMatic().howManyRounds(new int[] {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
                        26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 64, 128, 256, 512, 1000}, 987654321098765)); // 983703703814369

                  Console.ReadLine();
            }
      }
}
