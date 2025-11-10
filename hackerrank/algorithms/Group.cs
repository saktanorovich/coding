using System;
using System.Collections.Generic;

namespace interview.hackerrank {
    public class Group {
        public long count(string[] gridFarm, int numOfRows, int numOfCols) {
            var fields = getFields(gridFarm, numOfRows, numOfCols);
            var parity = new int[2];
            foreach (var field in fields) {
                ++parity[field & 1];
            }
            var result = 1L;
            result *= pow(2, parity[1] - 1);
            result %= modulo;
            result *= pow(2, parity[0]);
            result %= modulo;
            return result;
        }

        private const long modulo = 1000000007;

        private readonly int[] dr = { -1, 0, +1, 0 };
        private readonly int[] dc = { 0, -1, 0, +1 };

        private int[] getFields(string[] gridFarm, int numOfRows, int numOfCols) {
            var result = new List<int>();
            var visited = new bool[numOfRows, numOfCols];
            for (var r = 0; r < numOfRows; ++r) {
                for (var c = 0; c < numOfCols; ++c) {
                    if (gridFarm[r][c] == 'Y' && !visited[r, c]) {
                        result.Add(bfs(gridFarm, numOfRows, numOfCols, visited, r, c));
                    }
                }
            }
            return result.ToArray();
        }

        private int bfs(string[] gridFarm, int numOfRows, int numOfCols, bool[,] visited, int row, int col) {
            var result = 0;
            var queue = new Queue<int>();
            visited[row, col] = true;
            for (queue.Enqueue(row * numOfCols + col); queue.Count > 0;) {
                var element = queue.Dequeue();
                row = element / numOfCols;
                col = element % numOfCols;
                result = result + 1;
                for (var k = 0; k < 4; ++k) {
                    var r = row + dr[k];
                    var c = col + dc[k];
                    if (0 <= r && r < numOfRows) {
                        if (0 <= c && c < numOfCols) {
                            if (gridFarm[r][c] == 'Y' && !visited[r, c]) {
                                visited[r, c] = true;
                                queue.Enqueue(r * numOfCols + c);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private long pow(long x, long k) {
            if (k == 0) {
                return 1;
            }
            else if (k % 2 == 0) {
                return pow((x * x) % modulo, k / 2);
            }
            else {
                return (x * pow(x, k - 1)) % modulo;
            }
        }
    }
}
