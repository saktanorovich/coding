using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class YoungBrother {
        public string[] restoreWords(string[] lines, int n, int k) {
            var result = new List<string>();
            var name = string.Empty;
            for (var i = 0; i < lines.Length; ++i) {
                for (var j = 0; j < lines[i].Length; ++j) {
                    if (lines[i][j] != ' ') {
                        if (name.Length + 1 <= k) {
                            name += lines[i][j].ToString();
                        }
                        else {
                            result.Add(name);
                            name = lines[i][j].ToString();
                        }
                    }
                }
            }
            if (name != string.Empty) {
                result.Add(name);
            }
            if (k == 0) {
                for (var i = 0; i < n; ++i) {
                    result.Add("");
                }
            }
            return result.ToArray();
        }
    }
}
