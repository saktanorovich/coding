using System;

namespace TopCoder.Algorithm {
    public class SquareCode {
        public string[] completeIt(string[] grille) {
            return completeIt(Array.ConvertAll(grille, row => row.ToCharArray()), grille.Length);
        }

        private static string[] completeIt(char[][] grille, int n) {
            for (var i = 0; i < n / 2; ++i) {
                for (var j = 0; j < n / 2; ++j) {
                    var opened = 0;
                    for (int k = 0, x = i, y = j; k < 4; ++k) {
                        if (grille[x][y] == '.')
                            ++opened;
                        rotate(ref x, ref y, n);
                    }
                    if (opened == 0)
                        grille[i][j] = '.';
                    if (opened >= 2)
                        return new string[0];
                }
            }
            var result = new string[n];
            for (var i = 0; i < n; ++i) {
                result[i] = new string(grille[i]);
            }
            return result;
        }

        private static void rotate(ref int x, ref int y, int n) {
            var xx = x;
            var yy = y;
            x = yy;
            y = n - 1 - xx;
        }
    }
}