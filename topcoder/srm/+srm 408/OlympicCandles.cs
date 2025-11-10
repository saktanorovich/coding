using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class OlympicCandles {
            public int numberOfNights(int[] candles) {
                  return numberOfNights(candles.Length, candles);
            }

            private int numberOfNights(int n, int[] candles) {
                  int result = 0;
                  for (int day = 1; day <= n; ++day) {
                        Array.Sort(candles);
                        bool possible = true;
                        for (int k = n - 1; k >= n - day; --k) {
                              if (candles[k] == 0) {
                                    possible = false;
                                    break;
                              }
                        }
                        if (possible) {
                              for (int k = n - 1; k >= n - day; --k) {
                                    --candles[k];
                              }
                              result = result + 1;
                        }
                        else break;
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new OlympicCandles().numberOfNights(new int[] { 2, 2, 2 }));
                  Console.WriteLine(new OlympicCandles().numberOfNights(new int[] { 2, 2, 2, 4 }));
                  Console.WriteLine(new OlympicCandles().numberOfNights(new int[] { 5, 2, 2, 1 }));
                  Console.WriteLine(new OlympicCandles().numberOfNights(new int[] { 1, 2, 3, 4, 5, 6 }));
                  Console.WriteLine(new OlympicCandles().numberOfNights(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}