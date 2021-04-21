using System;

namespace FlippingBitsDiv1 {
      public class FlippingBitsDiv1 {
            public int getmin(string[] s, int m) {
                  string concatenated = string.Empty;
                  for (int i = 0; i < s.Length; ++i) {
                        concatenated += s[i];
                  }
                  return getmin(concatenated, concatenated.Length, m);
            }

            /* The prefix of the length m will be repeated from the begin to the end of the sequence. */
            private int getmin(string s, int n, int m) {
                  int result = int.MaxValue;
                  int groupsCount = n / m;
                  if (groupsCount < m) {
                        /* A simple bruteforce solution for groups flipping. */
                        int[] bits = new int[n];
                        for (int set = 0; set < (1 << groupsCount); ++set) {
                              for (int bit = 0; bit < n; ++bit) {
                                    bits[bit] = s[bit] - '0';
                              }
                              int flipsCount = 0;
                              int groupFlipped = 0;
                              for (int group = 0; group < groupsCount; ++group) {
                                    if ((set & (1 << group)) != 0) {
                                          ++flipsCount;
                                          groupFlipped ^= (1 << (group + 1)) - 1;
                                    }
                              }
                              for (int group = 0; group < groupsCount; ++group) {
                                    if ((groupFlipped & (1 << group)) != 0) {
                                          for (int j = 0; j < m; ++j) {
                                                bits[group * m + j] ^= 1;
                                          }
                                    }
                              }
                              result = Math.Min(result, flipsCount + minOperationsByFlippingOneBit(bits, n, m));
                        }
                  }
                  else {
                        /* A simple bruteforce of sequence's prefix. */
                        int[] bitsToFlip = new int[n];
                        for (int prefix = 0; prefix < (1 << m); ++prefix) {
                              for (int bit = 0; bit < n; ++bit) {
                                    bitsToFlip[bit] = s[bit] - '0';
                                    if ((prefix & (1 << (bit % m))) != 0) {
                                          bitsToFlip[bit] ^= 1;
                                    }
                              }
                              /* Find the minimum number of flips required to make bitsToFlip equals to zero.
                               * Performing these operations once again will result to the desired sequence. */
                              int flipsCount = 0;
                              for (int bit = groupsCount * m; bit < n; ++bit) {
                                    flipsCount += bitsToFlip[bit];
                              }
                              int[][] bitsCount = new int[groupsCount][];
                              for (int group = 0; group < groupsCount; ++group) {
                                    bitsCount[group] = new int[2];
                                    for (int bit = 0; bit < m; ++bit) {
                                          ++bitsCount[group][bitsToFlip[group * m + bit]];
                                    }
                              }
                              /* dp[g, bit] is min operations required to set 1..g groups to bit. */
                              int[,] dp = new int[groupsCount + 1, 2];
                              for (int group = 1; group <= groupsCount; ++group) {
                                    for (int currBit = 0; currBit < 2; ++currBit) {
                                          dp[group, currBit] = int.MaxValue;
                                          for (int prevBit = 0; prevBit < 2; ++prevBit) {
                                                dp[group, currBit] = Math.Min(dp[group, currBit],
                                                      dp[group - 1, prevBit] + bitsCount[group - 1][1 - currBit] + (currBit ^ prevBit));
                                          }
                                    }
                              }
                              result = Math.Min(result, flipsCount + Math.Min(dp[groupsCount, 0], dp[groupsCount, 1] + 1));
                        }
                  }
                  return result;
            }

            private int minOperationsByFlippingOneBit(int[] bits, int n, int m) {
                  int result = 0;
                   if (n - m + n - m <= n) {
                        for (int i = 1; i <= n - m; ++i) {
                              if (bits[i - 1] != bits[m + i - 1]) {
                                    ++result;
                              }
                        }
                        return result;
                  }
                  for (int bit = 0; bit < m; ++bit) {
                        int[] flipsCount = new int[2];
                        for (int k = 0; bit + k * m < n; ++k) {
                              ++flipsCount[1 - bits[bit + k * m]];
                        }
                        result += Math.Min(flipsCount[0], flipsCount[1]);
                  }
                  return result;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "00111000" }, 1));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "101100001101" }, 3));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "11111111" }, 4));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "1101001000" }, 8));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "1", "10", "11", "100", "101", "110" }, 5));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] { "1001011000101010001111111110111011011100110001001" }, 2));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] {
                        "01100010010101100000000110001010111001001000111100",
                        "10010001100101100110100001110010111110000010000111",
                        "01110111001010111000111101001001111101010110100101",
                        "00110011101110001000111001011011110101111110111111",
                        "10011101010001001000101011110010111000100001100000",
                        "10110001110001011010101101101111101001000101011001" }, 18));
                  Console.WriteLine(new FlippingBitsDiv1().getmin(new string[] {
                        "11100011100001101101101100111111010010010000110101",
                        "10100110010000100011100010100000000110110100110010",
                        "11110000000100110011000010001001111111001001111110",
                        "01001011100110101100011001010001001011100001101000",
                        "01101111000110110001010101101111011000010111011001",
                        "11010110111011000000101110110011010001100100000110" }, 8));
                  Console.ReadLine();
            }
      }
}
