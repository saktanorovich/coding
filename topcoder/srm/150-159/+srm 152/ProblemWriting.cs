using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ProblemWriting {
            public string myCheckData(string dotForm) {
                  if (1 <= dotForm.Length && dotForm.Length <= 25) {
                        int position = getIncorrectPosition(dotForm);
                        if (position >= 0) {
                              return string.Format("dotForm is not in dot notation, check character {0}.", position);
                        }
                        return string.Empty;
                  }
                  return "dotForm must contain between 1 and 25 characters, inclusive.";
            }

            private int getIncorrectPosition(string dotForm) {
                  int state = 0;
                  for (int position = 0; position < dotForm.Length; ++position) {
                        try {
                              state = stateMachine[state, getCharacterClass(dotForm[position])];
                              if (state == -1) {
                                    return position;
                              }
                        }
                        catch (Exception) {
                              return position;
                        }
                  }
                  if (state != 1) {
                        return dotForm.Length;
                  }
                  return -1;
            }

            private int getCharacterClass(char c) {
                  int position = alphabet.IndexOf(c);
                  if (position >= 0) {
                        if (position > 0) {
                              if (position > 4) {
                                    return 0;
                              }
                              return 1;
                        }
                        return 2;
                  }
                  throw new Exception();
            }

            private static readonly string alphabet = ".+-*/0123456789";
            private static readonly int[,] stateMachine = new int[4, 3] {
                  {  1, -1, -1 },
                  { -1,  2,  3 },
                  {  1, -1,  2 },
                  { -1,  2,  3 }
            };
      }
}