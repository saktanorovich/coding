using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class BaseMystery {
            public int[] getBase(string equation) {
                  string[] abc = equation.Split('+', '=');
                  List<int> result = new List<int>();
                  for (int @base = 2; @base <= 20; ++@base) {
                        if (valid(abc[0], abc[1], abc[2], @base)) {
                              result.Add(@base);
                        }
                  }
                  return result.ToArray();
            }

            private bool valid(string a, string b, string c, int @base) {
                  try {
                        int A = toDecimal(a, @base);
                        int B = toDecimal(b, @base);
                        int C = toDecimal(c, @base);
                        return (A + B == C);
                  }
                  catch (Exception) {
                        return false;
                  }
            }

            private static readonly string alphabet = "0123456789ABCDEFGHIJ";

            private int toDecimal(string x, int @base) {
                  int result = 0, b = 1;
                  for (int ix = x.Length - 1; ix >= 0; --ix) {
                        int digit = alphabet.IndexOf(x[ix]);
                        if (0 <= digit && digit < @base) {
                              result += b * digit;
                              b *= @base;
                        }
                        else throw new Exception();
                  }
                  return result;
            }
      }
}