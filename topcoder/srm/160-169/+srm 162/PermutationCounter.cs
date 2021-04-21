using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class PermutationCounter {
        public long count(string word) {
            return count(Array.ConvertAll(word.ToCharArray(),
                delegate(char c) {
                    return c - '0';
            }));
        }

        private long count(int[] word) {
            long[,] c = new long[word.Length + 1, word.Length + 1];
            for (int i = 0; i <= word.Length; ++i) {
                c[i, 0] = 1;
                for (int j = 1; j <= i; ++j) {
                    c[i, j] = c[i - 1, j - 1] + c[i - 1, j];
                }
            }
            int[] occurence = new int[10];
            foreach (int digit in word) {
                ++occurence[digit];
            }
            long result = 0;
            for (int i = 0; i < word.Length; ++i) {
                for (int j = 0; j < word[i]; ++j) {
                    if (occurence[j] > 0) {
                        --occurence[j];
                        result += count(word.Length - i - 1, occurence, c);
                        ++occurence[j];
                    }
                }
                --occurence[word[i]];
            }
            return result;
        }

        private long count(int length, int[] occurence, long[,] c) {
            long result = 1;
            for (int digit = 0; digit <= 9; ++digit) {
                result *= c[length, occurence[digit]];
                length -= occurence[digit];
            }
            return result;
        }

        /**
        private long count(int length, int[] occurence) {
            int[] factors = new int[length + 1];
            for (int i = 2; i <= length; ++i) {
                foreach (int x in factorize(i)) {
                    ++factors[x];
                }
            }
            foreach (int i in occurence) {
                if (i > 0) {
                    for (int j = 2; j <= i; ++j) {
                        foreach (int x in factorize(j)) {
                            --factors[x];
                        }
                    }
                }
            }
            long result = 1;
            for (int i = 1; i <= length; ++i) {
                for (int j = 0; j < factors[i]; ++j) {
                    result *= i;
                }
            }
            return result;
        }

        private IEnumerable<int> factorize(int x) {
            List<int> result = new List<int>();
            for (int p = 2; p * p <= x; ++p) {
                while (x % p == 0) {
                    result.Add(p);
                    x /= p;
                }
            }
            if (x > 1) {
                result.Add(x);
            }
            return result;
        }
        /**/
    }
}