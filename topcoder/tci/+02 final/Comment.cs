using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Comment {
            public string[] strip(string[] code) {
                  code = removeComments(code);
                  List<string> result = new List<string>();
                  for (int i = 0; i < code.Length; ++i) {
                        if (!string.IsNullOrEmpty(code[i])) {
                              result.Add(code[i]);
                        }
                  }
                  return result.ToArray();
            }

            private string[] removeComments(string[] code) {
                  string[] result = Array.ConvertAll(new int[code.Length],
                        delegate(int x) {
                              return string.Empty;
                  });
                  int state = 0;
                  for (int i = 0; i < code.Length; ++i) {
                        for (int j = 0; j < code[i].Length; ++j) {
                              switch (state) {
                                    case 0:
                                          if (isLineComment(code[i], j)) {
                                                goto nextLine;
                                          }
                                          if (isOpenAsteriskComment(code[i], j)) {
                                                state = 1;
                                                j = j + 1;
                                                continue;
                                          }
                                          if (code[i][j].Equals('"')) {
                                                state = 2;
                                          }
                                          result[i] += code[i][j];
                                          break;
                                    case 1:
                                          if (isCloseAsteriskComment(code[i], j)) {
                                                state = 0;
                                                j = j + 1;
                                          }
                                          break;
                                    case 2:
                                          if (code[i][j].Equals('\\')) {
                                                result[i] += code[i][j];
                                                j = j + 1;
                                                result[i] += code[i][j];
                                                continue;
                                          }
                                          else if (code[i][j].Equals('\"')) {
                                                state = 0;
                                          }
                                          result[i] += code[i][j];
                                          break;
                              }
                        }
                        nextLine:;
                  }
                  return result;
            }

            private bool isLineComment(string s, int position) {
                  if (position + 1 < s.Length) {
                        return s[position].Equals('/') && s[position + 1].Equals('/');
                  }
                  return false;
            }

            private bool isOpenAsteriskComment(string s, int position) {
                  if (position + 1 < s.Length) {
                        return s[position].Equals('/') && s[position + 1].Equals('*');
                  }
                  return false;
            }

            private bool isCloseAsteriskComment(string s, int position) {
                  if (position + 1 < s.Length) {
                        return s[position].Equals('*') && s[position + 1].Equals('/');
                  }
                  return false;
            }
      }
}