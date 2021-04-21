/**
Find the rank of the given word when it is arranged alphabetically.
For example, rank of the string bacd is 7, alphabetical arrangement
of word is 
  abcd
  abdc
  acbd
  acdb
  adbc
  adcb
  bacd
hence rank of the given string "bacd" is 7,

Input: The first line consist of T testcases. The next T line
consist of string s.

Output: Output contains the single line of the rank.

Constraints
  1 ≤ T ≤ 10
  1 ≤ length of string ≤ 100

Sample Input
2
abdfce
abfcde

Sample Output
11
19

Explanation
 1 abcdef
 2 abcdfe
 3 abcedf
 4 abcefd
 5 abcfde
 6 abcfed
 7 abdcef
 8 abdcfe
 9 abdecf
10 abdefc
11 abdfce
12 abdfec
13 abecdf
14 abecfd
15 abedcf
16 abedfc
17 abefcd
18 abefdc
19 abfcde
20 abfced
21 abfdce
22 abfdec
23 abfecd
24 abfedc
25 acbdef
*/
using System;

namespace interview.hackerrank {
    public class RankOfAWord {
        private readonly long MOD = 1000000007;
        private readonly long[] factorial = new long[100 + 1];

        public int[] rank(int n, string[] words) {
            factorial[0] = 1;
            for (var i = 1; i < factorial.Length; ++i) {
                factorial[i] = (i * factorial[i - 1]) % MOD;
            }
            var result = new int[n];
            for (var w = 0; w < n; ++w) {
                result[w] = (int)rank(Array.ConvertAll(words[w].ToCharArray(), c => c - 'a'));
            }
            return result;
        }

        private long rank(int[] word) {
            var occurence = new int[26];
            for (var i = 0; i < word.Length; ++i) {
                ++occurence[word[i]];
            }
            var result = 1L;
            for (var i = 0; i < word.Length; ++i) {
                var permutations = 0L;
                for (var j = 0; j < word[i]; ++j) {
                    if (occurence[j] > 0) {
                        --occurence[j];
                        permutations += permute(word.Length - i - 1, occurence);
                        permutations %= MOD;
                        ++occurence[j];
                    }
                }
                --occurence[word[i]];
                result += permutations;
                result %= MOD;
            }
            return (result - 1 + MOD) % MOD;
        }

        private long permute(int total, int[] occurence) {
            var result = 1L;
            for (var i = 0; i < occurence.Length; ++i) {
                if (occurence[i] > 1) {
                    result *= factorial[occurence[i]];
                    result %= MOD;
                }
            }
            return (factorial[total] * inv(result)) % MOD;
        }

        private long pow(long x, long k) {
            if (k == 0) {
                return 1;
            } else if (k % 2 == 0) {
                return pow((x * x) % MOD, k / 2);
            } else {
                return (x * pow(x, k - 1)) % MOD;
            }
        }

        private long inv(long x) {
            return pow(x, MOD - 2);
        }
    }
}
