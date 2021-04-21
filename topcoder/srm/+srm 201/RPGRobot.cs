using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class RPGRobot {
        public string[] where(string[] map, string movements) {
            return where(map, movements.Split(new[] { ',', ' '}, StringSplitOptions.RemoveEmptyEntries));
        }

        private static string[] where(string[] map, string[] movements) {
            var result = new List<string>();
            for (var j = 1; j < map[0].Length; j += 2) {
                for (var i = 1; i < map.Length; i += 2) {
                    if (possible(map, movements, i, j)) {
                        result.Add(string.Format("{0},{1}", j, i));
                    }
                }
            }
            return result.ToArray();
        }

        private static bool possible(string[] map, string[] movements, int x, int y) {
            for (var i = 0; i < movements.Length; i += 2) {
                for (var k = 0; k < alphabet.Length; ++k) {
                    if (movements[i].IndexOf(alphabet[k]) >= 0) {
                        if (get(map, x + dx[k], y + dy[k]) != ' ' &&
                            get(map, x + dx[k], y + dy[k]) != '?') {
                            return false;
                        }
                    }
                    else if (get(map, x + dx[k], y + dy[k]) == ' ') {
                        return false;
                    }
                }
                if (i + 1 < movements.Length) {
                    var k = alphabet.IndexOf(movements[i + 1]);
                    x += 2 * dx[k];
                    y += 2 * dy[k];
                }
            }
            return true;
        }

        private static char get(string[] map, int x, int y) {
            if (0 <= x && x < map.Length && 0 <= y && y < map[0].Length) {
                return map[x][y];
            }
            return '?';
        }

        private static readonly string alphabet = "NESW";
        private static readonly int[] dx = { -1, 0, +1, 0 };
        private static readonly int[] dy = { 0, +1, 0, -1 };
    }
}