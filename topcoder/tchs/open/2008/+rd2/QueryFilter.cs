using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class QueryFilter {
        public string[] preprocess(string query, string[] common) {
            var map = new SortedSet<string>();
            foreach (var word in query.Split(' ')) {
                if (common.Contains(word) == false) {
                    map.Add(word);
                }
            }
            return map.ToArray();
        }
    }
}
