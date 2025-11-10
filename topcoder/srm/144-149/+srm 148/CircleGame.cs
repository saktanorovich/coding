using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class CircleGame {
            public int cardsLeft(string deck) {
                  deck = deck.Replace("K", string.Empty);
                  for (bool activity = true; activity && deck.Length > 0; ) {
                        activity = false;
                        for (int i = 0; i + 1 < deck.Length; ) {
                              if (badPair(deck[i], deck[i + 1])) {
                                    deck = deck.Remove(i, 2);
                                    activity = true;
                                    continue;
                              }
                              i = i + 1;
                        }
                        if (deck.Length > 0) {
                              if (badPair(deck[0], deck[deck.Length - 1])) {
                                    deck = deck.Remove(0, 1);
                                    deck = deck.Remove(deck.Length - 1, 1);
                                    activity = true;
                              }
                        }
                  }
                  return deck.Length;
            }

            private bool badPair(char a, char b) {
                  return alphabet.IndexOf(a) + alphabet.IndexOf(b) == 11;
            }

            private static readonly string alphabet = "A23456789TJQK";
      }
}