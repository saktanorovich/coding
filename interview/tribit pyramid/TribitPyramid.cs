using System;
using System.IO;
using System.Text;

namespace coding.interview {
    public class Program {
        internal static void Main(string[] args) {
            try {
                if (args.Length == 1) {
                    Process(args[0]);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Unhandled exception is {0}", ex.Message + ex.StackTrace);
            }
        }

        private static void Process(string s) {
            var tribitPyramid = s.ToTribitPyramid();
            if (tribitPyramid != null) {
                new TribitPyramidTransformer(Console.Out).Transform(tribitPyramid);
            }
        }
    }

    public class TribitPyramidTransformer {
        private readonly TextWriter writer;

        public TribitPyramidTransformer(TextWriter writer) {
            this.writer = writer;
        }

        public void Transform(TribitPyramid tribitPyramid) {
            while (tribitPyramid.Count > 1) {
                writer.WriteLine(tribitPyramid);
                tribitPyramid = TransformImpl(tribitPyramid);
            }
            writer.WriteLine(tribitPyramid);
        }

        private TribitPyramid TransformImpl(TribitPyramid tribitPyramid) {
            var indices = tribitPyramid.GetPyramidIndices();
            var bitscnt = new[] { 0, 0 };
            for (var i = 0; i < indices.Length; ++i) {
                var triangle = tribitPyramid[indices[i]];
                var tribitPyramidRule = TribitPyramidRule.GetBy(triangle);
                if (tribitPyramidRule != null) {
                    tribitPyramid[indices[i]] = tribitPyramidRule.To;
                }
                else {
                    bitscnt[triangle[0]]++;
                }
            }
            if (bitscnt[0] + bitscnt[1] == indices.Length) {
                if (bitscnt[0] == 0) return new TribitPyramid(1);
                if (bitscnt[1] == 0) return new TribitPyramid(0);

                var bits = new byte[tribitPyramid.Count >> 2];
                for (var i = 0; i < bits.Length; ++i) {
                    bits[i] = tribitPyramid[indices[i]][0];
                }
                return new TribitPyramid(bits);
            }
            return tribitPyramid;
        }
    }

    public class TribitPyramid {
        private readonly byte[] bits;

        public TribitPyramid(byte bit) {
            this.bits = new[] { bit };
        }

        public TribitPyramid(byte[] bits) {
            this.bits = new byte[ToPow4(bits.Length)];
            Array.Copy(bits, this.bits, bits.Length);
        }

        public byte[] this[int[] indices] {
            get { return GetByIndices(indices); }
            set { SetByIndices(indices, value); }
        }

        public int Count {
            get { return bits.Length; }
        }

        public int[][] GetPyramidIndices() {
            var numOfRows = (int)Math.Sqrt(bits.Length);
            var pyramid = new int[numOfRows][];
            for (int bitsPos = 0, row = 0, capacity = 1; row < numOfRows; ++row, capacity += 2) {
                pyramid[row] = new int[capacity];
                for (var col = 0; col < capacity; ++col, ++bitsPos) {
                    pyramid[row][col] = bitsPos;
                }
            }
            var result = new int[numOfRows * numOfRows >> 2][];
            for (int row = 0, i = 0; row + 1 < numOfRows; row += 2) {
                for (int iCol = 0, jCol = 0, take = 1; iCol < pyramid[row].Length; ++i) {
                    var res = new int[4];
                    switch (take) {
                        case 1:
                            res[0] = pyramid[row][iCol];
                            res[1] = pyramid[row + 1][jCol + 0];
                            res[2] = pyramid[row + 1][jCol + 1];
                            res[3] = pyramid[row + 1][jCol + 2];
                            iCol += 1;
                            jCol += 3;
                            break;
                        case 3:
                            res[0] = pyramid[row + 1][jCol];
                            res[1] = pyramid[row][iCol + 0];
                            res[2] = pyramid[row][iCol + 1];
                            res[3] = pyramid[row][iCol + 2];
                            iCol += 3;
                            jCol += 1;
                            break;
                    }
                    result[i] = res;
                    take = 4 - take;
                }
            }
            return result;
        }

        public override string ToString() {
            var result = new StringBuilder();
            for (var i = bits.Length - 1; i >= 0; --i) {
                result.Append(bits[i]);
            }
            return result.ToString();
        }

        private byte[] GetByIndices(int[] indices) {
            var result = new byte[indices.Length];
            for (var i = 0; i < indices.Length; ++i) {
                result[i] = bits[indices[i]];
            }
            return result;
        }

        private void SetByIndices(int[] indices, byte[] value) {
            for (var i = 0; i < indices.Length; ++i) {
                bits[indices[i]] = value[i];
            }
        }

        private static int ToPow4(int length) {
            var result = 1;
            while (result < length) {
                result <<= 2;
            }
            return result;
        }
    }

    public class TribitPyramidRule {
        public readonly byte[] From;
        public readonly byte[] To;

        private TribitPyramidRule(byte[] from, byte[] to) {
            From = from;
            To = to;
        }

        public static TribitPyramidRule GetBy(byte[] from) {
            var result = 0;
            result = (result << 1) | from[3];
            result = (result << 1) | from[2];
            result = (result << 1) | from[1];
            result = (result << 1) | from[0];
            return rules[result];
        }

        private static readonly TribitPyramidRule[] rules = {
            null, //Parse("0000", "0000"),
            Parse("0001", "1000"),
            Parse("0010", "0001"),
            Parse("0011", "0010"),
            Parse("0100", "0000"),
            Parse("0101", "0010"),
            Parse("0110", "1011"),
            Parse("0111", "1011"),
            Parse("1000", "0100"),
            Parse("1001", "0101"),
            Parse("1010", "0111"),
            Parse("1011", "1111"),
            Parse("1100", "1101"),
            Parse("1101", "1110"),
            Parse("1110", "0111"),
            null, //Parse("1111", "1111")
        };

        private static TribitPyramidRule Parse(string from, string to) {
            return new TribitPyramidRule(from.ToBitArray(), to.ToBitArray());
        }
    }

    public static class TribitPyramidParser {
        public static TribitPyramid ToTribitPyramid(this string s) {
            var bits = s.ToBitArray();
            if (bits != null) {
                return new TribitPyramid(bits);
            }
            return null;
        }

        public static byte[] ToBitArray(this string s) {
            if (!string.IsNullOrEmpty(s)) {
                var bits = new byte[s.Length];
                for (var i = 0; i < s.Length; ++i) {
                    if ('0' <= s[i] && s[i] <= '1') {
                        bits[s.Length - 1 - i] = (byte)(s[i] - '0');
                    }
                    else return null;
                }
                return bits;
            }
            return null;
        }
    }
}
