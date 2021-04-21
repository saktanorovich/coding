using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class WordWrap {
        public string[] justify(string[] lines, int columnWidth) {
            return justifyImpl(split(lines), columnWidth);
        }

        private string[] justifyImpl(string[] words, int columnWidth) {
            var result = new List<string>();
            string current = string.Empty;
            for (var i = 0; i < words.Length; ++i) {
                if (string.Join(" ", current, words[i]).Trim().Length > columnWidth) {
                    result.Add(current);
                    current = string.Empty;
                }
                current = string.Join(" ", current, words[i]).Trim();
            }
            if (!string.IsNullOrEmpty(current)) {
                result.Add(current);
            }
            return result.ToArray();
        }

        private static string[] split(string[] lines) {
            var result = new List<string>();
            foreach (var line in lines) {
                result.AddRange(line.Split(' '));
            }
            return result.ToArray();
        }
    }
}