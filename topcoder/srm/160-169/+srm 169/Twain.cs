using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Twain {
        public string getNewSpelling(int year, string phrase) {
            for (var y = 0; y <= year; ++y) {
                phrase = " " + phrase;
                phrase = Rules[y](phrase);
                phrase = phrase.Remove(0, 1);
            }
            return phrase;
        }

        private static readonly Func<string, string>[] Rules = {
            Rule0, Rule1, Rule2, Rule3, Rule4, Rule5, Rule6, Rule7
        };

        private static string Rule0(string text) {
            return text;
        }

        private static string Rule1(string text) {
            return text.Replace(" x", " z").
                        Replace("x", "ks");
        }

        private static string Rule2(string text) {
            return text.Replace("y", "i");
        }

        private static string Rule3(string text) {
            text = text.Replace("ce", "se");
            text = text.Replace("ci", "si");
            return text;
        }

        private static string Rule4(string text) {
            while (text.IndexOf("ck") >= 0) {
                text = text.Replace("ck", "k");
            }
            return text;
        }

        private static string Rule5(string text) {
            text = text.Replace(" sch", " sk");
            while (text.IndexOf("chr") >= 0) {
                text = text.Replace("chr", "kr");
            }
            var chars = (text + "?").ToCharArray();
            for (var i = 0; i + 1 < chars.Length; ++i) {
                if (chars[i] == 'c') {
                    if (chars[i + 1] != 'h') {
                        chars[i] = 'k';
                    }
                }
            }
            return new string(chars.Take(text.Length).ToArray());
        }

        private static string Rule6(string text) {
            return text.Replace(" kn", " n");
        }

        private static string Rule7(string text) {
            foreach (var consonant in "bcdfghjklmnpqrstvwxz") {
                var doubled = consonant.ToString() + consonant.ToString();
                while (text.IndexOf(doubled) >= 0) {
                    text = text.Replace(doubled, consonant.ToString());
                }
            }
            return text;
        }
    }
}