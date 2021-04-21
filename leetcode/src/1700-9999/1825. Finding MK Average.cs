using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1825 {
        public class MKAverage {
            private readonly Queue<int> Q;
            private readonly STree L;
            private readonly STree M;
            private readonly STree R;
            private readonly int m, k;
            private int n;

            public MKAverage(int m, int k) {
                this.m = m;
                this.k = k;
                Q = new Queue<int>();
                L = new STree();
                M = new STree();
                R = new STree();
            }

            public void AddElement(int num) {
                Q.Enqueue(num);
                n = n + 1;
                if (n > m) {
                    var val = Q.Dequeue();
                    L.Exclude(val, n - m - 1);
                    M.Exclude(val, n - m - 1);
                    R.Exclude(val, n - m - 1);
                }
                if (n > m) {
                    if (M.Count == 0) {
                        if (num < R.Min.value) {
                            L.Include(num, n - 1);
                        } else {
                            R.Include(num, n - 1);
                        }
                    } else if (L.Count == 0) {
                        R.Include(num, n - 1);
                    } else if (R.Count == 0) {
                        L.Include(num, n - 1);
                    } else if (num < M.Min.value) {
                        L.Include(num, n - 1);
                    } else if (num < R.Min.value) {
                        M.Include(num, n - 1);
                    } else {
                        R.Include(num, n - 1);
                    }
                } else {
                    L.Include(num, n - 1);
                }
                Balance();
            }

            public int CalculateMKAverage() {
                var count = L.Count + M.Count + R.Count;
                if (count < m)
                    return -1;
                if (count > m)
                    throw new InvalidOperationException();
                return (int)(M.Summa / (m - 2 * k));
            }

            private void Balance() {
                if (n < m) {
                    return;
                }
                while (L.Count > k) { var max = L.Max; L.Exclude(max); M.Include(max); }
                while (R.Count > k) { var min = R.Min; R.Exclude(min); M.Include(min); }
                while (L.Count < k) { var min = M.Min; M.Exclude(min); L.Include(min); }
                while (R.Count < k) { var max = M.Max; M.Exclude(max); R.Include(max); }
            }

            private sealed class STree {
                private readonly SortedSet<(int value, int index)> set;
                private long sum;

                public STree() {
                    set = new SortedSet<(int value, int index)>(
                        Comparer<(int value, int index)>.Create(
                            ((int value, int index) item1, (int value, int index) item2) => {
                                if (item1.value != item2.value) {
                                    return item1.value.CompareTo(item2.value);
                                } else {
                                    return item1.index.CompareTo(item2.index);
                                }
                        }));
                }

                public int  Count => set.Count;
                public long Summa => sum;

                public (int value, int index) Min {
                    get {
                        if (set.Count > 0) {
                            return set.Min;
                        }
                        throw new IndexOutOfRangeException();
                    }
                }

                public (int value, int index) Max {
                    get {
                        if (set.Count > 0) {
                            return set.Max;
                        }
                        throw new IndexOutOfRangeException();
                    }
                }
                
                public void Include((int value, int index) item) {
                    if (set.Add(item)) {
                        sum += item.value;
                    }
                }

                public void Exclude((int value, int index) item) {
                    if (set.Remove(item)) {
                        sum -= item.value;
                    }
                }

                public void Include(int value, int index) {
                    if (set.Add((value, index))) {
                        sum += value;
                    }
                }

                public void Exclude(int value, int index) {
                    if (set.Remove((value, index))) {
                        sum -= value;
                    }
                }
            }
        }
    }
}
