using System;

namespace TopCoder.Algorithm {
    public class Poetry {
        public string rhymeScheme(string[] poem) {
            var result = new char[poem.Length];
            for (int i = 0, last = 0; i < poem.Length; ++i) {
                if (!string.IsNullOrWhiteSpace(poem[i])) {
                    if (result[i] == 0) {
                        var symbol = Abc[last++];
                        result[i] = symbol;
                        for (var j = i + 1; j < poem.Length; ++j) {
                            if (!string.IsNullOrWhiteSpace(poem[j])) {
                                if (rhyme(poem[i], poem[j])) {
                                    result[j] = symbol;
                                }
                            }
                        }
                    }
                }
                else result[i] = ' ';
            }
            return new string(result);
        }

        private static bool rhyme(string s1, string s2) {
            var word1 = getWord(s1);
            var word2 = getWord(s2);
            var ending1 = endingPattern(word1);
            var ending2 = endingPattern(word2);
            if (ending1.Equals(ending2)) {
                return true;
            }
            return false;
        }

        private static string endingPattern(string word) {
            for (var i = 0; i < word.Length; ++i) {
                if (!vowel(word, i - 1) && vowel(word, i)) {
                    var substring = word.Substring(i);
                    if (vowels(substring).Length == 1) {
                        return substring;
                    }
                }
            }
            return string.Empty;
        }

        private static string[] vowels(string s) {
            if (s.EndsWith("y")) {
                s = s.Remove(s.Length - 1, 1);
            }
            return s.ToLower().Split(new[] {
                'b', 'c', 'd', 'f', 'g',
                'h', 'j', 'k', 'l', 'm',
                'n', 'p', 'q', 'r', 's',
                't', 'v', 'w', 'x', 'z' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static bool vowel(string s, int pos) {
            if (pos >= 0) {
                if ("aeiou".IndexOf(s[pos].ToString()) >= 0) {
                    return true;
                }
                if (0 < pos && pos + 1 < s.Length) {
                    return s[pos] == 'y';
                }
            }
            return false;
        }

        private static string getWord(string s) {
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0) {
                return words[words.Length - 1].ToLower();
            }
            return string.Empty;
        }

        private const string Abc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}