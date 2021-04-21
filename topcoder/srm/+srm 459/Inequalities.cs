using System;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Inequalities {
            public int maximumSubset(string[] inequalities) {
                  int result = 0;
                  for (double x = -2000; x <= +2000; x += 0.5) {
                        result = Math.Max(result, match(x, inequalities));
                  }
                  return result;
            }

            private int match(double x, string[] inequalities) {
                  int result = 0;
                  foreach (string entry in inequalities) {
                        string[] splitted = entry.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        result += Convert.ToInt32(satisfy(x, splitted[1], double.Parse(splitted[2])));
                  }
                  return result;
            }

            private bool satisfy(double x, string op, double value) {
                  if (op == "<" ) return x <  value;
                  if (op == "<=") return x <= value;
                  if (op == "=" ) return x == value;
                  if (op == ">=") return x >= value;
                  if (op == ">" ) return x >  value;
                  return false;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new Inequalities().maximumSubset(new string[] { "X <= 12", "X = 13", "X > 9", "X < 10", "X >= 14" }));

                  Console.WriteLine("Ready...");
                  Console.ReadLine();
            }
      }
}