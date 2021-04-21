using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class SimpleTextProcessor {
        public string[] makeColumns(string[] words) {
            return makeColumns(
                words.Take(words.Length / 2),
                words.Skip(words.Length / 2)).ToArray();
        }

        private static IEnumerable<string> makeColumns(IEnumerable<string> left, IEnumerable<string> right) {
            return format(left, -1).Zip(format(right, +1), (a, b) => string.Format("{0}*{1}", a, b));
        }

        private static IEnumerable<string> format(IEnumerable<string> ss, int align) {
            var frmt = string.Format("{{0,{0}}}",
                ss.Max(x => x.Length) * align);
            return from s in ss select string.Format(frmt, s);
        }
    }
}
