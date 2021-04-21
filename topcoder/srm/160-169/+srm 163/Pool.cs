using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class Pool {
        public int rackMoves(int[] triangle) {
            int result = int.MaxValue;
            for (int i = 0; i < config.Length; ++i) {
                result = Math.Min(result, rackMoves(compress(triangle), config[i]));
            }
            return result;
        }

        private int rackMoves(int[] triangle, int[] goal) {
            if (triangle[4] != 2) {
                for (int i = 0; i < 15; ++i) {
                    if (triangle[i] == 2) {
                        int temp = triangle[i];
                        triangle[i] = triangle[4];
                        triangle[4] = temp;
                        break;
                    }
                }
                return 1 + rackMoves(triangle, goal);
            }
            int incorrect = 0;
            for (int i = 0; i < 15; ++i) {
                if (triangle[i] != goal[i]) {
                    incorrect = incorrect + 1;
                }
            }
            return incorrect / 2;
        }

        private int[] compress(int[] triangle) {
            int[] result = new int[15];
            for (int i = 0; i < 15; ++i) {
                result[i] = compress(triangle[i]);
            }
            return result;
        }

        private int compress(int ball) {
            if (ball < 8) return 0;
            if (ball > 8) return 1;
            return 2;
        }

        private static readonly int[][] config = new int[2][] {
            new int[] { 1, 0, 0, 1, 2, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0 },
            new int[] { 0, 1, 1, 0, 2, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1 },
        };
    }
}