using System;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class ParkAmusement {
            /* note that the landings form the directed acyclic graph so the dynamic programming
             * technique can be used in order to solve the problem... */
            public double getProbability(string[] landings, int startLanding, int k) {
                  List<int>[] graph = Array.ConvertAll(landings, delegate(string landing) {
                        List<int> result = new List<int>();
                        for (int i = 0; i < landing.Length; ++i) {
                              if (landing[i] == '1') {
                                    result.Add(i);
                              }
                        }
                        return result;
                  });
                  List<int> exits = getSpecificLandings(landings, 'E');
                  List<int> crocodiles = getSpecificLandings(landings, 'P');
                  double probabilitySum = 0.0;
                  double[] probability = new double[landings.Length];
                  for (int i = 0; i < landings.Length; ++i) {
                        if (exits.Contains(i) ||
                              crocodiles.Contains(i)) {
                                    continue;
                        }
                        probability[i] = getProbability(graph, landings.Length, exits, i, k);
                        probabilitySum += probability[i];
                  }
                  return probability[startLanding] / probabilitySum;
            }

            private double getProbability(List<int>[] graph, int numOfLandings, List<int> exits, int landing, int klandings) {
                  /* use dp bottom-up approach to calculate probability... */
                  double[,] p = new double[numOfLandings, klandings + 1];
                  p[landing, 0] = 1;
                  for (int k = 0; k < klandings; ++k) {
                        for (int curr = 0; curr < numOfLandings; ++curr) {
                              if (p[curr, k] > 0.0) {
                                    foreach (int next in graph[curr]) {
                                          p[next, k + 1] += p[curr, k] * (1.0 / graph[curr].Count);
                                    }
                              }
                        }
                  }
                  double result = 0.0;
                  foreach (int exit in exits) {
                        result += p[exit, klandings];
                  }
                  return result;
            }

            private List<int> getSpecificLandings(string[] landings, char specific) {
                  List<int> result = new List<int>();
                  for (int i = 0; i < landings.Length; ++i) {
                        if (landings[i].IndexOf(specific) >= 0) {
                              result.Add(i);
                        }
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new ParkAmusement().getProbability(new string[] {
                        "E000", "1000", "1000", "1000" }, 1, 1));
                  Console.WriteLine(new ParkAmusement().getProbability(new string[] {
                        "E000", "1000", "1001", "000P"},1 , 1));
                  Console.WriteLine(new ParkAmusement().getProbability(new string[] {
                        "01000100", "00111000", "00001010", "000E0000",
                        "0000E000", "00000P00", "000000P0", "01000000"}, 1, 2));
                  Console.WriteLine(new ParkAmusement().getProbability(new string[] {
                        "0100", "0010", "0001", "000E"}, 0, 2));
                  Console.WriteLine(new ParkAmusement().getProbability(new string[] {
                        "E00", "0E0", "010"}, 0, 1));

                  Console.WriteLine("Ready...");
                  Console.ReadLine();
            }
      }
}