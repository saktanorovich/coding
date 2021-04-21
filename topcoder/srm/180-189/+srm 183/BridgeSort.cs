using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class BridgeSort {
        public string sortedHand(string hand) {
            var cards = new List<string>();
            for (var i = 0; i < hand.Length; i += 2) {
                cards.Add(hand.Substring(i, 2));
            }
            cards.Sort((a, b) => {
                if (suit.IndexOf(a[0]) != suit.IndexOf(b[0])) {
                    return suit.IndexOf(a[0]) - suit.IndexOf(b[0]);
                }
                return value.IndexOf(a[1]) - value.IndexOf(b[1]);
            });
            return string.Concat(cards);
        }

        private const string suit = "CDHS";
        private const string value = "23456789TJQKA";
    }
}
