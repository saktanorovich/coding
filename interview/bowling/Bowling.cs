using System;
using System.Collections.Generic;
using System.Linq;

namespace coding.interview {
    /// <summary>
    /// Bowling game class.
    /// </summary>
    public class Bowling {
        private readonly List<int> pins = new List<int>();

        /// <summary>
        /// Player rolls a number of pins.
        /// </summary>
        /// <param name="pins">The number of pins knocked down during a roll.</param>
        public void Roll(int pins) {
            if (pins < 0 || pins > 10) {
                throw new Exception("Pins must be in [0, 10]");
            }
            this.pins.Add(pins);
        }

        /// <summary>
        /// Gets end score of the game.
        /// </summary>
        /// <returns>The score of the total game.</returns>
        public int Score() {
            return Score(pins, 0);
        }

        private static int Score(IEnumerable<int> pins, int frame) {
            if (0 <= frame && frame < 10) {
                if (pins.Any()) {
                    if (pins.Take(1).Sum() == 10) return pins.Take(3).Sum() + Score(pins.Skip(1), frame + 1);
                    if (pins.Take(2).Sum() == 10) return pins.Take(3).Sum() + Score(pins.Skip(2), frame + 1);
                    if (pins.Take(2).Sum()  < 10) return pins.Take(2).Sum() + Score(pins.Skip(2), frame + 1);

                    throw new Exception("Too many pins");
                }
            }
            return 0;
        }
    }
}
