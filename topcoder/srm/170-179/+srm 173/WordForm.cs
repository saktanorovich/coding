using System;

namespace TopCoder.Algorithm {
    public class WordForm {
        public string getSequence(string word) {
            var chars = word.ToLower().ToCharArray();
            for (var pos = 0; pos < chars.Length; ++pos) {
                if (vowel(chars, pos))
                    chars[pos] = 'V';
                else
                    chars[pos] = 'C';
            }
            var result = new string(chars);
            result = replace(result, "VV", "V");
            result = replace(result, "CC", "C");
            return result;
        }

        private static bool vowel(char[] chars, int pos) {
            if ("aeiou".IndexOf(chars[pos]) >= 0) {
                return true;
            }
            if (pos > 0) {
                return chars[pos] == 'y' && chars[pos - 1] == 'C';
            }
            return false;
        }

        private static string replace(string text, string replaceable, string replacement) {
            while (text.IndexOf(replaceable) >= 0) {
                text = text.Replace(replaceable, replacement);
            }
            return text;
        }
    }
}