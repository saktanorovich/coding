using System;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class RecursiveFigures {
            public double getArea(int sideLength, int k) {
                  return getArea(1.0 * sideLength, k);
            }

            private double getArea(double sideLength, int k) {
                  if (k > 1) {
                        return getArea(sideLength * 0.5 * Math.Sqrt(2), k - 1) + 3.1415926535897932384626433832795 * sideLength * 0.5 * sideLength * 0.5 - sideLength * 0.5 * sideLength * 0.5 * 2;
                  }
                  return 3.1415926535897932384626433832795 * sideLength * 0.5 * sideLength * 0.5;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new RecursiveFigures().getArea(10, 1));
                  Console.WriteLine(new RecursiveFigures().getArea(10, 2));
                  Console.WriteLine(new RecursiveFigures().getArea(10, 3));

                  Console.WriteLine("Ready...");
                  Console.ReadLine();
            }
      }
}