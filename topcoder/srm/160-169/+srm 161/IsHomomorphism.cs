using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class IsHomomorphism {
        public string[] numBad(string[] source, string[] target, int[] mapping) {
            return numBad(convert(source), convert(target), mapping);
        }

        private string[] numBad(int[][] source, int[][] target, int[] mapping) {
            List<string> result = new List<string>();
            for (int a = 0; a < source.Length; ++a) {
                for (int b = 0; b < source.Length; ++b) {
                    if (mapping[source[a][b]] != target[mapping[a]][mapping[b]]) {
                        result.Add(string.Format("({0},{1})", a, b));
                    }
                }
            }
            return result.ToArray();
        }

        private int[][] convert(string[] source) {
            return Array.ConvertAll(source, delegate(string s) {
                return Array.ConvertAll(s.ToCharArray(),
                    delegate(char c) {
                        return c - '0';
                    });
            });
        }
    }
}