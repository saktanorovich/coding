using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PickTeam {
            public string[] pickPeople(int teamSize, string[] people) {
                  string[] names = new string[people.Length];
                  int[][] well = new int[people.Length][];
                  for (int i = 0; i < people.Length; ++i) {
                        names[i] = people[i].Substring(0, people[i].IndexOf(' '));
                        well[i] = Array.ConvertAll(people[i].Substring(people[i].IndexOf(' ') + 1).Split(' '),
                              delegate(string s) {
                                    return int.Parse(s);
                        });
                  }
                  int bestSum = int.MinValue;
                  List<int> bestSet = new List<int>();
                  for (int set = 1; set < (1 << people.Length); ++set) {
                        if (cardinality(set) == teamSize) {
                              List<int> who = new List<int>();
                              for (int i = 0; i < people.Length; ++i) {
                                    if ((set & (1 << i)) != 0) {
                                          who.Add(i);
                                    }
                              }
                              int sum = 0;
                              for (int i = 0; i < teamSize; ++i) {
                                    for (int j = i + 1; j < teamSize; ++j) {
                                          sum += well[who[i]][who[j]];
                                    }
                              }
                              if (bestSum < sum) {
                                    bestSum = sum;
                                    bestSet = who;
                              }
                        }
                  }
                  string[] result = Array.ConvertAll(bestSet.ToArray(),
                        delegate(int x) {
                              return names[x];
                  });
                  Array.Sort(result);
                  return result;
            }

            private int cardinality(int set) {
                  if (set > 0) {
                        return 1 + cardinality(set & (set - 1));
                  }
                  return 0;
            }
      }
}