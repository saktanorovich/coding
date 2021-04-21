using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ChipRace {
        public double[] chances(int[] chips) {
            var prob = new double[chips.Length];
            chances(chips, prob, new bool[chips.Length], (int)Math.Round(chips.Sum() / 5.0), chips.Sum(), 1.0);
            return prob;
        }

        private void chances(int[] chips, double[] prob, bool[] used, int awards, int total, double lastProb) {
            if (awards > 0) {
                for (var i = 0; i < chips.Length; ++i) {
                    if (!used[i]) {
                        used[i] = true;
                        prob[i] += lastProb * chips[i] / total;
                        chances(chips, prob, used, awards - 1, total - chips[i], lastProb * chips[i] / total);
                        used[i] = false;
                    }
                }
            }
        }
    }
}