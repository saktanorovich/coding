using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class HigherMaze {
            public int navigate(int a, int p, int[] dim, int[] start, int[] finish, string[] wormholes) {
                  Wormhole[] wormholes_ = Array.ConvertAll(wormholes, delegate(string s) {
                        int[] items = Array.ConvertAll(s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                              delegate(string t) {
                                    return int.Parse(t);
                        });
                        int[] src = new int[dim.Length];
                        int[] dst = new int[dim.Length];
                        for (int i = 0; i < dim.Length; ++i) {
                              src[i] = items[i];
                              dst[i] = items[dim.Length + 1 + i];
                        }
                        return new Wormhole(new State(normalize(src, 0)), new State(normalize(dst, 0)), items[dim.Length]);
                  });
                  Hashtable asteroids = new Hashtable();
                  dim = normalize(dim, 1);
                  long current = 1;
                  for (int i0 = 0; i0 < dim[0]; ++i0) {
                        for (int i1 = 0; i1 < dim[1]; ++i1) {
                              for (int i2 = 0; i2 < dim[2]; ++i2) {
                                    for (int i3 = 0; i3 < dim[3]; ++i3) {
                                          for (int i4 = 0; i4 < dim[4]; ++i4) {
                                                current = (current * a) % p;
                                                if ((current & 1) == 1) {
                                                      asteroids.Add(new State(new int[] { i0, i1, i2, i3, i4 }), null);
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  return navigate(dim, new State(normalize(start, 0)), new State(normalize(finish, 0)), asteroids, wormholes_);
            }

            private int[] normalize(int[] a, int d) {
                  int[] result = new int[5] { d, d, d, d, d };
                  for (int i = 0; i < a.Length; ++i) {
                        result[i] = a[i];
                  }
                  return result;
            }

            private int navigate(int[] dim, State start, State finish, Hashtable asteroids, Wormhole[] wormholes) {
                  int[,,,,] time = new int[dim[0], dim[1], dim[2], dim[3], dim[4]];
                  for (int i0 = 0; i0 < dim[0]; ++i0) {
                        for (int i1 = 0; i1 < dim[1]; ++i1) {
                              for (int i2 = 0; i2 < dim[2]; ++i2) {
                                    for (int i3 = 0; i3 < dim[3]; ++i3) {
                                          for (int i4 = 0; i4 < dim[4]; ++i4) {
                                                time[i0, i1, i2, i3, i4] = int.MaxValue;
                                          }
                                    }
                              }
                        }
                  }
                  start[time] = 0;
                  Queue<State> queue = new Queue<State>();
                  for (queue.Enqueue(start); queue.Count > 0; ) {
                        State curr = queue.Dequeue();
                        if (curr[time] != int.MaxValue) {
                              foreach (State next in curr.GetNext(dim, asteroids)) {
                                    if (next[time] > curr[time] + 1) {
                                          next[time] = curr[time] + 1;
                                          queue.Enqueue(next);
                                    }
                              }
                              foreach (Wormhole wormhole in wormholes) {
                                    if (curr.Equals(wormhole.Src)) {
                                          State next = wormhole.Dst;
                                          if (next[time] > curr[time] + wormhole.Time) {
                                                next[time] = curr[time] + wormhole.Time;
                                                queue.Enqueue(next);
                                          }
                                    }
                              }
                        }
                  }
                  return finish[time];
            }

            private class State : IComparable<State>, IEquatable<State> {
                  private readonly int[] location;

                  public State(int[] location) {
                        this.location = location;
                  }

                  public int this[int[,,,,] a] {
                        get { return a[location[0],
                                       location[1],
                                       location[2],
                                       location[3],
                                       location[4]]; }
                        set { a[location[0],
                                location[1],
                                location[2],
                                location[3],
                                location[4]] = value; }
                  }

                  public List<State> GetNext(int[] dim, Hashtable asteroids) {
                        List<State> result = new List<State>();
                        for (int d0 = -1; d0 <= +1; ++d0) {
                              for (int d1 = -1; d1 <= +1; ++d1) {
                                    for (int d2 = -1; d2 <= +1; ++d2) {
                                          for (int d3 = -1; d3 <= +1; ++d3) {
                                                for (int d4 = -1; d4 <= +1; ++d4) {
                                                      if (d0 == 0 && d1 == 0 && d2 == 0 && d3 == 0 && d4 == 0) {
                                                            continue;
                                                      }
                                                      int[] vect = new int[5] { d0, d1, d2, d3, d4 };
                                                      int[] next = new int[5];
                                                      for (int i = 0; i < 5; ++i) {
                                                            next[i] = location[i] + vect[i];
                                                            if (0 <= next[i] && next[i] < dim[i]) {
                                                            }
                                                            else goto next;
                                                      }
                                                      State state = new State(next);
                                                      if (asteroids.Contains(state)) {
                                                            goto next;
                                                      }
                                                      result.Add(state);
                                                      next:;
                                                }
                                          }
                                    }
                              }
                        }
                        return result;
                  }

                  public bool Equals(State other) {
                        if (other != null) {
                              if (this.location.Length == other.location.Length) {
                                    for (int i = 0; i < this.location.Length; ++i) {
                                          if (this.location[i] != other.location[i]) {
                                                return false;
                                          }
                                    }
                                    return true;
                              }
                        }
                        return false;
                  }

                  public override bool Equals(object obj) {
                        if (obj is State) {
                              return Equals(obj as State);
                        }
                        return false;
                  }

                  public override int GetHashCode() {
                        return this.ToString().GetHashCode();
                  }

                  public override string ToString() {
                        string result = string.Empty;
                        for (int i = 0; i < location.Length; ++i) {
                              result += location[i];
                              if (i + 1 < location.Length) {
                                    result += ';';
                              }
                        }
                        return string.Format("[{0}]", result);
                  }

                  public int CompareTo(State other) {
                        if (this.location.Length == other.location.Length) {
                              for (int i = 0; i < this.location.Length; ++i) {
                                    if (this.location[i] != other.location[i]) {
                                          return this.location[i].CompareTo(other.location[i]);
                                    }
                              }
                              return 0;
                        }
                        return this.location.Length.CompareTo(other.location.Length);
                  }
            }

            private class Wormhole {
                  public State Src, Dst;
                  public int Time;

                  public Wormhole(State src, State dst, int time) {
                        Src = src;
                        Dst = dst;
                        Time = time;
                  }
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new HigherMaze().navigate(138, 193,
                        new int[] { 5, 5 }, new int[] { 0, 0 }, new int[] { 4, 4 }, new string[] { }));
                  Console.WriteLine(new HigherMaze().navigate(138, 193,
                        new int[] {5,5}, new int[] {0,0}, new int[] {4,4}, new string[] {"0 0 2 4 4"}));
                  Console.WriteLine(new HigherMaze().navigate(138, 193,
                        new int[]{5,5}, new int[]{0,0}, new int[]{4,4}, new string[] {"0 0 2 4 4","0 0 8 1 4","0 2 5 1 4","1 4 -8 4 4"}));
                  Console.WriteLine(new HigherMaze().navigate(54, 73,
                        new int[]{20,20}, new int[]{1,2}, new int[]{12,12}, new string[] {}));
                  Console.WriteLine(new HigherMaze().navigate(139, 193,
                        new int[]{20,20}, new int[]{0,2}, new int[]{18,19}, new string[] {"0 2 -100000 0 19","4 18 -100000 7 0","8 0 1000000 19 10","19 6 -800000 13 8"}));
                  Console.WriteLine(new HigherMaze().navigate(138, 193,
                        new int[]{5,5}, new int[]{0,0}, new int[]{4,0}, new string[] {"0 0 -6 4 4"}));
                  Console.WriteLine(new HigherMaze().navigate(139, 193,
                        new int[] { 20 }, new int[] { 2 }, new int[] { 19 }, new string[] { }));
 
                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}