using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class SolitaireSimulation {
            public int periodLength(int[] heaps) {
                  return periodLength(new State(heaps));
            }

            private int periodLength(State state) {
                  positions[state] = 0;
                  for (int step = 1; true; ++step) {
                        state = state.GetNextState();
                        if (!positions.ContainsKey(state)) {
                              positions.Add(state, step);
                        }
                        else {
                              return step - positions[state];
                        }
                  }
            }

            private SortedDictionary<State, int> positions = new SortedDictionary<State, int>();

            private class State : IEquatable<State>, IComparable<State> {
                  private int[] heaps;

                  public State(int[] heap) {
                        this.heaps = (int[])heap.Clone();
                        Array.Sort(this.heaps);
                  }

                  public State GetNextState() {
                        List<int> next = new List<int>();
                        foreach (int heap in this.heaps) {
                              if (heap > 1) {
                                    next.Add(heap - 1);
                              }
                        }
                        next.Add(heaps.Length);
                        return new State(next.ToArray());
                  }

                  public bool Equals(State other) {
                        if (this.heaps.Length == other.heaps.Length) {
                              for (int i = 0; i < this.heaps.Length; ++i) {
                                    if (this.heaps[i] != other.heaps[i]) {
                                          return false;
                                    }
                              }
                              return true;
                        }
                        return false;
                  }

                  public override bool Equals(object obj) {
                        if (obj is State) {
                              return Equals(obj as State);
                        }
                        return false;
                  }

                  public int CompareTo(State other) {
                        if (this.heaps.Length != other.heaps.Length) {
                              return this.heaps.Length - other.heaps.Length;
                        }
                        for (int i = 0; i < this.heaps.Length; ++i) {
                              if (this.heaps[i] != other.heaps[i]) {
                                    return this.heaps[i] - other.heaps[i];
                              }
                        }
                        return 0;
                  }

                  public override int GetHashCode() {
                        return this.ToString().GetHashCode();
                  }

                  public override string ToString() {
                        string result = string.Empty;
                        for (int i = 0; i < this.heaps.Length; ++i) {
                              result += this.heaps[i].ToString();
                              if (i + 1 < this.heaps.Length) {
                                    result += ",";
                              }
                        }
                        return string.Format("[{0}]", result);
                  }
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new SolitaireSimulation().periodLength(new int[] { 1, 4 }));
                  Console.WriteLine(new SolitaireSimulation().periodLength(new int[] { 3, 1, 3 }));
                  Console.WriteLine(new SolitaireSimulation().periodLength(new int[] { 1 }));
                  Console.WriteLine(new SolitaireSimulation().periodLength(new int[] { 4, 3, 2, 1 }));
                  Console.WriteLine(new SolitaireSimulation().periodLength(new int[] { 3, 3, 3, 3 }));

                  Console.ReadLine();
            }
      }
}
