using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class TournamentSeeding {
            public string[] getSeeds(string[] teams, string[] games, int[] seeds) {
                  return getSeeds(Team.GetTeams(concatenate(teams)), Game.GetGames(concatenate(games)), seeds);
            }

            private readonly string[] Invalid = new string[0];

            private string[] getSeeds(Team[] teams, Game[] games, int[] seeds) {
                  int numOfTeams = teams.Length;
                  int numOfGames = games.Length;
                  List<Team>[] graph = Array.ConvertAll(new object[numOfTeams], delegate(object x) {
                        return new List<Team>();
                  });
                  for (int i = 0; i < numOfGames; ++i) {
                        graph[games[i].Winner.Id].Add(games[i].Looser);
                        ++games[i].Winner.Wins;
                        ++games[i].Looser.Lost;
                        if (games[i].Looser.Lost > 1) {
                              return Invalid;
                        }
                  }
                  /* try to find teams which competed in each round... */
                  Team[] players = (Team[])teams.Clone();
                  int numOfRounds = getNumOfRounds(numOfTeams);
                  Team[,] tournament = new Team[numOfTeams, numOfRounds];
                  for (int round = 0; round < numOfRounds; ++round) {
                        List<Team> advancers = new List<Team>();
                        for (int i = 0; i < players.Length; ++i) {
                              Team player = players[i];
                              /* if the number of wins greater than round then the team will be advanced to the next round... */
                              if (player.Wins > round) {
                                    int possible = 0;
                                    foreach (Team looser in graph[player.Id]) {
                                          if (looser.Wins == round) {
                                                ++possible;
                                                tournament[player.Id, round] = looser;
                                          }
                                    }
                                    if (possible > 1) {
                                          return Invalid;
                                    }
                                    advancers.Add(players[i]);
                              }
                              /* otherwise only the teams which are not presented as loosers should be analyzed... */
                              else if (player.Lost == 0) {
                                    /* at this case we know that some games are not presented in the provided list... */
                                    for (int j = players.Length - 1; j >= 0; --j) {
                                          Team looser = players[j];
                                          if (looser.Lost == 0 && looser.Wins <= round) {
                                                looser.Lost = 1;
                                                tournament[player.Id, round] = looser;
                                                advancers.Add(players[i]);
                                                goto next;
                                          }
                                    }
                                    return Invalid;
                              }
                              next:;
                        }
                        if (advancers.Count != 1 << (numOfRounds - round - 1)) {
                              return Invalid;
                        }
                        players = advancers.ToArray();
                  }
                  /* try to build a valid assignment of the seeds... */
                  Team[] assignment = new Team[numOfTeams];
                  players[0].Seed = 0;
                  assignment[players[0].Seed] = players[0];
                  for (int round = numOfRounds - 1; round >= 0; --round) {
                        int numOfTeamsInTheRound = 1 << (numOfRounds - round);
                        foreach (Team winner in teams) {
                              Team looser = tournament[winner.Id, round];
                              if (looser != null) {
                                    looser.Seed = numOfTeamsInTheRound - 1 - winner.Seed;
                                    assignment[looser.Seed] = looser;
                              }
                        }
                  }
                  return Array.ConvertAll(seeds, delegate(int seed) {
                        return assignment[seed].Name;
                  });
            }

            private string[] concatenate(string[] s) {
                  string result = string.Empty;
                  for (int i = 0; i < s.Length; ++i) {
                        result = result + s[i];
                  }
                  return result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            private int getNumOfRounds(int numOfTeams) {
                  if ((numOfTeams & (numOfTeams - 1)) == 0) {
                        int result = 0;
                        for (; numOfTeams > 1; numOfTeams /= 2) {
                              result = result + 1;
                        }
                        return result;
                  }
                  throw new NotSupportedException();
            }

            private class Team {
                  private static Dictionary<string, Team> teamByName = new Dictionary<string, Team>();

                  public static Team[] GetTeams(string[] teams) {
                        Array.Sort(teams);
                        Team[] result = new Team[teams.Length];
                        for (int i = 0; i < teams.Length; ++i) {
                              result[i] = new Team(i, teams[i]);
                              teamByName[teams[i]] = result[i];
                        }
                        return result;
                  }

                  public static Team GetTeamByName(string name) {
                        return teamByName[name];
                  }

                  private Team(int id, string name) {
                        Id = id;
                        Name = name;
                  }

                  public string Name { get; private set; }
                  public int Id { get; private set; }

                  public int Wins { get; set; }
                  public int Lost { get; set; }
                  public int Seed { get; set; }

                  public override string ToString() {
                        return string.Format("{0}", Name);
                  }
            }

            private class Game {
                  public static Game[] GetGames(string[] games) {
                        Game[] result = new Game[games.Length / 2];
                        for (int i = 0; i < games.Length; i += 2) {
                              result[i / 2] = new Game(Team.GetTeamByName(games[i]), Team.GetTeamByName(games[i + 1]));
                        }
                        return result;
                  }

                  public Team Winner { get; private set; }
                  public Team Looser { get; private set; }

                  public Game(Team winner, Team looser) {
                        Winner = winner;
                        Looser = looser;
                  }

                  public override string ToString() {
                        return string.Format("{0} - {1}", Winner, Looser);
                  }
            }

            private static string ToString(string[] s) {
                  string result = string.Empty;
                  for (int i = 0; i < s.Length; ++i) {
                        result += s[i];
                        if (i + 1 < s.Length) {
                              result += ' ';
                        }
                  }
                  return "[" + result + "]";
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "CELTICS ", "LAKER", "S SPURS PISTONS" },
                        new string[] {"CELTICS LAKERS CELTICS PISTONS LAKERS SPURS"},
                        new int[] { 0, 1, 2, 3 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "GIANTS PATRIOTS CHARGERS PACKERS" },
                        new string[] { "PATRIOTS CHARGERS" },
                        new int[] { 3, 2, 1, 0 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] {"REDSOX PHILLIES METS DODGER", "S ORIOLES BLUEJAYS CUBS AN", "GELS"},
                        new string[] {"METS ANGELS", " METS CU", "BS ORIO", "LES ANGELS"},
                        new int[] { 0, 1, 2, 3, 4, 5, 5, 5 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] {"REDSOX PHILLIES METS DODGER", "S ORIOLES BLUEJAYS CUBS AN", "GELS"},
                        new string[] {"METS ANGELS", " METS CU", "BS CU", "BS DODGERS REDSOX PHILLIES"},
                        new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "A B C D E F 8 H I 3 9 L 4 N O P" },
                        new string[] { "P A B H D C D E E N" },
                        new int[] { 0, 2, 0, 0, 3, 4, 7, 2 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "A B C D E F G H" },
                        new string[] { "A B C D A C E F" },
                        new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "NEWYORKISLANDERS" },
                        new string[] { },
                        new int[] { 0 })));

                  Console.WriteLine(ToString(new TournamentSeeding().getSeeds(
                        new string[] { "CELTICS ", "LAKER", "S SPURS PISTONS" },
                        new string[] { "CELTICS LAKERS CELTICS PISTONS LAKERS SPURS SPURS CELTICS" },
                        new int[] { 0, 1, 2, 3 })));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}