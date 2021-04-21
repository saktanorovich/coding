using System;
using System.Collections;
using System.Collections.Generic;

namespace topcoder.algorithm {
      public class NumberGuessing {
            public int bestGuess(int range, int[] guesses, int numLeft) {
                  return bestGuess(range, new List<int>(guesses), numLeft);
            }

            /* assumes that range^numLeft will be less than or equal to 1,000,000 (taken from problem statement). */
            private int bestGuess(int R, List<int> currguesses, int numLeft) {
                  if (numLeft == 0) {
                        if (currguesses.Count > 0) {
                              currguesses.Add(1 - 1);
                              currguesses.Add(R + 1);
                              currguesses.Sort(); /* the guesses should be sorted in order to find minimum guess... */
                              int bestguess = 0, bestscore = 0;
                              for (int i = 1; i + 1 < currguesses.Count; ++i) {
                                    relax(ref bestscore, ref bestguess, getScore(R, currguesses[i] - 1, currguesses[i - 1], currguesses[i]), currguesses[i] - 1);
                                    relax(ref bestscore, ref bestguess, getScore(R, currguesses[i] + 1, currguesses[i], currguesses[i + 1]), currguesses[i] + 1);
                              }
                              currguesses.Remove(1 - 1);
                              currguesses.Remove(R + 1);
                              return bestguess;
                        }
                        return 1;
                  }
                  else {
                        int bestguess = 0, bestscore = 0;
                        for (int guess = 1; guess <= R; ++guess) {
                              if (!currguesses.Contains(guess)) {
                                    List<int> nextguesses = new List<int>(currguesses); nextguesses.Add(guess);
                                    for (int i = 1; i <= numLeft; ++i) {
                                          nextguesses.Add(bestGuess(R, nextguesses, numLeft - i));
                                    }
                                    relax(ref bestscore, ref bestguess, getScore(R, nextguesses, guess), guess);
                              }
                        }
                        return bestguess;
                  }
            }

            private int getScore(int R, List<int> guesses, int guess) {
                  guesses.Sort();
                  int lo = 0, hi = R + 1;
                  for (int i = 0; i < guesses.Count; ++i) {
                        if (guesses[i] < guess) lo = Math.Max(lo, guesses[i]);
                        if (guesses[i] > guess) hi = Math.Min(hi, guesses[i]);
                  }
                  return getScore(R, guess, lo, hi);
            }

            private int getScore(int R, int guess, int lo, int hi) { /* (lo, hi) are numbers nearest to the guess... */
                  if (lo >= 1) lo = (lo + guess + 0) / 2;
                  if (hi <= R) hi = (hi + guess + 1) / 2;
                  return (hi - lo - 1);
            }

            private void relax(ref int bestscore, ref int bestguess, int currscore, int currguess) {
                  if (bestscore < currscore) {
                        bestscore = currscore;
                        bestguess = currguess;
                  }
            }
      }
}