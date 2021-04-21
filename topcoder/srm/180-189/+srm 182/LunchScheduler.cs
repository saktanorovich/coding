using System;

namespace TopCoder.Algorithm {
    public class LunchScheduler {
        public int getOverlap(string[] hands) {
            var result = hands.Length;
            permute(0, new int[hands.Length], order => {
                result = Math.Min(result, getOverlap(hands, order));
            });
            return result;
        }

        private static void permute(int position, int[] order, Action<int[]> process) {
            if (position == order.Length) {
                process(order);
            }
            else {
                for (var member = 0; member < order.Length; ++member) {
                    if (order[member] == 0) {
                        order[member] = position + 1;
                        permute(position + 1, order, process);
                        order[member] = 0;
                    }
                }
            }
        }

        private static int getOverlap(string[] hands, int[] order) {
            var into = new int[hands.Length];
            var outo = new int[hands.Length];
            for (var time = 0; time < hands.Length; ++time) {
                ++into[time];
                ++outo[time];
                for (var left = hands.Length - 1; left > time; --left) {
                    if (hands[order[time] - 1][order[left] - 1] == '1') {
                        --outo[time];
                        ++outo[left];
                        break;
                    }
                }
            }
            var result = 0;
            for (int time = 0, attime = 0; time < hands.Length; ++time) {
                attime += into[time];
                result = Math.Max(result, attime);
                attime -= outo[time];
            }
            return result;
        }
    }
}