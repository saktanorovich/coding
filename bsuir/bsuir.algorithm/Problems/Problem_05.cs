using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Problem_05 {
        public bool process(int testCase, InputReader reader, OutputWriter writer) {
            this.n = reader.NextInt();
            this.m = reader.NextInt();
            this.g = new List<Edge>[n];
            for (var i = 0; i < n; ++i) {
                g[i] = new List<Edge>();
            }
            for (var i = 0; i < m; ++i) {
                var a = reader.NextInt() - 1;
                var b = reader.NextInt() - 1;
                var c = reader.NextInt();
                g[a].Add(new Edge(b, c));
                g[b].Add(new Edge(a, c));
            }
            var S = reader.NextInt() - 1;
            var T = reader.NextInt() - 1;
            writer.WriteLine(dijkstra(S, T));
            return true;
        }

        private long dijkstra(int S, int T) {
            this.dist = new long[n];
            this.heap = new int[n + 1];
            this.addr = new int[n + 1];
            dist[S] = 0;
            addr[S] = 1;
            heap[1] = S;
            size = 1;
            for (var i = 0; i < n; ++i) {
                if (i != S) {
                    dist[i] = long.MaxValue;
                    push(i);
                }
            }
            while (true) {
                var a = pop();
                if (a == T) {
                    return dist[T];
                }
                foreach (var e in g[a]) {
                    relax(a, e.target, e.length);
                }
            }
        }

        private void relax(int a, int b, int c) {
            if (dist[b] > dist[a] + c) {
                dist[b] = dist[a] + c;
                pushUp(addr[b]);
            }
        }

        public void push(int node) {
            size = size + 1;
            heap[size] = node;
            addr[node] = size;
            pushUp(size);
        }

        public int pop() {
            var node = heap[1];
            swap(1, size--);
            pushDn(1);
            return node;
        }

        private void pushUp(int pos) {
            if (pos > 1) {
                var par = parent(pos);
                if (dist[heap[pos]] < dist[heap[par]]) {
                    swap(par, pos);
                    pushUp(par);
                }
            }
        }

        private void pushDn(int pos) {
            var le = leChild(pos);
            var ri = riChild(pos);
            if (le > 0) {
                var child = le;
                if (ri > 0) {
                    if (dist[heap[ri]] < dist[heap[le]]) {
                        child = ri;
                    }
                }
                if (dist[heap[child]] < dist[heap[pos]]) {
                    swap(child, pos);
                    pushDn(child);
                }
            }
        }

        private void swap(int a, int b) {
            addr[heap[a]] = b;
            addr[heap[b]] = a;
            var temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;
        }

        private int parent(int pos) {
            if (1 < pos) {
                return pos / 2;
            }
            return 0;
        }

        private int leChild(int pos) {
            if (2 * pos <= size) {
                return 2 * pos;
            }
            return 0;
        }

        private int riChild(int pos) {
            if (2 * pos + 1 <= size) {
                return 2 * pos + 1;
            }
            return 0;
        }

        private List<Edge>[] g;
        private long[] dist;
        private int[] addr;
        private int[] heap;
        private int size;
        private int n;
        private int m;

        private struct Edge {
            public readonly int target;
            public readonly int length;

            public Edge(int target, int length) {
                this.target = target;
                this.length = length;
            }
        }
    }
}
