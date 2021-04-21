using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Masterbrain {
            public int possibleSecrets(string[] guesses, string[] results) {
                  int result = 0;
                  for (int secret = 1111; secret <= 7777; ++secret) {
                        if (isValidSecret(secret.ToString())) {
                              int totalGuessesMatch = 0;
                              for (int i = 0; i < guesses.Length; ++i) {
                                    if (results[i].Equals(match(guesses[i], secret.ToString()))) {
                                          totalGuessesMatch = totalGuessesMatch + 1;
                                    }
                              }
                              if (totalGuessesMatch == guesses.Length - 1) {
                                    result = result + 1;
                              }
                        }
                  }
                  return result;
            }

            private bool isValidSecret(string secret) {
                  return secret.IndexOf('0') == -1 &&
                         secret.IndexOf('8') == -1 &&
                         secret.IndexOf('9') == -1;
            }

            private string match(string guess, string secret) {
                  int black = 0;
                  int white = 0;
                  bool[] g = new bool[guess.Length];
                  bool[] s = new bool[guess.Length];
                  for (int i = 0; i < guess.Length; ++i) {
                        if (guess[i] == secret[i]) {
                              black = black + 1;
                              g[i] = true;
                              s[i] = true;
                        }
                  }
                  for (int i = 0; i < guess.Length; ++i) {
                        for (int j = 0; j < guess.Length; ++j) {
                              if (!g[i] && !s[j] && guess[i] == secret[j]) {
                                    white = white + 1;
                                    g[i] = true;
                                    s[j] = true;
                              }
                        }
                  }
                  return string.Format("{0}b {1}w", black, white);
            }
      }
}