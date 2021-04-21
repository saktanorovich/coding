using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class SumSort {
            public int valueAt(int rangeLo, int rangeHi, int position) {
                  return valueAt(rangeLo, rangeHi, position + 1, new Calculator(9));
            }

            private int valueAt(int rangeLo, int rangehi, int position, Calculator calculator) {
                  int target = 0;
                  for (int sum = 0; sum <= 81; ++sum) {
                        int total = calculator.count(rangeLo, rangehi, sum);
                        if (total < position) {
                              position -= total;
                        }
                        else {
                              target = sum;
                              break;
                        }
                  }
                  int lo = rangeLo, hi = rangehi;
                  while (lo < hi) {
                        int value = (lo + hi) >> 1;
                        int total = calculator.count(rangeLo, value, target);
                        if (total < position) {
                              lo = value + 1;
                        }
                        else {
                              hi = value;
                        }
                  }
                  return lo;
            }

            private class Calculator {
                  private int numOfDigits;
                  private int[,] memo;

                  public Calculator(int numOfDigits) {
                        this.numOfDigits = numOfDigits;
                        memo = new int[numOfDigits + 1, 9 * numOfDigits + 1];
                        for (int sumOfDigits = 0; sumOfDigits <= 9 * numOfDigits; ++sumOfDigits) {
                              memo[0, sumOfDigits] = 0;
                              for (int digits = 1; digits <= numOfDigits; ++digits) {
                                    memo[digits, sumOfDigits] = -1;
                              }
                        }
                        memo[0, 0] = 1;
                  }

                  public int count(int lo, int hi, int sum) {
                        return count(hi, sum) - count(lo - 1, sum);
                  }

                  private int count(int x, int sum) {
                        if (x >= 0) {
                              int result = 0;
                              int[] digits = digitize(x);
                              for (int i = 0; i <= numOfDigits; ++i) {
                                    if (digits[i] != 0) {
                                          for (int j = i; j <= numOfDigits; ++j) {
                                                for (int d = 0; d < digits[j]; ++d) {
                                                      result += get(numOfDigits - j, sum - d);
                                                }
                                                sum -= digits[j];
                                          }
                                          break;
                                    }
                              }
                              result += get(0, sum);
                              return result;
                        }
                        return 0;
                  }

                  private int[] digitize(int x) {
                        int[] digits = new int[numOfDigits + 1];
                        for (int i = 0; i <= numOfDigits; ++i, x /= 10) {
                              digits[numOfDigits - i] = x % 10;
                        }
                        return digits;
                  }

                  private int get(int digits, int sum) {
                        if (sum >= 0) {
                              if (digits >= 0) {
                                    if (memo[digits, sum] == -1) {
                                          memo[digits, sum] = 0;
                                          for (int d = 0; d < 10; ++d) {
                                                memo[digits, sum] += get(digits - 1, sum - d);
                                          }
                                    }
                                    return memo[digits, sum];
                              }
                        }
                        return 0;
                  }
            }
      }
}