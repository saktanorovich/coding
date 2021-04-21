using System;

namespace TopCoder.Algorithm {
    public class WordSpaces {
        public int[] find(string sentence, string[] words) {
            var result = new int[words.Length];
            for (var i = 0; i < words.Length; ++i) {
                result[i] = find(sentence, words[i]);
            }
            return result;
        }

        private static int find(string sentence, string word) {
            for (var i = 0; i + word.Length <= sentence.Length; ++i) {
                if (sentence[i] == word[0]) {
                    for (var spaces = 0; ; ++spaces) {
                        if (i + (word.Length - 1) * (spaces + 1) < sentence.Length) {
                            for (int a = i, b = 0; b < word.Length; a += spaces + 1, ++b) {
                                if (sentence[a] != word[b]) {
                                    goto next;
                                }
                            }
                            return i;
                            next:;
                        }
                        else break;
                    }
                }
            }
            return -1;
        }
    }
}