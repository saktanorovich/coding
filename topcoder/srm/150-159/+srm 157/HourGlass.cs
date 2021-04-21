using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class HourGlass {
            public int[] measurable(int glass1, int glass2) {
                  List<int> result = new List<int>();
                  bool[] possible = getPossibleMeasures(Math.Min(glass1, glass2), Math.Max(glass1, glass2));
                  for (int measure = 1; measure < possible.Length && result.Count < limit; ++measure) {
                        if (possible[measure]) {
                              result.Add(measure);
                        }
                  }
                  return result.ToArray();
            }

            private bool[] getPossibleMeasures(int min, int max) {
                  bool[] possible = new bool[max * limit + 1];
                  if (min == max) {
                        for (int times = 1; times <= limit; ++times) {
                              possible[times * min] = true;
                        }
                  }
                  else {
                        HourGlassesState.minLimit = min;
                        HourGlassesState.maxLimit = max;
                        bool[,,] was = new bool[min + 1, max + 1, max * limit + 1];
                        calculate(new HourGlassesState(0, 0, 0), was, possible);
                  }
                  return possible;
            }

            private void calculate(HourGlassesState state, bool[,,] was, bool[] possible) {
                  if (state.time < HourGlassesState.maxLimit * limit) {
                        if (!was[state.minState, state.maxState, state.time]) {
                              was[state.minState, state.maxState, state.time] = true;
                              if (state.possible()) {
                                    possible[state.time] = true;
                              }
                              foreach (HourGlassesState next in state.getNext()) {
                                    calculate(next, was, possible);
                              }
                        }
                  }
            }

            private static readonly int limit = 10;

            private class HourGlassesState {
                  public static int minLimit;
                  public static int maxLimit;
                  public int minState;
                  public int maxState;
                  public int time;

                  public HourGlassesState(int minState, int maxState, int time) {
                        this.minState = minState;
                        this.maxState = maxState;
                        this.time = time;
                  }

                  public List<HourGlassesState> getNext() {
                        List<HourGlassesState> result = new List<HourGlassesState>();
                        if (minState + maxState == 0) {
                              result.Add(new HourGlassesState(minLimit, maxLimit, time));
                        }
                        else if (minState == 0) {
                              result.Add(new HourGlassesState(minLimit, flip(maxState, maxState), time));
                              result.Add(new HourGlassesState(minLimit, flip(maxState, maxLimit), time));
                              result.Add(new HourGlassesState(minState, 0, time + maxState));
                        }
                        else if (maxState == 0) {
                              result.Add(new HourGlassesState(flip(minState, minState), maxLimit, time));
                              result.Add(new HourGlassesState(flip(minState, minLimit), maxLimit, time));
                              result.Add(new HourGlassesState(0, maxState, time + minState));
                        }
                        else {
                              int min = Math.Min(minState, maxState);
                              int max = Math.Max(minState, maxState);
                              result.Add(new HourGlassesState(minState - min, maxState - min, time + min));
                        }
                        return result;
                  }

                  public bool possible() {
                        return minState == 0 || maxState == 0;
                  }

                  private int flip(int state, int limit) {
                        if (limit > state) {
                              return limit - state;
                        }
                        return state;
                  }
            }
      }
}