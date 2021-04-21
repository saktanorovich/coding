using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class NTicTacToe {
            public string compute(string[] xmoves, string[] omoves, int n) {
                  List<Move> moves = new List<Move>();
                  for (int i = 0; i < Math.Max(xmoves.Length, omoves.Length); ++i) {
                        if (i < xmoves.Length) moves.Add(Move.Parse(0, xmoves[i]));
                        if (i < omoves.Length) moves.Add(Move.Parse(1, omoves[i]));
                  }
                  return compute(moves, n);
            }

            private string compute(List<Move> moves, int n) {
                  int[, ,] field = new int[n, n, n];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              for (int k = 0; k < n; ++k) {
                                    field[i, j, k] = -1;
                              }
                        }
                  }
                  for (int move = 0; move < moves.Count; ++move) {
                        int[] w = moves[move].Where;
                        field[w[0], w[1], w[2]] = moves[move].Player;
                        for (int i = 0; i < n; ++i) {
                              for (int j = 0; j < n; ++j) {
                                    for (int k = 0; k < n; ++k) {
                                          if (field[i, j, k] == moves[move].Player) {
                                                if (win(i, j, k, field, n, moves[move].Player)) {
                                                      return string.Format("{0},{1}", "XO"[moves[move].Player], (move + 2) / 2);
                                                }
                                          }
                                    }
                              }
                        }
                  }
                  return "NO WINNER";
            }

            private bool win(int i, int j, int k, int[,,] field, int n, int player) {
                  for (int di = -1; di <= +1; ++di) {
                        for (int dj = -1; dj <= +1; ++dj) {
                              for (int dk = -1; dk <= +1; ++dk) {
                                    if (di != 0 || dj != 0 || dk != 0) {
                                          int total = 0;
                                          for (int t = 0; t < n; ++t) {
                                                int ii = i + t * di;
                                                int jj = j + t * dj;
                                                int kk = k + t * dk;
                                                if (0 <= ii && ii < n && 0 <= jj && jj < n && 0 <= kk && kk < n) {
                                                      if (field[ii, jj, kk] == player) {
                                                            total = total + 1;
                                                      }
                                                }
                                          }
                                          if (total == n) {
                                                return true;
                                          }
                                    }
                              }
                        }
                  }
                  return false;
            }

            private class Move {
                  public int[] Where;
                  public int Player;

                  public Move(int[] where, int player) {
                        Where = where;
                        Player = player;
                  }

                  public static Move Parse(int player, string move) {
                        return new Move(Array.ConvertAll(move.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                              delegate(string s) {
                                    return int.Parse(s);
                        }), player);
                  }
            }

            public static void Main(string[] args) {
                  // X,4
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "1,2,1", "2,1,2", "3,0,3", "0,3,0" },
                        new string[] { "1,2,2", "2,2,2", "0,2,3" }, 4));
                  // X,8
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "1,5,1", "2,5,2", "0,5,0", "4,5,4", "7,5,7", "3,5,3", "6,5,6", "5,5,5", "3,3,3" },
                        new string[] { "3,4,3", "7,7,7", "6,6,6", "5,6,5", "4,3,2", "1,2,3", "1,1,1", "4,2,2", "7,7,6" }, 8));
                  // NO WINNER
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,2,2", "1,2,2", "2,2,2", "1,0,0" },
                        new string[] { "2,2,1", "1,2,1", "1,1,1" }, 4));
                  // O,3
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "2,1,0", "0,0,0", "2,2,2" },
                        new string[] { "1,1,1", "1,1,0", "1,1,2" }, 3));
                  // O,7
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,0,5", "1,0,4", "3,3,2", "0,0,0", "4,0,1", "5,0,2", "4,3,1" },
                        new string[] { "0,5,1", "0,5,2", "0,5,5", "0,5,3", "0,5,4", "5,5,1", "0,5,0" }, 6));
                  // X,9
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "3,3,3", "4,4,1", "3,2,2", "3,4,4", "1,0,4", "3,0,0", "3,5,5,", "3,1,1", "3,6,6" },
                        new string[] { "3,0,3", "1,4,1", "0,2,2", "6,4,4", "1,0,3", "3,6,0", "3,3,5,", "5,3,1" }, 7));
                  // O,3
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "2,2,2", "1,1,1", "1,1,2" },
                        new string[] { "0,0,0", "1,1,0", "2,2,0" }, 3));
                  // O,5
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "4,4,3", "2,2,2", "1,1,1", "1,1,0", "1,3,4" },
                        new string[] { "1,1,4", "0,0,4", "4,4,4", "3,3,4", "2,2,4" }, 5));
                  // X,3
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,0,0", "1,1,1", "2,2,2" },
                        new string[] { "2,1,1", "1,0,0" }, 3));
                  // X,8
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "1,5,1", "2,5,2", "0,5,0", "4,5,4", "7,5,7", "3,5,3", "6,5,6", "5,5,5" },
                        new string[] { "3,4,3", "7,7,7", "6,6,6", "5,6,5", "4,3,2", "1,2,3", "1,1,1" }, 8));
                  // NO WINNER
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,0,5", "1,0,4", "2,0,3", "3,3,2", "0,0,0", "4,0,1", "5,0,2" },
                        new string[] { "0,0,1", "1,1,2", "2,2,1", "3,3,1", "4,4,4", "5,5,1" }, 6));
                  // X,3
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,2,2", "1,2,2", "2,2,2", "1,0,0" },
                        new string[] { "2,2,1", "1,2,1", "1,1,1" }, 3));
                  // O,8
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "1,1,1", "2,4,3", "3,4,4", "2,4,2", "6,5,6", "4,5,5", "3,3,3", "3,3,6" },
                        new string[] { "6,0,6", "5,5,5", "6,6,0", "6,5,1", "6,3,3", "6,4,2", "6,2,4", "6,1,5" }, 7));
                  // X,5
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "3,3,2", "3,2,2", "3,3,3", "3,0,2", "3,1,2", "2,2,1" },
                        new string[] { "3,0,3", "1,2,1", "0,2,2", "1,1,1", "2,2,2" }, 4));
                  // X,9
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,8,0", "8,0,8", "7,1,7", "5,3,5", "6,2,6", "1,7,1", "2,6,2", "3,5,3", "4,4,4" },
                        new string[] { "2,3,5", "3,1,2", "6,3,1", "8,2,1", "8,8,8", "3,3,4", "2,1,4", "8,2,5" }, 9));
                  // O,5
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "1,1,1", "2,3,4", "2,4,3", "2,0,0", "4,4,4" },
                        new string[] { "0,0,4", "2,2,2", "1,1,3", "4,4,0", "3,3,1" }, 5));
                  // X,7
                  Console.WriteLine(new NTicTacToe().compute(
                        new string[] { "0,6,6", "3,3,3", "2,4,4", "1,5,5", "4,2,2", "5,1,1", "6,0,0" },
                        new string[] { "2,3,4", "6,5,3", "2,2,2", "2,6,2", "0,0,0", "2,6,6" }, 7));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}