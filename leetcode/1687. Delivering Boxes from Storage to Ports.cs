using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1687 {
        public int BoxDelivering(int[][] boxes, int portsCount, int maxBoxes, int maxWeight) {
            if (boxes.Length < 10) {
                return small(boxes, maxBoxes, maxWeight);
            } else {
                return large(boxes, maxBoxes, maxWeight);
            }
        }

        private int small(int[][] boxes, int maxBoxes, int maxWeight) {
            var trip = new int[boxes.Length + 1];
            for (var i = 1; i < boxes.Length; ++i) {
                trip[i + 1] = trip[i];
                if (boxes[i][0] != boxes[i - 1][0]) {
                    trip[i + 1]++;
                }
            }
            var best = new int[boxes.Length + 1];
            for (var i = 1; i <= boxes.Length; ++i) {
                System.Console.WriteLine("i=" + i);
                best[i] = int.MaxValue;
                var weight = 0;
                for (var j = i; j > 0 && i - j < maxBoxes; --j) {
                    weight += boxes[j - 1][1];
                    if (weight <= maxWeight) {
                        if (best[i] > best[j - 1] + 2 + trip[i] - trip[j]) {
                            best[i] = best[j - 1] + 2 + trip[i] - trip[j];
                            System.Console.WriteLine("  j=" + j);
                        }
                    } else break;
                }
            }
            for (var i = 0; i <= boxes.Length; ++i) {
                System.Console.Write(boxes[i] + " ");
            }
            return best[boxes.Length];
        }

        private int large(int[][] boxes, int maxBoxes, int maxWeight) {
            return -1;
        }
    }
}
