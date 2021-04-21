using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TopPilot {
            public int addFlights(string[] destinations) {
                  bool[,] graph = new bool[51, 51];
                  for (int curr = 0; curr < destinations.Length; ++curr) {
                        int[] adj = Array.ConvertAll(destinations[curr].Split(new char[] { ',' }),
                              delegate(string s) {
                                    return int.Parse(s);
                              });
                        foreach (int next in adj) {
                              graph[curr, next] = true;
                        }
                  }
                  for (int k = 0; k < 51; ++k) {
                        for (int i = 0; i < 51; ++i) {
                              for (int j = 0; j < 51; ++j) {
                                    graph[i, j] = graph[i, j] || graph[i, k] && graph[k, j];
                              }
                        }
                  }
                  int result = 0;
                  for (int i = 0; i < 51; ++i) {
                        for (int j = 0; j < 51; ++j) {
                              if (i != j) {
                                    if (graph[i, j]) {
                                          result = result + 1;
                                    }
                              }
                        }
                  }
                  return result;
            }
      }
}