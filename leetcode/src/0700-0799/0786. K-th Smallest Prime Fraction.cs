using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0786 {
        public int[] KthSmallestPrimeFraction(int[] A, int K) {
            var heap = new Heap<int>((x, y) => {
                var x0 = HI(x);
                var x1 = LO(x);
                var y0 = HI(y);
                var y1 = LO(y);
                return A[x0] * A[y1] - A[x1] * A[y0];
            }, A.Length - 1);
            for (var i = 0; i < A.Length - 1; ++i) {
                heap.Push(MK(i, A.Length - 1));
            }
            for (; K > 1; --K) {
                var h = heap.Pop();
                var h0 = HI(h);
                var h1 = LO(h);
                if (h0 + 1 < h1) {
                    heap.Push(MK(h0, h1 - 1));
                }
            }
            var m = heap.Pop();
            var m0 = HI(m);
            var m1 = LO(m);
            return new[] { A[m0], A[m1] };
        }

        private int MK(int x, int y) => (x << 16) | y;
        private int HI(int x)        => (x >> 16) & 0xFFFF;
        private int LO(int x)        => (x & 0xFFFF);

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

            private void swap(int a, int b) {
                var temp = heap[a];
                heap[a] = heap[b];
                heap[b] = temp;
            }
        }
    }
}
