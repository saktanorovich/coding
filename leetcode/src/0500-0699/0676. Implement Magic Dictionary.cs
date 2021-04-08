using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0676 {
        public class MagicDictionary {
            private readonly Dictionary<string, HashSet<char>> dict;

            /** Initialize your data structure here. */
            public MagicDictionary() {
                dict = new Dictionary<string, HashSet<char>>();
            }
            
            /** Build a dictionary through a list of words. */
            public void BuildDict(string[] words) {
                foreach (var word in words) {
                    var chars = word.ToCharArray();
                    for (var i = 0; i < word.Length; ++i) {
                        chars[i] = '*';
                        var template = new string(chars);
                        if (dict.ContainsKey(template) == false) {
                            dict.Add(template, new HashSet<char>());
                        }
                        dict[template].Add(word[i]);
                        chars[i] = word[i];
                    }
                }
            }

            /** Returns if there is any word in the dictionary
                that equals to the given word after modifying
                exactly one character. */
            public bool Search(string word) {
                var template = word.ToCharArray();
                for (var i = 0; i < word.Length; ++i) {
                    template[i] = '*';
                    if (dict.TryGetValue(new string(template), out var chars)) {
                        if (chars.Contains(word[i]) == false || chars.Count > 1) {
                            return true;
                        }
                    }
                    template[i] = word[i];
                }
                return false;
            }
        }
    }
}
