using System;

namespace TopCoder.Algorithm {
    public class Foobar {
        public string censor(string plain, string code, string text) {
            return censor(text.ToCharArray(), plain, code, new[] {
                "heck", "gosh", "dang", "shucks", "fooey", "snafu", "fubar"
            });
        }

        private static string censor(char[] text, string plain, string code, string[] forbidden) {
            var result = (char[])text.Clone();
            for (var i = 0; i < text.Length; ++i)
                for (var j = i; j < text.Length; ++j)
                    if (text[i] != ' ' && text[j] != ' ')
                        if (censor(extract(text, i, j), plain, code, forbidden)) {
                            for (var k = i; k <= j; ++k) {
                                result[k] = '*';
                            }
                        }
            return new string(result);
        }

        private static bool censor(string word, string plain, string code, string[] forbidden) {
            foreach (var profanity in forbidden) {
                if (censor(word, plain, code, profanity)) {
                    return true;
                }
            }
            return false;
        }

        private static bool censor(string word, string plain, string code, string profanity) {
            if (word.Length == profanity.Length) {
                for (var i = 0; i < word.Length; ++i) {
                    if (word[i] != profanity[i]) {
                        for (var j = 0; j < code.Length; ++j) {
                            if (code[j] == word[i] && plain[j] == profanity[i]) {
                                goto next;
                            }
                        }
                        return false;
                        next:;
                    }
                }
                return true;
            }
            return false;
        }

        private static string extract(char[] text, int i, int j) {
            var result = string.Empty;
            for (var k = i; k <= j; ++k) {
                if (text[k] != ' ') {
                    result += text[k];
                }
            }
            return result;
        }
    }
}