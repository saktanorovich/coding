using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class Soma {
        public int letMeCountTheWays(string[] pattern) {
            for (var i = 0; i < pieces.Length; ++i) {
                blocks[i] = BlockUtils.rotate(BlockUtils.make(parse(pieces[i])));
            }
            return letMeCountTheWays(BlockUtils.make(parse(pattern)));
        }

        private int letMeCountTheWays(int[][] pattern) {
            Array.Sort(pattern, BlockComparer.Comparison);
            foreach (var c in pattern) {
                space[c[0], c[1], c[2]] = 1;
            }
            letMeCountTheWays(pattern, pattern.Length, 0);
            return numOfWays;
        }

        private void letMeCountTheWays(int[][] pattern, int volume, int numOfBlocks) {
            if (numOfBlocks == blocks.Length) {
                if (volume == 0)
                    ++numOfWays;
                return;
            }
            foreach (var c in pattern) {
                var x = c[0];
                var y = c[1];
                var z = c[2];
                if (space[x, y, z] == 1) {
                    for (var i = 0; i < blocks.Length; ++i) {
                        if (taken[i] == 0) {
                            foreach (var block in blocks[i]) {
                                if (free(x, y, z, block)) {
                                    taken[i] = 1;
                                    mark(x, y, z, block, -1);
                                    letMeCountTheWays(pattern, volume - block.Length, numOfBlocks + 1);
                                    mark(x, y, z, block, +1);
                                    taken[i] = 0;
                                }
                            }
                        }
                    }
                    return;
                }
            }
        }

        private bool free(int x, int y, int z, int[][] block) {
            foreach (var c in block) {
                if (c[0] + x >= 0 && c[1] + y >= 0 && c[2] + z >= 0) {
                    if (space[c[0] + x, c[1] + y, c[2] + z] != 1)
                        return false;
                }
                else return false;
            }
            return true;
        }

        private void mark(int x, int y, int z, int[][] block, int inc) {
            foreach (var c in block) {
                space[c[0] + x, c[1] + y, c[2] + z] += inc;
            }
        }

        private readonly List<int[][]>[] blocks = new List<int[][]>[pieces.Length];
        private readonly int[] taken = new int[pieces.Length];
        private readonly int[,,] space = new int[30, 30, 30];
        private int numOfWays = 0;

        private static int[][] parse(string[] pattern) {
            return Array.ConvertAll(pattern, item =>
                Array.ConvertAll(item.ToCharArray(), c => int.Parse(c.ToString()))
            );
        }

        private static readonly string[][] pieces = {
            new[] { "111", "100" },
            new[] { "111", "010" },
            new[] { "011", "110" },
            new[] { "11", "10" },
            new[] { "02", "11" },
            new[] { "20", "11" },
            new[] { "12", "01" },
        };

        private static class BlockUtils {
            public static List<int[][]> rotate(int[][] block) {
                var result = new List<int[][]>();
                for (var x = 0; x < 4; ++x) {
                    for (var y = 0; y < 4; ++y) {
                        for (var z = 0; z < 4; ++z) {
                            result.Add(block);
                            block = rotate(block, 2);
                        }
                        block = rotate(block, 1);
                    }
                    block = rotate(block, 0);
                }
                foreach (var res in result) {
                    Array.Sort(res, BlockComparer.Comparison);
                }
                foreach (var res in result) {
                   for (var k = res.Length - 1; k >= 0; --k) {
                       res[k][0] += -res[0][0];
                       res[k][1] += -res[0][1];
                       res[k][2] += -res[0][2];
                   }
                }
                var comparer = new BlockComparer();
                result.Sort(comparer);
                var distinct = new List<int[][]> { result[0] };
                for (int current = 1, last = 0; current < result.Count; ++current) {
                    if (comparer.Compare(result[current], result[last]) != 0) {
                        distinct.Add(result[current]);
                        last = current;
                    }
                }
                return distinct;
            }

            public static int[][] make(int[][] pattern) {
                var result = new List<int[]>();
                for (var x = 0; x < pattern.Length; ++x) {
                    for (var y = 0; y < pattern[x].Length; ++y) {
                        for (var z = 0; z < pattern[x][y]; ++z) {
                            result.Add(new[] { x, y, z });
                        }
                    }
                }
                return result.ToArray();
            }

            private static int[][] rotate(int[][] block, int axis) {
                var result = new int[block.Length][];
                for (var i = 0; i < block.Length; ++i) {
                    result[i] = mul(block[i], rot[axis]);
                }
                return result;
            }

            private static int[] mul(int[] a, int[,] b) {
                var result = new int[a.Length];
                for (var i = 0; i < a.Length; ++i) {
                    for (var j = 0; j < a.Length; ++j) {
                        result[i] += a[j] * b[j, i];
                    }
                }
                return result;
            }

            private static readonly int[][,] rot = {
                new[,] { { +1, 0, 0 }, { 0, 0, +1 }, { 0, -1, 0 } },
                new[,] { { 0, 0, -1 }, { 0, +1, 0 }, { +1, 0, 0 } },
                new[,] { { 0, +1, 0 }, { -1, 0, 0 }, { 0, 0, +1 } }
            };
        }

        private class BlockComparer : IComparer<int[][]> {
            public int Compare(int[][] a, int[][] b) {
                for (var i = 0; i < a.Length; ++i) {
                    var result = Comparison(a[i], b[i]);
                    if (result != 0) {
                        return result;
                    }
                }
                return 0;
            }

            public static readonly Comparison<int[]> Comparison =
                delegate(int[] a, int[] b) {
                    for (var i = 0; i < 3; ++i) {
                        if (a[i] != b[i]) {
                            return a[i].CompareTo(b[i]);
                        }
                    }
                    return 0;
                };
        }
    }
}