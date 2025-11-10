using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Matching {
        public string[] findMatch(string[] first, string[] second) {
            var result = new string[4];
            for (var i = 0; i < 4; ++i) {
                result[i] = findMatch(Characteristics[i], first[i], second[i]);
            }
            return result;
        }

        private string findMatch(string[] symbols, string first, string second) {
            if (first == second) {
                return first;
            }
            foreach (var symbol in symbols) {
                if (symbol != first && symbol != second) {
                    return symbol;
                }
            }
            return string.Empty;
        }

        private static readonly string[][] Characteristics = {
            new[] { "CIRCLE", "SQUIGGLE", "DIAMOND" },
            new[] { "RED", "BLUE", "GREEN" },
            new[] { "SOLID", "STRIPED", "EMPTY" },
            new[] { "ONE", "TWO", "THREE" }
        };
    }
}