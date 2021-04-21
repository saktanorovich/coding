using System;

namespace TopCoder.Algorithm {
    public class Ambigram {
        public string ambiword(string word) {
            for (var i = 0; i < word.Length; ++i) {
                for (var j = 0; j < word.Length; ++j) {
                    memo[i, j, 0] = new Res(string.Empty, int.MaxValue);
                    memo[i, j, 1] = new Res(string.Empty, int.MaxValue);
                    if (j < i) {
                        memo[i, j, 0].Cost = 0;
                        memo[i, j, 1].Cost = 0;
                    }
                }
                relax(ref memo[i, i, 1], new Res(string.Empty, distance(word[i])));
                foreach (var c in q) {
                    relax(ref memo[i, i, 0], new Res(c.ToString(), distance(word[i], c)));
                    relax(ref memo[i, i, 1], new Res(c.ToString(), distance(word[i], c)));
                }
            }
            return ambiword(word, 0, word.Length - 1, 0).Word;
        }

        private Res ambiword(string word, int a, int b, int e) {
            if (memo[a, b, e].Cost < int.MaxValue) {
                return memo[a, b, e];
            }
            relax(ref memo[a, b, e], ambiword(word, a + 1, b, 0), distance(word[a]));
            relax(ref memo[a, b, e], ambiword(word, a, b - 1, 0), distance(word[b]));
            for (var i = 0; i < p.Length; ++i) {
                relax(ref memo[a, b, e], ambiword(word, a + 1, b - 1, 1), distance(word[a], p[i]) + distance(word[b], d[i]), p[i], d[i]);
            }
            return memo[a, b, e];
        }

        private static void relax(ref Res result, Res comparand) {
            if (result.CompareTo(comparand) > 0) {
                result.Word = comparand.Word;
                result.Cost = comparand.Cost;
            }
        }

        private static void relax(ref Res result, Res comparand, int add) {
            relax(ref result, new Res(comparand.Word, comparand.Cost + add));
        }

        private static void relax(ref Res result, Res comparand, int add, char le, char ri) {
            relax(ref result, new Res(le + comparand.Word + ri, comparand.Cost + add));
        }

        private static int distance(char a, char b) {
            if (a < b) {
                return b - a;
            }
            return a - b;
        }

        private static int distance(char a) {
            return Math.Min(a - 'A' + 1, 'Z' - a + 1);
        }

        private readonly Res[,,] memo = new Res[50, 50, 2];

        private const string p = "HINOSXZMW";
        private const string d = "HINOSXZWM";
        private const string q = "HINOSXZ";

        private struct Res : IComparable<Res> {
            public string Word;
            public int Cost;

            public Res(string word, int cost) {
                Word = word;
                Cost = cost;
            }

            public int CompareTo(Res other) {
                if (Cost != other.Cost) {
                    return Cost.CompareTo(other.Cost);
                }
                if (Word.Length != other.Word.Length) {
                    return other.Word.Length.CompareTo(Word.Length);
                }
                return Word.CompareTo(other.Word);
            }
        }
    }
}