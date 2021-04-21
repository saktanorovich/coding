using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Networking {
            public double getProbability(int numOfNodes, string[] connections) {
                  return getProbability(numOfNodes, Array.ConvertAll(connections,
                        delegate(string conn) {
                              string[] s = conn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                              return new Connection(int.Parse(s[0]), int.Parse(s[1]), double.Parse(s[2]));
                  }));
            }

            private double getProbability(int numOfNodes, Connection[] connections) {
                  double result = 0.0;
                  bool[,] graph = new bool[numOfNodes, numOfNodes];
                  for (int set = 0; set < 1 << connections.Length; ++set) {
                        double prob = 1.0;
                        for (int conn = 0; conn < connections.Length; ++conn) {
                              if ((set & (1 << conn)) != 0) {
                                    graph[connections[conn].Src, connections[conn].Dst] = true;
                                    graph[connections[conn].Dst, connections[conn].Src] = true;
                                    prob *= connections[conn].Prob;
                              }
                              else {
                                    graph[connections[conn].Src, connections[conn].Dst] = false;
                                    graph[connections[conn].Dst, connections[conn].Src] = false;
                                    prob *= (1 - connections[conn].Prob);
                              }
                        }
                        result += prob * dfs(graph, numOfNodes, 0, numOfNodes - 1, new bool[numOfNodes]);
                  }
                  return result;
            }

            private int dfs(bool[,] graph, int numOfNodes, int curr, int goal, bool[] visited) {
                  visited[curr] = true;
                  if (curr == goal) {
                        return 1;
                  }
                  for (int next = 0; next < numOfNodes; ++next) {
                        if (graph[curr, next] && !visited[next]) {
                              if (dfs(graph, numOfNodes, next, goal, visited) > 0) {
                                    return 1;
                              }
                        }
                  }
                  return 0;
            }

            private struct Connection {
                  public int Src;
                  public int Dst;
                  public double Prob;

                  public Connection(int src, int dst, double prob) {
                        Src = src;
                        Dst = dst;
                        Prob = prob;
                  }
            }

            public static void Main(string[] args) {
                  // 0.8
                  Console.WriteLine(new Networking().getProbability(2, new string[] { "0 1 0.80" }));
                  // 0.755
                  Console.WriteLine(new Networking().getProbability(3, new string[] { "0 2 0.50", "0 2 0.50", "0 1 0.10", "1 2 0.20" }));
                  // 0.0
                  Console.WriteLine(new Networking().getProbability(4, new string[] { "0 2 0.06", "3 1 1.00", "1 3 0.50", "1 2 0.00" }));
                  // 0.8244
                  Console.WriteLine(new Networking().getProbability(4, new string[] { "0 1 0.40", "1 3 0.99", "0 2 0.89", "2 3 0.60", "1 2 0.50" }));
                  // 0.432863552
                  Console.WriteLine(new Networking().getProbability(7, new string[] { "0 1 0.80", "1 4 0.20", "4 6 0.50", "6 5 0.40", "5 3 0.60", "3 0 0.70", "0 2 0.20", "2 6 0.30", "1 2 0.60", "2 3 0.70" }));
                  // 1.0
                  Console.WriteLine(new Networking().getProbability(2, new string[] { "0 1 0.20", "1 0 1.00" }));
                  // 0.6
                  Console.WriteLine(new Networking().getProbability(4, new string[] { "0 3 0.60", "0 1 0.90", "0 0 0.90", "0 2 0.80" }));
                  // 0.6784000000000001
                  Console.WriteLine(new Networking().getProbability(4, new string[] { "0 1 0.90", "1 2 0.90", "0 2 0.20", "2 3 0.80" }));
                  // 0.3
                  Console.WriteLine(new Networking().getProbability(4, new string[] { "0 1 0.60", "1 3 0.50", "0 2 0.00", "2 3 1.00" }));
                  // 0.7193563200000002
                  Console.WriteLine(new Networking().getProbability(5, new string[] { "0 1 0.80", "1 2 0.99", "2 3 0.98", "1 3 0.97", "3 4 0.90" }));
                  // 0.11487645511550248
                  Console.WriteLine(new Networking().getProbability(10, new string[] { "0 1 0.10", "0 2 0.10", "0 3 0.10", "0 4 0.10", "0 5 0.10", "0 6 0.10", "0 7 0.10", "0 8 0.10", "0 9 0.10", "1 2 0.10", "1 3 0.10", "1 4 0.10", "1 5 0.10", "1 6 0.10", "1 7 0.10", "1 8 0.10", "1 9 0.10", "5 6 0.10", "7 8 0.10", "3 4 0.10" }));
                  // 0.0
                  Console.WriteLine(new Networking().getProbability(10, new string[] { }));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}