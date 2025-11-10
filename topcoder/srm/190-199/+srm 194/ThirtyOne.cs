using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ThirtyOne {
        public int findWinner(string[] hands) {
            var winner = 0;
            for (var i = 1; i < hands.Length; ++i)
                if (eval(hands[i]) > eval(hands[winner]))
                    winner = i;
            return winner;
        }

        private static int eval(string hand) {
            var cards = hand.Split(' ').OrderBy(x => {
                if ("2345678910JQK".IndexOf(x) >= 0)
                    return -1;
                return +1;
            });
            var distinct = cards.Distinct();
            if (distinct.Count() == 1) {
                return 61;
            }
            var result = 0;
            foreach (var card in cards) {
                if ("2345678910JQK".IndexOf(card) >= 0) {
                    if ("10JQK".IndexOf(card) < 0)
                        result += "23456789".IndexOf(card) + 2;
                    else
                        result += 10;
                    continue;
                }
                if (result + 11 <= 31)
                    result += 11;
                else
                    result += 1;
            }
            return 2 * result;
        }
    }
}