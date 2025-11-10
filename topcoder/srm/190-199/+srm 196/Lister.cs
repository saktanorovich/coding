using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Lister {
        public string[] doList(int pageWidth, string[] names) {
            Array.Sort(names);
            for (var numOfRows = 1;; ++numOfRows) {
                var listing = doList(numOfRows, pageWidth, names);
                if (listing != null) {
                    return listing;
                }
            }
        }

        /**/
        private string[] doList(int numOfRows, int pageWidth, string[] names) {
            var best = (string[])null;
            var bestCols = int.MaxValue;
            for (var full = 1; full <= names.Length; ++full) {
                var inColumns = new List<int>(Array.ConvertAll(new int[full], a => numOfRows));
                for (var part = 0; part <= names.Length; ++part) {
                    if (full * numOfRows + part * (numOfRows - 1) == names.Length) {
                        var listingColumns = full + part;
                        var listing = doList(numOfRows, listingColumns, pageWidth, names, inColumns);
                        if (listing[0].Length <= pageWidth) {
                            relax(listing, listingColumns, ref best, ref bestCols);
                        }
                    }
                    inColumns.Add(numOfRows - 1);
                }
            }
            return best;
        }

        private void relax(string[] listing, int listingCols, ref string[] best, ref int bestCols) {
            var difference = rightmost(listing) - rightmost(best);
            if (difference < 0 || (difference == 0 && listingCols < bestCols)) {
                best = listing;
                bestCols = listingCols;
            }
        }

        private static int rightmost(string[] listing) {
            if (listing != null) {
                var position = 0;
                for (var row = 0; row < listing.Length; ++row)
                    for (var col = listing[row].Length - 1; col >= 0; --col)
                        if (listing[row][col] != ' ') {
                            position = Math.Max(position, col);
                        }
                return position;
            }
            return int.MaxValue;
        }

        /**
        // this method can be a slightly modified for building listings of arbitrary form..
        private string[] doList(int numOfRows, int pageWidth, string[] names) {
            var longest = new int[names.Length, names.Length];
            for (var i = 0; i < names.Length; ++i) {
                longest[i, i] = names[i].Length;
                for (var j = i + 1; j < names.Length; ++j) {
                    longest[i, j] = Math.Max(longest[i, j - 1], names[j].Length);
                }
            }
            var best = new int[names.Length + 1, names.Length + 1];
            var prev = new int[names.Length + 1, names.Length + 1];
            best[1, numOfRows] = longest[0, numOfRows - 1];
            prev[1, numOfRows] = numOfRows;
            for (var col = 2; col <= names.Length; ++col) {
                for (var take = numOfRows + 1; take <= names.Length; ++take) {
                    best[col, take] = int.MaxValue;
                    // at this point we should check that each row has a name in each column, except the last row
                    // which may have no names in its final column(s)(requirement from the problem statement)..
                    for (var here = numOfRows - 1; here <= numOfRows; ++here) { // in general start from 1 but in order to optimize iterate from numOfRows - 1
                        var validListing = here < numOfRows || (here == numOfRows && take == col * numOfRows);
                        if (validListing) {
                            if (0 < best[col - 1, take - here] && best[col - 1, take - here] < int.MaxValue) {
                                var eval = best[col - 1, take - here] + longest[take - here, take - 1] + 1;
                                if (best[col, take] > eval) {
                                    best[col, take] = eval;
                                    prev[col, take] = here;
                                }
                            }
                        }
                    }
                }
            }
            var optimum = int.MaxValue;
            for (var numOfCols = 1; numOfCols <= names.Length; ++numOfCols) {
                if (0 < best[numOfCols, names.Length] && best[numOfCols, names.Length] <= pageWidth) {
                    optimum = Math.Min(optimum, best[numOfCols, names.Length]);
                }
            }
            if (optimum < int.MaxValue) {
                for (var numOfCols = 1; numOfCols <= names.Length; ++numOfCols) {
                    if (best[numOfCols, names.Length] == optimum) {
                        var inColumn = new int[numOfCols];
                        for (int total = names.Length, col = numOfCols; total > 0; --col) {
                            inColumn[col - 1] = prev[col, total];
                            total -= inColumn[col - 1];
                        }
                        return doList(numOfRows, numOfCols, pageWidth, names, inColumn);
                    }
                }
            }
            return null;
        }
        /**/

        private static string[] doList(int numOfRows, int numOfCols, int pageWidth, string[] names, IList<int> inColumn) {
            var columns = new string[numOfCols][];
            for (var col = 0; col < numOfCols; ++col) {
                columns[col] = new string[numOfRows];
                for (var row = 0; row < numOfRows; ++row) {
                    columns[col][row] = string.Empty;
                }
            }
            var ixname = 0;
            for (var col = 0; col < numOfCols; ++col) {
                for (var row = 0; row < inColumn[col]; ++row) {
                    columns[col][row] = names[ixname];
                    ixname = ixname + 1;
                }
            }
            var columnsWidth = new int[numOfCols];
            for (var col = 0; col < numOfCols; ++col) {
                columnsWidth[col] = columns[col].Select(name => name.Length).Max();
            }
            var result = new string[numOfRows];
            for (var row = 0; row < numOfRows; ++row) {
                result[row] = string.Empty;
                for (var col = 0; col < numOfCols; ++col) {
                    var name = columns[col][row];
                    result[row] += name.PadRight(columnsWidth[col], ' ');
                    if (col + 1 < numOfCols) {
                        result[row] += ' ';
                    }
                }
                result[row] = result[row].PadRight(pageWidth, ' ');
            }
            return result;
        }
    }
}