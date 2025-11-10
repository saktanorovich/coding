using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ShufflingMachine {
            public double stackDeck(int[] shuffle, int maxShuffles, int[] cardsReceived, int k) {
                  int numOfCards = shuffle.Length;
                  int[] deck = new int[numOfCards];
                  for (int card = 0; card < numOfCards; ++card) {
                        deck[card] = card;
                  }
                  int[] frequency = new int[numOfCards];
                  for (int numOfShuffles = 1; numOfShuffles <= maxShuffles; ++numOfShuffles) {
                        int[] shuffled = new int[numOfCards];
                        for (int i = 0; i < numOfCards; ++i) {
                              shuffled[shuffle[i]] = deck[i];
                        }
                        deck = shuffled;
                        foreach (int position in cardsReceived) {
                              ++frequency[deck[position]];
                        }
                  }
                  Array.Sort(frequency);
                  double result = 0.0;
                  for (int i = 0; i < k; ++i) {
                        result += frequency[numOfCards - 1 - i];
                  }
                  return result / maxShuffles;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new ShufflingMachine().stackDeck(new int[] { 1, 0 }, 3, new int[] { 0 }, 1));
                  Console.WriteLine(new ShufflingMachine().stackDeck(new int[] { 1, 2, 0}, 5, new int[] { 0 }, 2));
                  Console.WriteLine(new ShufflingMachine().stackDeck(new int[] { 1, 2, 0, 4, 3}, 7, new int[] { 0, 3 }, 2));
                  Console.WriteLine(new ShufflingMachine().stackDeck(new int[] { 0, 4, 3, 5, 2, 6, 1}, 19, new int[] { 1, 3, 5 }, 2));
                  Console.WriteLine(new ShufflingMachine().stackDeck(new int[] { 3, 4, 7, 2, 8, 5, 6, 1, 0, 9}, 47, new int[] { 6, 3, 5, 2, 8, 7, 4 }, 8));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadKey();
            }
      }
}