using System;

namespace TopCoder.Algorithm {
    public class Hangman {
        public string guessWord(string feedback, string[] words) {
            var result = string.Empty;
            foreach (var word in words) {
                if (match(word, feedback)) {
                    if (!string.IsNullOrEmpty(result)) {
                        return string.Empty;
                    }
                    result = word;
                }
            }
            return result;
        }

        private static bool match(string word, string feedback) {
            if (word.Length == feedback.Length) {
                var cnt = new int[256];
                for (var i = 0; i < word.Length; ++i) {
                    ++cnt[word[i]];
                }
                for (var i = 0; i < word.Length; ++i) {
                    if (feedback[i] != '-') {
                        if (feedback[i] != word[i]) {
                            return false;
                        }
                        --cnt[feedback[i]];
                    }
                }
                for (var i = 0; i < word.Length; ++i) {
                    if (feedback[i] != '-')
                        if (cnt[feedback[i]] != 0) {
                            return false;
                        }
                }
                return true;
            }
            return false;
        }
    }
}