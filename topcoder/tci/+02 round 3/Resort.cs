using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class Resort {
            public string classify(string[] runs, string classifyName) {
                  return classify(Array.ConvertAll(runs, delegate(string s) {
                        Run result = Run.GetRunByName(s.Substring(0, s.IndexOf(':')));
                        switch (s.Substring(s.IndexOf(':') + 1, 1)) {
                              case "E": result.Difficulty = 0; break;
                              case "M": result.Difficulty = 1; break;
                              case "H": result.Difficulty = 2; break;
                        }
                        int position = s.IndexOf(',');
                        if (position >= 0) {
                              foreach (string feed in s.Substring(position, s.Length - position).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                                    result.Feeds.Add(Run.GetRunByName(feed));
                              }
                        }
                        return result;
                  }), Run.GetRunByName(classifyName));
            }

            private string classify(Run[] runs, Run origin) {
                  return getClassName(dfs(origin, Array.ConvertAll(new int[runs.Length],
                        delegate(int x) {
                              return -1;
                  })));
            }

            private int dfs(Run origin, int[] cache) {
                  if (cache[origin.Id] == -1) {
                        cache[origin.Id] = origin.Difficulty;
                        if (origin.Feeds.Count > 0) {
                              cache[origin.Id] = 2;
                              foreach (Run feed in origin.Feeds) {
                                    cache[origin.Id] = Math.Min(cache[origin.Id], dfs(feed, cache));
                              }
                              cache[origin.Id] = Math.Max(cache[origin.Id], origin.Difficulty);
                        }
                  }
                  return cache[origin.Id];
            }

            private string getClassName(int difficulty) {
                  switch (difficulty) {
                        case 0: return "GREEN CIRCLE";
                        case 1: return "BLUE SQUARE";
                        case 2: return "BLACK DIAMOND";
                  }
                  throw new Exception();
            }

            private class Run {
                  private static readonly Dictionary<string, Run> runByName = new Dictionary<string, Run>();
                  private static int numOfRuns = 0;

                  public static Run GetRunByName(string runName) {
                        if (!runByName.ContainsKey(runName)) {
                              Run run = new Run(numOfRuns, runName);
                              numOfRuns = numOfRuns + 1;
                              runByName.Add(runName, run);
                        }
                        return runByName[runName];
                  }

                  public int Id;
                  public string Name;
                  public int Difficulty;
                  public List<Run> Feeds = new List<Run>();

                  public Run(int id, string name) {
                        Id = id;
                        Name = name;
                  }

                  public override string ToString() {
                        return Name;
                  }
            };

            private static string ToString<T>(T[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i].ToString();
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                        result += Environment.NewLine;
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new Resort().classify(new string[] {
"BLACK STALLION:M,BLUE MOUNTAIN,TOPCODER,DOKS RUN"
,"BLUE MOUNTAIN:H"
,"TOPCODER:H"
,"DOKS RUN:H,CHOGYS RUN"
,"CHOGYS RUN:M,EASY RUN,MEDIUM RUN,HARD RUN"
,"EASY RUN:E"
,"MEDIUM RUN:M"
,"HARD RUN:H"}, "BLACK STALLION"));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}