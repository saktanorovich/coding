using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class Rochambo {
        public int wins(string opponent) {
            string player = "RR";
            for (int i = 2; i < opponent.Length; ++i) {
                char move = opponent[i - 1];
                if (opponent[i - 1] != opponent[i - 2]) {
                    move = (char)('P' + 'R' + 'S' - opponent[i - 1] - opponent[i - 2]);
                }
                player += beatBy[move];
            }
            return beat(player, opponent);
        }

        private int beat(string player, string opponent) {
            int result = 0;
            for (int i = 0; i < player.Length; ++i) {
                if (player[i] == beatBy[opponent[i]]) {
                    result = result + 1;
                }
            }
            return result;
        }

        private readonly IDictionary<char, char> beatBy =
            new Dictionary<char, char>() {
                { 'R', 'P' },
                { 'P', 'S' },
                { 'S', 'R' },
            };
    }
}