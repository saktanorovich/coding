using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Wireless {
            public int bestRoute(int range, string[] roamNodes, string[] statNodes) {
                  return bestRoute(range, Node.Parse(roamNodes), Node.Parse(statNodes));
            }

            private readonly int far = 10000;

            private int bestRoute(int range, Node[] roamNodes, Node[] statNodes) {
                  double[,] statDistance = new double[statNodes.Length, statNodes.Length];
                  for (int i = 0; i < statNodes.Length; ++i) {
                        for (int j = 0; j < statNodes.Length; ++j) {
                              statDistance[i, j] = int.MaxValue;
                              double d = distance(statNodes[i], statNodes[j]);
                              if (d <= range) {
                                    statDistance[i, j] = d;
                              }
                        }
                  }
                  for (int k = 0; k < statNodes.Length; ++k) {
                        for (int i = 0; i < statNodes.Length; ++i) {
                              for (int j = 0; j < statNodes.Length; ++j) {
                                    statDistance[i, j] = Math.Min(statDistance[i, j], statDistance[i, k] + statDistance[k, j]);
                              }
                        }
                  }
                  double result = double.MaxValue;
                  for (int time = 0; true; ++time) {
                        result = Math.Min(result, getDistance(roamNodes, statNodes, statDistance, range));
                        for (int i = 0; i < 2; ++i) {
                              roamNodes[i].x += roamNodes[i].dx;
                              roamNodes[i].y += roamNodes[i].dy;
                              if (-far - range <= roamNodes[i].x && roamNodes[i].x <= +far + range &&
                                  -far - range <= roamNodes[i].y && roamNodes[i].y <= +far + range) {
                              }
                              else goto exit;
                        }
                  }
                  exit: {
                        if (result < int.MaxValue) {
                              return (int)(result + 0.5);
                        }
                        return -1;
                  }
            }

            private double getDistance(Node[] roamNodes, Node[] statNodes, double[,] statDistance, int range) {
                  double result = double.MaxValue;
                  double[,] d = new double[2, statNodes.Length];
                  for (int r = 0; r < 2; ++r) {
                        for (int i = 0; i < statNodes.Length; ++i) {
                              d[r, i] = int.MaxValue;
                              double dist = distance(roamNodes[r], statNodes[i]);
                              if (dist <= range) {
                                    d[r, i] = dist;
                              }
                        }
                  }
                  for (int i0 = 0; i0 < statNodes.Length; ++i0) {
                        for (int i1 = 0; i1 < statNodes.Length; ++i1) {
                              result = Math.Min(result, d[0, i0] + d[1, i1] + statDistance[i0, i1]);
                        }
                  }
                  return result;
            }

            private double distance(Node a, Node b) {
                  return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
            }

            private class Node {
                  public int x, dx;
                  public int y, dy;

                  public static Node[] Parse(string[] nodes) {
                        return Array.ConvertAll(nodes, delegate(string item) {
                              string[] s = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                              Node node = new Node();
                              node.x = int.Parse(s[s.Length - 2]);
                              node.y = int.Parse(s[s.Length - 1]);
                              if (s.Length == 3) {
                                    switch (s[0]) {
                                          case "UP":    node.dy = +1; break;
                                          case "DOWN":  node.dy = -1; break;
                                          case "LEFT":  node.dx = -1; break;
                                          case "RIGHT": node.dx = +1; break;
                                          default:
                                                throw new NotSupportedException();
                                    }
                              }
                              return node;
                        });
                  }
            }

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new Wireless().bestRoute(1,
                        new string[] { "DOWN 100 200", "DOWN 100 200" },
                        new string[] { "1000 1000" }));
                  Console.WriteLine(new Wireless().bestRoute(30000,
                        new string[] { "DOWN 10000 10000", "RIGHT -10000 -10000" },
                        new string[] { "10000 -10000", "10000 -10000" }));
                  Console.WriteLine(new Wireless().bestRoute(3000,
                        new string[] { "DOWN 0 10000", "LEFT 10000 0" },
                        new string[] { "14 100", "25 -10", "98 204", "99 1000" }));
                  Console.WriteLine(new Wireless().bestRoute(30000,
                        new string[] { "DOWN 0 0", "DOWN 0 0" },
                        new string[] { "10000 10000", "9000 10000", "8000 10000", "7000 10000" }));
                  Console.WriteLine(new Wireless().bestRoute(20,
                        new string[] {"DOWN -20 0","DOWN 80 1"},
                        new string[] {"0 0","20 0","40 0","60 1"}));
                  Console.WriteLine(new Wireless().bestRoute(2000,
                        new string[] {"LEFT 10000 10000", "DOWN 10000 10000"},
                        new string[] {"-10000 8000", "-10000 6000", "-10000 4000", "-10000 2000", "-10000 0", "-10000 -2000", "-10000 -4000", "-10000 -6000", "-10000 -8000", "-10000 -10000", "8000 -10000", "6000 -10000", "4000 -10000", "2000 -10000", "0 -10000", "-2000 -10000", "-4000 -10000", "-6000 -10000", "-8000 -10000", "-10000 -10000"}));
                  Console.WriteLine(new Wireless().bestRoute(3,
                  new string[] {"RIGHT -10000 0", "LEFT 10000 5"},
                  new string[] {"11 4", "2 -1", "3 4", "6 -1", "-13 -4", "-10 0", "9 -3", "-3 -3", "3 -1", "-2 1", "5 4", "5 0", "-6 -1", "7 3", "9 0", "9 0", "13 -3", "11 4", "-3 2", "-6 2", "-11 4", "-1 2", "-8 1", "6 0", "-4 -3", "0 1", "-2 0", "-4 4", "0 0", "10 0"}));
                  Console.WriteLine(new Wireless().bestRoute(30000,
                  new string[] {"DOWN 0 0", "RIGHT -10000 0"},
                  new string[] {"10000 10000", "9000 10000", "8000 10000", "7000 10000", "10000 1100", "9000 1100", "8000 1100", "7000 1100", "10000 1200", "9000 1200", "8000 1200", "7000 1200", "10000 1300", "9000 1300", "8000 1300", "7000 1300", "10000 1400", "9000 1400", "8000 1400", "7000 1400", "10000 1500", "9000 1500", "8000 1000", "7000 1500", "10000 1600", "9000 1600", "8000 1600", "7000 1600", "10000 1700", "9000 1700"}));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}