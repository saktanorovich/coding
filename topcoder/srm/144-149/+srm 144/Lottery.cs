using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Lottery {
            public string[] sortByOdds(string[] rules_) {
                  List<Rule> rules = new List<Rule>(Array.ConvertAll(rules_,
                        delegate(string rule) {
                              return Rule.parse(rule);
                  }));
                  rules.Sort();
                  return Array.ConvertAll(rules.ToArray(), delegate(Rule rule) {
                        return rule.name;
                  });
            }

            private class Rule : IComparable<Rule> {
                  public string name;
                  public long numOfTickets;

                  public Rule(string name, long numOfTickets) {
                        this.name = name;
                        this.numOfTickets = numOfTickets;
                  }

                  public static Rule parse(string rule) {
                        int position = rule.IndexOf(':');
                        return new Rule(rule.Substring(0, position),
                                          getNumOfTickets(rule.Substring(position + 1, rule.Length - position - 1)));
                  }

                  private static long getNumOfTickets(string rule) {
                        string[] splitted = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        return getNumOfTickets(
                              int.Parse(splitted[0]),
                              int.Parse(splitted[1]),
                              "FT".IndexOf(splitted[2][0]),
                              "FT".IndexOf(splitted[3][0]));
                  }

                  private static long getNumOfTickets(int choices, int blanks, int sorted, int unique) {
                        if (sorted > 0) {
                              long[,] memo = new long[choices + 1, blanks];
                              for (int choice = 1; choice <= choices; ++choice) {
                                    memo[choice, 0] = choice;
                                    for (int i = 1; i < blanks; ++i) {
                                          memo[choice, i] = -1;
                                    }
                              }
                              return getNumOfTicketsSorted(choices, blanks - 1, unique, memo);
                        }
                        long result = 1;
                        for (int i = 0; i < blanks; ++i) {
                              result *= choices - unique * i;
                        }
                        return result;
                  }

                  private static long getNumOfTicketsSorted(int choices, int blanks, int unique, long[,] memo) {
                        if (memo[choices, blanks] == -1) {
                              memo[choices, blanks] = 0;
                              for (int next = choices; next >= 1; --next) {
                                    memo[choices, blanks] += getNumOfTicketsSorted(next - unique, blanks - 1, unique, memo);
                              }
                        }
                        return memo[choices, blanks];
                  }

                  public int CompareTo(Rule other) {
                        if (this.numOfTickets != other.numOfTickets) {
                              return this.numOfTickets.CompareTo(other.numOfTickets);
                        }
                        return this.name.CompareTo(other.name);
                  }
            }
      }
}