using System;
using System.Collections;
using System.Collections.Generic;

public class EllysDeathStars {
      private static class MathUtils {
            public static readonly double eps = 1e-9;
            public static readonly double pi = 3.1415926535897932384626433832795;

            public static int sign(double x) {
                  if (x + eps < 0) {
                        return -1;
                  }
                  if (x - eps > 0) {
                        return +1;
                  }
                  return 0;
            }

            public static double abs(double x) {
                  if (sign(x) < 0) {
                        return -x;
                  }
                  return +x;
            }

            public static double max(double a, double b) {
                  if (sign(a - b) > 0) {
                        return a;
                  }
                  return b;
            }

            public static double min(double a, double b) {
                  if (sign(a - b) < 0) {
                        return a;
                  }
                  return b;
            }
      }

      private class Segment {
            public readonly double beg;
            public readonly double end;

            public Segment(double beg, double end) {
                  this.beg = beg;
                  this.end = end;
            }
      }

      private class Event {
            public readonly int shipId;
            public readonly double time;
            public readonly int type;

            public Event(int shipId, double time, int type) {
                  this.shipId = shipId;
                  this.time = time;
                  this.type = type;
            }
      }

      private class Star {
            public readonly int x;
            public readonly int y;

            private Star(int x, int y) {
                  this.x = x;
                  this.y = y;
            }

            public static Star Parse(string s) {
                  string[] d = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                  return new Star(int.Parse(d[0]), int.Parse(d[1]));
            }
      }

      private class Ship {
            public readonly int xbeg;
            public readonly int ybeg;
            public readonly int xend;
            public readonly int yend;
            public readonly int speed;
            public readonly int range;
            public readonly int energy;

            private Ship(int xbeg, int ybeg, int xend, int yend, int speed, int range, int energy) {
                  this.xbeg = xbeg;
                  this.ybeg = ybeg;
                  this.xend = xend;
                  this.yend = yend;
                  this.speed = speed;
                  this.range = range;
                  this.energy = energy;
            }

            public Segment coverAt(Star star) {
                  double phi = 2 * MathUtils.pi + Math.Atan2(yend - ybeg, xend - xbeg);
                  double sx = (star.x - xbeg) * Math.Cos(-phi) - (star.y - ybeg) * Math.Sin(-phi);
                  double sy = (star.x - xbeg) * Math.Sin(-phi) + (star.y - ybeg) * Math.Cos(-phi);
                  if (MathUtils.sign(MathUtils.abs(sy) - range) < 0) {
                        double dd = Math.Sqrt(range * range - sy * sy);
                        double lo = MathUtils.max(sx - dd, 0);
                        double hi = MathUtils.min(sx + dd, Math.Sqrt(1.0 * (xend - xbeg) * (xend - xbeg) + 1.0 * (yend - ybeg) * (yend - ybeg)));
                        if (MathUtils.sign(lo - hi) < 0) {
                              return new Segment(lo / speed, hi / speed);
                        }
                  }
                  return null;
            }

            public static Ship Parse(string s) {
                  string[] d = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                  return new Ship(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]), int.Parse(d[3]), int.Parse(d[4]), int.Parse(d[5]), int.Parse(d[6]));
            }
      }

      public double getMax(string[] stars, string[] ships) {
            Ship[] _stars = Array.ConvertAll<string, Ship>(ships, Ship.Parse);
            List<Ship> active = new List<Ship>();
            foreach (Ship ship in _stars) {
                  if (ship.xbeg != ship.xend || ship.ybeg != ship.yend) {
                        active.Add(ship);
                  }
            }
            return getMax(Array.ConvertAll<string, Star>(stars, Star.Parse), active.ToArray());
      }

      private double getMax(Star[] stars, Ship[] ships) {
            int starsCount = stars.Length;
            int shipsCount = ships.Length;
            int src = 0, dst = starsCount + shipsCount + 2 * starsCount * shipsCount;
            double[,] capacity = new double[dst - src + 1, dst - src + 1];
            for (int iship = 0; iship < shipsCount; ++iship) {
                  capacity[src, iship + 1] = ships[iship].energy;
                  capacity[iship + 1, src] = 0;
            }
            for (int istar = 0; istar < starsCount; ++istar) {
                  capacity[shipsCount + istar + 1, dst] = double.MaxValue / 2;
                  capacity[dst, shipsCount + istar + 1] = 0;
            }
            int curr = shipsCount + starsCount;
            for (int istar = 0; istar < starsCount; ++istar) {
                  Star star = stars[istar];
                  List<Event> events = new List<Event>();
                  for (int iship = 0; iship < shipsCount; ++iship) {
                        Segment segment = ships[iship].coverAt(star);
                        if (segment != null) {
                              events.Add(new Event(iship, segment.beg, +1));
                              events.Add(new Event(iship, segment.end, -1));
                        }
                  }
                  events.Sort(delegate(Event e1, Event e2) {
                        if (MathUtils.sign(e1.time - e2.time) != 0) {
                              return MathUtils.sign(e1.time - e2.time);
                        }
                        return Math.Sign(e1.type - e2.type);
                  });
                  int set = 0;
                  for (int i = 0; i < events.Count; ++i) {
                        set += events[i].type * (1 << events[i].shipId);
                        if (set != 0) {
                              double delta = events[i + 1].time - events[i].time;
                              if (MathUtils.sign(delta) > 0) {
                                    ++curr;
                                    for (int jship = 0; jship < shipsCount; ++jship) {
                                          if ((set & (1 << jship)) != 0) {
                                                capacity[jship + 1, curr] = double.MaxValue / 2;
                                                capacity[curr, jship + 1] = 0;
                                                capacity[curr, shipsCount + istar + 1] = delta;
                                                capacity[shipsCount + istar + 1, curr] = 0;
                                          }
                                    }
                              }
                        }
                  }
            }
            return maxFlow(src, dst, dst - src + 1, capacity);
      }

      private double maxFlow(int src, int dst, int verticesCount, double[,] capacity) {
            double[,] flow = new double[verticesCount, verticesCount];
            List<int>[] adj = new List<int>[verticesCount];
            for (int i = 0; i < verticesCount; ++i) {
                  adj[i] = new List<int>();
            }
            for (int i = 0; i < verticesCount; ++i) {
                  for (int j = 0; j < verticesCount; ++j) {
                        if (MathUtils.sign(capacity[i, j]) > 0) {
                              adj[i].Add(j);
                              adj[j].Add(i);
                        }
                  }
            }
            double result = 0.0; 
            while (true) {
                  int[] prev = new int[verticesCount];
                  for (int i = 0; i < verticesCount; ++i) {
                        prev[i] = -1;
                  }
                  Queue<int> queue = new Queue<int>(); 
                  for (queue.Enqueue(src); queue.Count > 0; ) {
                        int curr = queue.Dequeue();
                        for (int i = 0; i < adj[curr].Count; ++i) {
                              int next = adj[curr][i];
                              if (prev[next] == -1) {
                                    if (MathUtils.sign(capacity[curr, next] - flow[curr, next]) > 0) {
                                          prev[next] = curr;
                                          queue.Enqueue(next);
                                    }
                              }
                        }
                  }
                  if (prev[dst] != -1) {
                        double by = double.MaxValue / 2;
                        for (int curr = dst; curr != src;) {
                              by = MathUtils.min(by, capacity[prev[curr], curr] - flow[prev[curr], curr]);
                              curr = prev[curr];
                        }
                        for (int curr = dst; curr != src;) {
                              flow[prev[curr], curr] += by;
                              flow[curr, prev[curr]] -= by;
                              curr = prev[curr];
                        }
                        result += by;
                  }
                  else {
                        break;
                  }
            }
            return result;
      }

      // BEGIN CUT HERE
      public void run_test(int Case) {
            if ((Case == -1) || (Case == 0)) test_case_0();
            if ((Case == -1) || (Case == 1)) test_case_1();
            if ((Case == -1) || (Case == 2)) test_case_2();
            if ((Case == -1) || (Case == 3)) test_case_3();
            if ((Case == -1) || (Case == 4)) test_case_4();
      }
      private void verify_case(int Case, double Expected, double Received) {
            Console.Write("Test Case #" + Case + "...");
            if (Math.Abs(Expected - Received) <= 1e-9)
                  Console.WriteLine("PASSED");
            else {
                  Console.WriteLine("FAILED");
                  Console.WriteLine("\tExpected: \"" + Expected + '\"');
                  Console.WriteLine("\tReceived: \"" + Received + '\"');
            }
      }
      private void test_case_0() { string[] Arg0 = new string[] { "2 2" }; string[] Arg1 = new string[] { "1 1 5 3 2 1 2" }; double Arg2 = 0.894427190999916; verify_case(0, Arg2, getMax(Arg0, Arg1)); }
      private void test_case_1() { string[] Arg0 = new string[] { "12 10", "7 5" }; string[] Arg1 = new string[] { "10 10 12 10 1 1 3", "6 1 8 10 1 2 3", "3 6 8 2 5 3 1", "42 42 42 42 6 6 6" }; double Arg2 = 4.983770744659944; verify_case(1, Arg2, getMax(Arg0, Arg1)); }
      private void test_case_2() {
            string[] Arg0 = new string[] { "5 77", "60 50", "10 46", "22 97", "87 69" }; string[] Arg1 = new string[]{"42 17 66 11 5 7 13", "10 10 20 20 3 3 3", "13 15 18 9 4 1 2",
 "99 71 63 81 19 4 60", "27 34 56 43 11 3 12"}; double Arg2 = 0.0; verify_case(2, Arg2, getMax(Arg0, Arg1));
      }
      private void test_case_3() {
            string[] Arg0 = new string[]{"141 393", "834 847", "568 43", "18 228", "515 794",
 "167 283", "849 333", "719 738", "434 261", "613 800",
 "127 340", "466 938", "598 601"}; string[] Arg1 = new string[]{"410 951 472 100 337 226 210", "713 352 677 908 731 687 300",
 "191 41 337 92 446 716 213", "598 889 446 907 148 650 203",
 "168 556 470 924 344 369 198", "300 182 350 936 737 533 45",
 "410 871 488 703 746 631 80", "270 777 636 539 172 103 56",
 "466 906 522 98 693 77 309", "768 698 846 110 14 643 14",
 "755 724 664 465 263 759 120"}; double Arg2 = 31.965770956316362; verify_case(3, Arg2, getMax(Arg0, Arg1));
      }
      private void test_case_4() {
            string[] Arg0 = new string[]{"38 42", "45 45", "41 38", "46 46", "43 39", "38 44", "41 42", "43 42", "44 43", "40 41", "46 41", "39 40", "42 43", "42 46", "45 39", "38 41", "42 46", "46 41", "43 42", "40 44"};
            string[] Arg1 = new string[] {
                  "18 38 66 46 5 43 12", "3 30 81 54 18 39 12", "72 3 12 81 33 49 4", "96 44 1 40 1 46 1",
                  "74 83 10 1 39 40 4", "75 3 9 81 25 1 7", "22 26 62 58 43 12 7", "2 65 82 19 14 16 5",
                  "67 54 17 30 10 9 13", "96 53 1 31 20 50 2", "50 70 34 14 1 40 7",
                  "16 30 68 54 36 16 1", "43 82 41 2 26 38 3", "18 3 66 81 50 14 9", "50 50 34 34 38 49 10",
                  "65 43 19 41 45 18 2", "8 1 76 83 39 22 3", "57 41 27 43 9 27 12", "62 46 22 38 43 3 1", "47 11 37 73 48 47 6" };
            double Arg2 = 104.68164748120223; verify_case(4, Arg2, getMax(Arg0, Arg1));
      }
      // END CUT HERE

      // BEGIN CUT HERE
      [STAThread]
      public static void Main(string[] args) {
            EllysDeathStars item = new EllysDeathStars();
            item.run_test(-1);
            Console.ReadLine();
      }
      // END CUT HERE
}
