using System;

namespace TopCoder.Algorithm {
    public class DiskScheduler {
        public int optimize(int start, int[] sectors) {
            return Math.Min(
                optimize(sectors, start),
                optimize(sectors, start + 100));
        }

        private int optimize(int[] sectors, int start) {
            var result = int.MaxValue;
            for (var left = 1; left <= start; ++left)
                for (var right = start; right <= 200; ++right) {
                    if (read(sectors, left, right)) {
                        result = Math.Min(result, Math.Min(right - start, start - left) + right - left);
                    }
                }
            return result;
        }

        private bool read(int[] sectors, int left, int right) {
            foreach (var sector in sectors) {
                disk[sector] = 1;
                disk[sector + 100] = 1;
            }
            var result = 0;
            for (var pos = left; pos <= right; ++pos) {
                if (disk[pos] == 1) {
                    ++result;
                    disk[pos] = 0;
                    if (pos > 100)
                        disk[pos - 100] = 0;
                }
            }
            return result == sectors.Length;
        }

        private readonly int[] disk = new int[200 + 1];
    }
}