using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Roundabout {
            public int clearUpTime(string north, string east, string south, string west) {
                  return clearUpTime(new int[4][] { parse(north), parse(west), parse(south), parse(east) });
            }

            private int[] parse(string direction) {
                  return Array.ConvertAll(direction.TrimEnd(new char[] { '-' }).ToCharArray(),
                        delegate(char c) {
                              return "NWSE".IndexOf(c);
                  });
            }

            private int clearUpTime(int[][] schedule) {
                  RoundaboutState state = new RoundaboutState();
                  for (int time = 0; true; ++time) {
                        state = state.next(schedule, time);
                        if (state == null) {
                              return time;
                        }
                  }
            }

            private class RoundaboutState {
                  private static Queue<int>[] queue = new Queue<int>[4] {
                        new Queue<int>(),
                        new Queue<int>(),
                        new Queue<int>(),
                        new Queue<int>()
                  };
                  private int[] currEntryState;

                  public RoundaboutState()
                        : this(new int[4] { -1, -1, -1, -1 }) {
                  }

                  public RoundaboutState(int[] entryState) {
                        this.currEntryState = entryState;
                  }

                  public RoundaboutState next(int[][] schedule, int time) {
                        bool hasAnyIncomingOrMovingCar = false;
                        for (int entry = 0; entry < 4; ++entry) {
                              if (time < schedule[entry].Length) {
                                    if (schedule[entry][time] >= 0) {
                                          queue[entry].Enqueue(schedule[entry][time]);
                                    }
                                    hasAnyIncomingOrMovingCar = true;
                              }
                              if (queue[entry].Count > 0 || currEntryState[entry] >= 0) {
                                    hasAnyIncomingOrMovingCar = true;
                              }
                        }
                        if (hasAnyIncomingOrMovingCar) {
                              int[] nextEntryState = new int[4] { -1, -1, -1, -1 };
                              for (int entry = 0; entry < 4; ++entry) {
                                    if (currEntryState[entry] != entry) {
                                          nextEntryState[nextEntry(entry)] = currEntryState[entry];
                                    }
                              }
                              if (getNumOfIncomingCars() < 4) {
                                    bool[] incomingAccepted = new bool[4];
                                    for (int entry = 0; entry < 4; ++entry) {
                                          if (queue[entry].Count > 0) {
                                                if (queue[prevEntry(entry)].Count == 0 &&
                                                      currEntryState[prevEntry(entry)] == -1) {
                                                            incomingAccepted[entry] = true;
                                                }
                                          }
                                    }
                                    for (int entry = 0; entry < 4; ++entry) {
                                          if (incomingAccepted[entry]) {
                                                nextEntryState[entry] = queue[entry].Dequeue();
                                          }
                                    }
                              }
                              else if (currEntryState[prevEntry(0)] == -1) {
                                    nextEntryState[0] = queue[0].Dequeue();
                              }
                              return new RoundaboutState(nextEntryState);
                        }
                        return null;
                  }

                  private int getNumOfIncomingCars() {
                        int numOfIncomingCars = 0;
                        for (int entry = 0; entry < 4; ++entry) {
                              if (queue[entry].Count > 0) {
                                    ++numOfIncomingCars;
                              }
                        }
                        return numOfIncomingCars;
                  }

                  private static int nextEntry(int entry) { return (entry + 1 + 4) % 4; }
                  private static int prevEntry(int entry) { return (entry - 1 + 4) % 4; }
            }
      }
}