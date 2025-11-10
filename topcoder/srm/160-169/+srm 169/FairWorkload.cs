using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class FairWorkload {
        public int getMostWork(int[] folders, int workers) {
            return getMostWork1(folders, workers);
            //return getMostWork2(folders, workers);
        }

        private static int getMostWork1(int[] folders, int workers) {
            var best = new int[folders.Length + 1, workers + 1];
            for (var folder = 0; folder <= folders.Length; ++folder) {
                for (var worker = 0; worker <= workers; ++worker) {
                    best[folder, worker] = int.MaxValue;
                }
            }
            best[0, 0] = 0;
            for (var curr = 1; curr <= folders.Length; ++curr) {
                for (var worker = 1; worker <= workers; ++worker) {
                    for (var prev = curr - 1; prev >= 0; --prev) {
                        best[curr, worker] = Math.Min(best[curr, worker], Math.Max(best[prev, worker - 1], payload(folders, prev, curr - 1)));
                    }
                }
            }
            return best[folders.Length, workers];
        }

        private static int payload(int[] folders, int lo, int hi) {
            var result = 0;
            for (var i = lo; i <= hi; ++i) {
                result += folders[i];
            }
            return result;
        }

        private static int getMostWork2(int[] folders, int workers) {
            int lo = 1, hi = folders.Sum();
            while (lo < hi) {
                int payload = (lo + hi) / 2;
                int total = 0, required = 1;
                for (var i = 0; i < folders.Length; ++i) {
                    if (total + folders[i] <= payload) {
                        total = total + folders[i];
                    }
                    else {
                        total = folders[i];
                        ++required;
                        if (total > payload) {
                            required += workers;
                            break;
                        }
                    }
                }
                if (required > workers)
                    lo = payload + 1;
                else
                    hi = payload;
            }
            return lo;
        }
    }
}