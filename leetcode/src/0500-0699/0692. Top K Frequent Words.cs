using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0692 {
        public IList<string> TopKFrequent(string[] words, int k) {
            var freq = new Dictionary<string, int>();
            foreach (var word in words) {
                if (freq.ContainsKey(word) == false) {
                    freq.Add(word, 1);
                } else {
                    freq[word]++;
                }
            }
            var heap = new Heap<string>((a, b) => {
                if (freq[a] != freq[b]) {
                    return freq[b] - freq[a];
                }
                return a.CompareTo(b);
            }, freq.Keys.ToArray());
            var res = new string[k];
            for (var i = 0; i < k; ++i) {
                res[i] = heap.Pop();
            }
            return res;
        }

        private class Heap<T> {
            public readonly Comparison<int> less;
            public readonly T[] heap;
            public int size;

            public Heap(Comparison<T> comp, T[] data) {
                heap = new T[data.Length + 1];
                less = (x, y) => comp(heap[x], heap[y]);
                size = data.Length;
                for (var i = 0; i < size; ++i) {
                    heap[i + 1] = data[i];
                }
                for (var i = size / 2; i > 0; --i) {
                    down(i);
                }
            }

            public void Push(T element) {
                size = size + 1;
                heap[size] = element;
                for (var pos = size; pos > 1;) {
                    var parent = pos / 2;
                    if (less(pos, parent) < 0) {
                        swap(pos, parent);
                        pos = parent;
                        continue;
                    }
                    break;
                }
            }

            public T Pop() {
                var element = heap[1];
                swap(size, 1);
                size = size - 1;
                down(1);
                return element;
            }

            private void down(int pos) {
                while (2 * pos <= size) {
                    var child = 2 * pos;
                    if (2 * pos + 1 <= size) {
                        if (less(2 * pos + 1, child) < 0) {
                            child = 2 * pos + 1;
                        }
                    }
                    if (less(child, pos) < 0) {
                        swap(child, pos);
                        pos = child;
                        continue;
                    }
                    break;
                }
            }

            private void swap(int a, int b) {
                var temp = heap[a];
                heap[a] = heap[b];
                heap[b] = temp;
            }
        }
    }
}
