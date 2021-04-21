using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ContestScore {
            public string[] sortResults(string[] data) {
                  if (data.Length > 0) {
                        Team[] teams = Array.ConvertAll(data,
                              delegate(string s) {
                                    return Team.Parse(s);
                        });
                        int numOfJudges = data[0].Split(' ').Length - 1;
                        for (int judge = 0; judge < numOfJudges; ++judge) {
                              SortedDictionary<int, int> scores = new SortedDictionary<int, int>();
                              foreach (Team team in teams) {
                                    if (!scores.ContainsKey(-team.score[judge])) {
                                          scores[-team.score[judge]] = 0;
                                    }
                                    ++scores[-team.score[judge]];
                              }
                              int rank = 1;
                              foreach (KeyValuePair<int, int> entry in scores) {
                                    int score = -entry.Key;
                                    int count = +entry.Value;
                                    foreach (Team team in teams) {
                                          if (team.score[judge] == score) {
                                                team.ranks[judge] = rank;
                                          }
                                    }
                                    rank = rank + count;
                              }
                        }
                        Array.Sort(teams);
                        return Array.ConvertAll(teams, delegate(Team team) {
                              return team.ToString();
                        });
                  }
                  return new string[0];
            }

            private class Team : IComparable<Team> {
                  public string name;
                  public int[] score;
                  public int[] ranks;

                  public Team(string name, int[] scores) {
                        this.name = name;
                        this.score = scores;
                        this.ranks = new int[scores.Length];
                  }

                  public override string ToString() {
                        return string.Format("{0} {1} {2:f1}", name, Total(ranks), 1.0 * Total(score) / 10);
                  }

                  public int CompareTo(Team other) {
                        if (Total(this.ranks) != Total(other.ranks)) {
                              return +Total(this.ranks) - Total(other.ranks);
                        }
                        if (Total(this.score) != Total(other.score)) {
                              return -Total(this.score) + Total(other.score);
                        }
                        return this.name.CompareTo(other.name);
                  }

                  public static Team Parse(string team) {
                        string name = team.Substring(0, team.IndexOf(' '));
                        int[] scores = Array.ConvertAll(team.Substring(
                              team.IndexOf(' ') + 1).Split(' '),
                                    delegate(string s) {
                                          return Convert.ToInt32(double.Parse(s) * 10);
                        });
                        return new Team(name, scores);
                  }

                  public static int Total(int[] a) {
                        int result = 0;
                        foreach (int element in a) {
                              result += element;
                        }
                        return result;
                  }
            }
      }
}