using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class StringChain {
        private readonly HashSet<string> _vocabulary = new HashSet<string>();
        private readonly Dictionary<string, int> _cache = new Dictionary<string, int>();

        public int longest(string[] words) {
            foreach (var word in words) {
                _vocabulary.Add(word);
            }
            var result = 1;
            foreach (var word in words) {
                result = Math.Max(result, longest(word));
            }
            return result;
        }

        private int longest(string word) {
            if (_vocabulary.Contains(word)) {
                int result;
                if (!_cache.TryGetValue(word, out result)) {
                    result = 0;
                    for (var i = 0; i < word.Length; ++i) {
                        result = Math.Max(result, longest(word.Remove(i, 1)) + 1);
                    }
                    _cache[word] = result;
                }
                return result;
            }
            return 0;
        }
    }
}
