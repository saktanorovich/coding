using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class StampPads {
            public int bestCombo(string[] pads, string[] wishlist) {
                  return bestCombo(Array.ConvertAll(wishlist, delegate(string s) {
                        return registry[s];
                  }), Array.ConvertAll(pads, delegate(string s) {
                        return Array.ConvertAll(s.Split(' '), delegate(string color) {
                              return registry[color];
                        });
                  }));
            }

            private int bestCombo(int[] wishlist, int[][] pads) {
                  return bestCombo(wishlist, Array.ConvertAll(pads, delegate(int[] pad) {
                        int result = 0;
                        for (int i = 0; i < pad.Length; ++i) {
                              if (pad[i] < wishlist.Length) {
                                    result |= 1 << pad[i];
                              }
                        }
                        return result;
                  }));
            }

            private int bestCombo(int[] wishlist, int[] pads) {
                  int wishlistSet = 0;
                  foreach (int color in wishlist) {
                        wishlistSet |= 1 << color;
                  }
                  int result = int.MaxValue;
                  for (int set = 1; set < 1 << pads.Length; ++set) {
                        int cardinality = 0, gathered = 0;
                        for (int i = 0; i < pads.Length; ++i) {
                              if ((set & (1 << i)) != 0) {
                                    cardinality = cardinality + 1;
                                    gathered |= pads[i];
                              }
                        }
                        if (gathered == wishlistSet) {
                              result = Math.Min(result, cardinality);
                        }
                  }
                  if (result < int.MaxValue) {
                        return result;
                  }
                  return -1;
            }

            private Registry registry = new Registry();

            private class Registry {
                  private IDictionary<string, int> registry = new Dictionary<string, int>();
                  private int count = 0;

                  public int this[string s] {
                        get {
                              int result = 0;
                              if (!registry.TryGetValue(s, out result)) {
                                    registry.Add(s, count);
                                    count = count + 1;
                              }
                              return registry[s];
                        }
                  }
            }
      }
}