using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class VendingMachine {
            public int motorUse(string[] prices, string[] purchases) {
                  return motorUse(Array.ConvertAll(prices,
                        delegate(string s) {
                              return Array.ConvertAll(s.Split(new char[] { ' ' }),
                                    delegate(string price) {
                                          return int.Parse(price);
                              });
                  }),
                  Array.ConvertAll(purchases,
                        delegate(string s) {
                              return Purchase.Parse(s);
                  }));
            }

            private int motorUse(int[][] prices, Purchase[] purchases) {
                  int numOfRows = prices.Length;
                  int numOfCols = prices[0].Length;
                  int curCol = getMostExpensiveCol(prices, numOfRows, numOfCols);
                  int result = rotate(0, curCol, numOfCols);
                  for (int i = 0; i < purchases.Length; ++i) {
                        if (prices[purchases[i].row][purchases[i].col] > 0) {
                              if (i > 0) {
                                    /* a purchase was made before so we should check on 5-minutes delay... */
                                    if (purchases[i].tim - purchases[i - 1].tim >= 5) {
                                          int nextCol = getMostExpensiveCol(prices, numOfRows, numOfCols);
                                          result += rotate(curCol, nextCol, numOfCols);
                                          curCol = nextCol;
                                    }
                              }
                              result += rotate(purchases[i].col, curCol, numOfCols);
                              prices[purchases[i].row][purchases[i].col] = 0;
                              curCol = purchases[i].col;
                        }
                        else  return -1;
                  }
                  return result + rotate(curCol, getMostExpensiveCol(prices, numOfRows, numOfCols), numOfCols);
            }

            private int rotate(int beg, int end, int numOfCols) {
                  return Math.Min((beg - end + numOfCols) % numOfCols, (end - beg + numOfCols) % numOfCols);
            }

            private int getMostExpensiveCol(int[][] prices, int numOfRows, int numOfCols) {
                  int result = numOfCols, best = -1;
                  for (int col = 0; col < numOfCols; ++col) {
                        int price = 0;
                        for (int row = 0; row < numOfRows; ++row) {
                              price += prices[row][col];
                        }
                        if (best < price) {
                              best = price;
                              result = col;
                        }
                  }
                  return result;
            }

            private struct Purchase {
                  public int row;
                  public int col;
                  public int tim;

                  public static Purchase Parse(string s) {
                        string[] items = s.Split(new char[] { ',', ':' });
                        Purchase result = new Purchase();
                        result.row = int.Parse(items[0]);
                        result.col = int.Parse(items[1]);
                        result.tim = int.Parse(items[2]);
                        return result;
                  }
            }
      }
}