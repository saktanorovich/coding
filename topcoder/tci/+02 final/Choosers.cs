using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Choosers {
            public int length(string[] game, int start) {
                  int state = 0;
                  int[,] chooser = new int[game.Length, 2];
                  for (int i = 0; i < game.Length; ++i) {
                        string[] items = game[i].Split(new char[] { ' ' });
                        state |= "LR".IndexOf(items[0]) << i;
                        chooser[i, 0] = items[1] != "X" ? int.Parse(items[1]) : -1;
                        chooser[i, 1] = items[2] != "X" ? int.Parse(items[2]) : -1;
                  }
                  int result = 1;
                  for (bool[,] visited = new bool[game.Length, 1 << game.Length]; true; ) {
                        if (!visited[start, state]) {
                              visited[start, state] = true;
                              int next = chooser[start, (state >> start) & 1];
                              if (next >= 0) {
                                    result = result + 1;
                                    state ^= 1 << start;
                                    start = next;
                              }
                              else break;
                        }
                        else return -1;
                  }
                  return result;
            }
      }
}