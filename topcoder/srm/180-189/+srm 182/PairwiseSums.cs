using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class PairwiseSums {
        public int[] reverse(string[] sums) {
            return reverse(Array.ConvertAll(string.Concat(
                Array.ConvertAll(sums, sum => " " + sum + " ")).Split(
                    new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse));
        }

        private static int[] reverse(int[] sums) {
            Array.Sort(sums);
            var numOfNumbers = 0;
            for (var i = 0; i <= sums.Length; ++i) {
                if (i * (i - 1) / 2 == sums.Length) {
                    numOfNumbers = i;
                    break;
                }
            }
            if (numOfNumbers > 0) {
                var results = new List<int[]>();
                for (var smallest = 0; smallest + smallest <= sums[0]; ++smallest) {
                    var result = reverse(new List<int>(sums), numOfNumbers, smallest);
                    if (result != null)
                        results.Add(result);
                }
                if (results.Count == 1)
                    return results[0];
            }
            return new int[0];
        }

        private static int[] reverse(IList<int> sums, int numOfNumbers, int smallest) {
            var numbers = new int[numOfNumbers]; numbers[0] = smallest;
            for (var ipos = 1; ipos < numOfNumbers; ++ipos) {
                if (sums.Count > 0) {
                    numbers[ipos] = sums[0] - smallest;
                    for (var jpos = 0; jpos < ipos; ++jpos) {
                        if (!sums.Remove(numbers[ipos] + numbers[jpos])) {
                            return null;
                        }
                    }
                }
                else return null;
            }
            return numbers;
        }
    }
}