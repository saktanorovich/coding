using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0973 {
        public int[][] KClosest(int[][] points, int K) {
            var d = new long[points.Length];
            for (var i = 0; i < points.Length; ++i) {
                d[i] = 1L * points[i][0] * points[i][0] + points[i][1] * points[i][1];
            }
            var f = new Comparison<int>((a, b) => d[b].CompareTo(d[a]));
            var h = new Heap<int>(f, K);
            for (var i = 0; i < K; ++i) {
                h.Push(i);
            }
            for (var i = K; i < points.Length; ++i) {
                if (f(h.Peek(), i) < 0) {
                    h.Pop();
                    h.Push(i);
                }
            }
            return h.AsEnumerable().Select(x => points[x]).ToArray();
        }

        private class Heap<T> {
            private readonly Comparison<int> less;
            private readonly T[] heap;
            private int size;

            public Heap(Comparison<T> comp, int capacity) {
                heap = new T[capacity + 1];
                less = (x, y) => comp(heap[x], heap[y]);
                size = 0;
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

            public T Peek() {
                return heap[1];
            }

            public T Pop() {
                var element = heap[1];
                swap(size, 1);
                size = size - 1;
                for (var pos = 1; 2 * pos <= size;) {
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
                return element;
            }

            public IEnumerable<T> AsEnumerable() {
                for (var i = 1; i <= size; ++i) {
                    yield return heap[i];
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
