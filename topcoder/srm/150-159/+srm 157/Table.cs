using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Table {
            public string[] layout(string[] tbl) {
                  return layout(Tuple.parse(tbl));
            }

            private string[] layout(List<Tuple>[] list) {
                  char[][] table = new char[limit][];
                  for (int i = 0; i < table.Length; ++i) {
                        table[i] = new char[limit];
                  }
                  for (int row = 0; row < list.Length; ++row) {
                        for (int k = 0; k < list[row].Count; ++k) {
                              Tuple tuple = list[row][k];
                              for (int col = 0; col < limit; ++col) {
                                    if (table[row][col] == 0) {
                                          for (int i = 0; i < tuple.rowspan; ++i) {
                                                for (int j = 0; j < tuple.colspan; ++j) {
                                                      table[row + i][col + j] = tuple.content;
                                                }
                                          }
                                          break;
                                    }
                              }
                        }
                  }
                  List<string> result = new List<string>();
                  for (int i = 0; i < table.Length; ++i) {
                        if (table[i][0] != 0) {
                              string entry = string.Empty;
                              for (int j = 0; j < table[i].Length; ++j) {
                                    if (table[i][j] != 0) {
                                          entry += table[i][j];
                                    }
                                    else break;
                              }
                              result.Add(entry);
                        }
                  }
                  return result.ToArray();
            }

            private static readonly int limit = 100;

            private class Tuple {
                  public int colspan;
                  public int rowspan;
                  public char content;

                  public Tuple(int colspan, int rowspan, char content) {
                        this.colspan = colspan;
                        this.rowspan = rowspan;
                        this.content = content;
                  }

                  public static List<Tuple>[] parse(string[] tbl) {
                        List<Tuple>[] result = new List<Tuple>[tbl.Length];
                        for (int i = 0; i < tbl.Length; ++i) {
                              result[i] = parse(tbl[i].Replace('(', '|').Replace(')', '|'));
                        }
                        return result;
                  }

                  private static List<Tuple> parse(string tbl) {
                        List<Tuple> result = new List<Tuple>();
                        foreach (string entry in tbl.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)) {
                              string[] items = entry.Split(',');
                              int colspan = int.Parse(items[0]);
                              int rowspan = int.Parse(items[1]);
                              result.Add(new Tuple(colspan, rowspan, items[2][0]));
                        }
                        return result;
                  }
            }
      }
}