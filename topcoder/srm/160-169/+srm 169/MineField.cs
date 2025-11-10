using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class MineField {
        public string[] getMineField(string mineLocations) {
            var rows = new List<int>();
            var cols = new List<int>();
            var ints = mineLocations.Split(new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < ints.Length; i += 2) {
                rows.Add(int.Parse(ints[i + 0]));
                cols.Add(int.Parse(ints[i + 1]));
            }
            return getMineField(rows, cols);
        }

        private static string[] getMineField(IList<int> rows, IList<int> cols) {
            var result = new int[9][];
            for (var i = 0; i < 9; ++i) {
                result[i] = new int[9];
            }
            for (var i = 0; i < rows.Count; ++i) {
                result[rows[i]][cols[i]] =-1;
            }
            for (var i = 0; i < 9; ++i)
                for (var j = 0; j < 9; ++j)
                    if (result[i][j] >= 0)
                        for (var a = i - 1; a <= i + 1; ++a)
                            for (var b = j - 1; b <= j + 1; ++b)
                                if (0 <= a && a < 9 && 0 <= b && b < 9)
                                    if (result[a][b] == -1) {
                                        ++result[i][j];
                                    }
            return Array.ConvertAll(result, row => {
                var res = string.Empty;
                for (var i = 0; i < row.Length; ++i) {
                    if (row[i] < 0) {
                        res += "M";
                        continue;
                    }
                    res += row[i].ToString();
                }
                return res;
            });
        }
    }
}