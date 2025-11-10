using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0691 {
        public int MinStickers(string[] stickers, string target) {
            var stick = stickers.Select(s => new Sticker(s)).ToArray();
            var count = new Dictionary<Sticker, int>();
            var queue = new Queue<Sticker>();
            var start = new Sticker(target);
            count[start] = 0;
            for (queue.Enqueue(start); queue.Count > 0;) {
                var curr = queue.Dequeue();
                var have = count[curr];
                if (curr.IsEmpty()) {
                    return have;
                }
                foreach (var take in stick) {
                    if (curr.Any(take)) {
                        var next = curr.Sub(take);
                        if (count.TryGetValue(next, out var len) == false || len > have + 1) {
                            count[next] = have + 1;
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            return -1;
        }

        private class Sticker {
            private readonly int[] state;
            private readonly int hash;

            private Sticker(int[] state) {
                this.state = state;
                foreach (var c in state) {
                    hash = hash * 17 + c;
                }
            }

            public Sticker(string sticker) {
                state = new int[26];
                foreach (var c in sticker) {
                    state[c - 'a']++;
                }
                foreach (var c in state) {
                    hash = hash * 17 + c;
                }
            }
            
            public bool IsEmpty() {
                return hash == 0;
            }

            public bool Any(Sticker that) {
                for (var i = 0; i < 26; ++i) {
                    if (this.state[i] != 0 && that.state[i] != 0) {
                        return true;
                    }
                }
                return false;
            }

            public Sticker Sub(Sticker that) {
                var state = new int[26];
                for (var i = 0; i < 26; ++i) {
                    state[i] = this.state[i];
                    if (state[i] != 0 && that.state[i] != 0) {
                        if (state[i] >= that.state[i]) {
                            state[i] -= that.state[i];
                        } else {
                            state[i] = 0;
                        }
                    }
                }
                return new Sticker(state);
            }

            public override bool Equals(object obj) {
                var other = (Sticker)obj;
                for (var i = 0; i < 26; ++i) {
                    if (state[i] != other.state[i]) {
                        return false;
                    }
                }
                return true;
            }

            public override int GetHashCode() {
                return hash;
            }
        }
    }
}
