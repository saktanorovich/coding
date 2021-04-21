/*
Consider an n-element set U={1,2,..,n}. Each subset S from U is assigned a value val(S).
Initially val(S)=0 for all S from U.

We have three types of queries:

1) 1 x S: Given an integer x and set S assign a value x for all subsets of S (i.e. set val(T)=x for all T from S).
2) 2 x S: Given an integer x and set S XOR all values in the subset of S with x (i.e. set val(T)=val(T)XORx for all T from S).
3) 3 S: Given a set S find and print val(S) on a new line.

n <= 16
m <= 100000
x <= 2^30-1

*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace interview.hackerrank {
    public class SubmaskQueries {
        public bool process(int testCase, TextReader reader, TextWriter writer) {
            var buf = reader.ReadLine().Split(' ');
            var n = int.Parse(buf[0]);
            var m = int.Parse(buf[1]);
            setVal = new int[256, 256];
            setIdx = new int[256, 256];
            xorVal = new List<int>[256, 256];
            xorIdx = new List<int>[256, 256];
            for (var hi = 0; hi < 256; ++hi) {
                for (var lo = 0; lo < 256; ++lo) {
                    setIdx[hi, lo] = -1;
                    xorVal[hi, lo] = new List<int> { +0 };
                    xorIdx[hi, lo] = new List<int> { -2 };
                }
            }
            for (var i = 0; i < m; ++i) {
                buf = reader.ReadLine().Split(' ');
                var op = int.Parse(buf[0]);
                switch (op) {
                    case 1:
                        set(parse(buf[2], n), int.Parse(buf[1]), i);
                        break;
                    case 2:
                        xor(parse(buf[2], n), int.Parse(buf[1]), i);
                        break;
                    case 3:
                        writer.WriteLine(get(parse(buf[1], n)));
                        break;
                }
            }
            return false;
        }

        private void set(int set, int val, int idx) {
            var lo = set & 0xff;
            var hi = set >> 8;
            foreach (var sub in subsets(lo)) {
                setVal[hi, sub] = val;
                setIdx[hi, sub] = idx;
            }
        }

        private void xor(int set, int val, int idx) {
            var lo = set & 0xff;
            var hi = set >> 8;
            foreach (var sub in subsets(lo)) {
                var have = xorVal[hi, sub].Count;
                var last = xorVal[hi, sub][have - 1];
                xorVal[hi, sub].Add(val ^ last);
                xorIdx[hi, sub].Add(idx);
            }
        }

        private int get(int set) {
            var lo = set & 0xff;
            var hi = set >> 8;
            var uu = ~hi & 0xff;

            var lastSetIdx = -1;
            var lastSetVal = 0;
            var lastXorVal = 0;

            // scan set ops
            foreach (var sub in subsets(uu)) {
                var sup = sub | hi;
                if (lastSetIdx < setIdx[sup, lo]) {
                    lastSetIdx = setIdx[sup, lo];
                    lastSetVal = setVal[sup, lo];
                }
            }

            // scan xor ops
            foreach (var sub in subsets(uu)) {
                var sup = sub | hi;
                var takeAt = ~xorIdx[sup, lo].BinarySearch(lastSetIdx);
                lastXorVal ^= xorVal[sup, lo][takeAt - 1];
                lastXorVal ^= xorVal[sup, lo][xorVal[sup, lo].Count - 1];
            }

            return lastSetVal ^ lastXorVal;
        }

        private static IEnumerable<int> subsets(int set) {
            for (var sub = set; sub > 0; sub = (sub - 1) & set) {
                yield return sub;
            }
            yield return 0;
        }

        private static int parse(string s, int n) {
            var t = 0;
            for (var i = 0; i < n; ++i) {
                if (s[i] == '1') {
                    t |= 1 << i;
                }
            }
            return t;
        }

        private List<int>[,] xorVal;
        private List<int>[,] xorIdx;
        private int[,] setVal;
        private int[,] setIdx;
    }
}
