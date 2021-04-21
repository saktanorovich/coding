public class Solution {
    public IList<int> FindSubstring(string text, string[] words) {
        if (text.Length < 1) {
            return new Small(words).solve(text, text.Length);
        } else {
            return new Large(words).solve(text, text.Length);
        }
    }


    private class Small {
        private readonly Dictionary<string, int> hash;
        private readonly int[] freq;
        private readonly int m;
        private readonly int k;

        public Small(string[] words) {
            this.hash = new Dictionary<string, int>();
            foreach (var word in words) {
                hash.TryAdd(word, hash.Count);
            }
            this.freq = new int[hash.Count];
            foreach (var word in words) {
                freq[hash[word]] ++;
            }
            this.m = words.Length;
            this.k = words[0].Length;
        }

        public IList<int> solve(string text, int n) {
            var answ = new List<int>();
            for (var i = 0; i + m * k <= n; ++i) {
                var take = 0;
                var seen = new int[hash.Count];
                for (var j = 0; j < m; ++j) {
                    var word = text.Substring(i + j * k, k);
                    if (hash.TryGetValue(word, out var indx)) {
                        seen[indx] ++;
                        if (seen[indx] > freq[indx]) {
                            break;
                        }
                        take ++;
                    } else break;
                }
                if (take == m) {
                    answ.Add(i);
                }
            }
            return answ;
        }
    }

    private class Large {
        private readonly Dictionary<string, int> freq;
        private readonly int m;
        private readonly int k;

        public Large(string[] words) {
            this.freq = new Dictionary<string, int>();
            foreach (var word in words) {
                freq.TryAdd(word, 0);
                freq[word] ++;
            }
            this.m = words.Length;
            this.k = words[0].Length;
        }

        public IList<int> solve(string text, int n) {
            var answ = new List<int>();
            var seen = new Dictionary<string, int>();
            // let's iterate over the first positions of sliding window
            for (var i = 0; i < k; ++i) {
                var l = i;
                var r = i;
                var t = 0;
                // move window's block by word length
                while (r + k <= n) {
                    var word = text.Substring(r, k);
                    r += k;
                    if (freq.ContainsKey(word) == false) {
                        t = 0;
                        l = r;
                        seen.Clear();
                        continue;
                    }
                    seen.TryAdd(word, 0);
                    seen[word] ++;
                    t ++;
                    while (seen[word] > freq[word]) {
                        var what = text.Substring(l, k);
                        seen[what] --;
                        t -= 1;
                        l += k;
                    }
                    if (t == m) {
                        answ.Add(l);
                    }
                }
                seen.Clear();
            }
            return answ;
        }
    }
}
