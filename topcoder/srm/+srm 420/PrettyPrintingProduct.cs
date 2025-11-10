using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PrettyPrintingProduct {
            public string prettyPrint(int a, int b) {
                  long result = 1, exponent;
                  List<int> listOfNumbers = getListOfNumbers(a, b, out exponent);
                  foreach (int number in listOfNumbers) {
                        result = result * number;
                        if (getNumOfDigits(result) > longLength) {
                              return prettyPrintLong(listOfNumbers, exponent);
                        }
                  }
                  return string.Format("{0} * 10^{1}", result, exponent);
            }

            private const int longLength = 10;
            private const int longPartLength = 5;
            private const int longPartFactor = 100000;

            private string prettyPrintLong(List<int> listOfNumbers, long exponent) {
                  /* For each 10^k ≤ c < 10^(k + 1) we know that k ≤ log(c) < k + 1. So the
                   * number of digits in c is 1 + floor(log(c)). */
                  double head = 1;
                  foreach (int number in listOfNumbers) {
                        head = head * number;
                        while (head > 1.0 * longPartFactor * longPartFactor) {
                              head = head / longPartFactor;
                        }
                  }
                  while (getNumOfDigits((long)Math.Truncate(head)) < longPartLength) {
                        head = head * 10;
                  }
                  while (getNumOfDigits((long)Math.Truncate(head)) > longPartLength) {
                        head = head / 10;
                  }
                  long tail = 1;
                  foreach (int number in listOfNumbers) {
                        tail = (tail * number) % longPartFactor;
                  }
                  return string.Format("{0}...{1} * 10^{2}", Convert.ToInt32(Math.Truncate(head)), tail.ToString().PadLeft(longPartLength, '0'), exponent);
            }

            private List<int> getListOfNumbers(int a, int b, out long exponent) {
                  List<int> result = new List<int>();
                  int numOf2 = 0, numOf5 = 0;
                  for (int x = a; x <= b; ++x) {
                        int y = x;
                        numOf2 += reduceBy(ref y, 2);
                        numOf5 += reduceBy(ref y, 5);
                        if (y > 1) {
                              result.Add(y);
                        }
                  }
                  exponent = Math.Min(numOf2, numOf5);
                  for (int x = 1; x <= numOf2 - exponent; ++x) {
                        result.Add(2);
                  }
                  for (int x = 1; x <= numOf5 - exponent; ++x) {
                        result.Add(5);
                  }
                  return result;
            }

            private int reduceBy(ref int x, int p) {
                  int result = 0;
                  while (x % p == 0) {
                        result = result + 1;
                        x = x / p;
                  }
                  return result;
            }

            private int getNumOfDigits(long x) {
                  int result = 0;
                  for (; x > 0; x /= 10) {
                        result = result + 1;
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(427784, 744439));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(1, 1000000)); // "82639...12544 * 10^249998"

                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(1, 10));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(7, 7));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(211, 214));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(411, 414));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(412, 415));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(47, 4700));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(1, 19));
                  Console.WriteLine(new PrettyPrintingProduct().prettyPrint(13, 25));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}
