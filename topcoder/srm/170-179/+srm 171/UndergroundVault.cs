using System;

namespace TopCoder.Algorithm {
    public class UndergroundVault {
        public int[] sealOrder(string[] rooms) {
            return sealOrder(Array.ConvertAll(rooms, room => {
                if (!string.IsNullOrWhiteSpace(room)) {
                    return Array.ConvertAll(room.Split(','), int.Parse);
                }
                return new int[0];
            }), rooms.Length);
        }

        private static int[] sealOrder(int[][] rooms, int n) {
            var result = new int[n];
            var closed = new bool[n];
            for (var order = 0; order + 1 < n; ++order) {
                for (var room = 1; room < n; ++room) {
                    if (!closed[room]) {
                        var reach = new bool[n];
                        closed[room] = true;
                        dfs(rooms, closed, 0, reach);
                        var possible = true;
                        for (var remain = 1; remain < n; ++remain) {
                            if (!closed[remain] && !reach[remain]) {
                                possible = false;
                                break;
                            }
                        }
                        if (possible) {
                            result[order] = room;
                            break;
                        }
                        closed[room] = false;
                    }
                }
            }
            return result;
        }

        private static void dfs(int[][] rooms, bool[] closed, int room, bool[] reach) {
            reach[room] = true;
            foreach (var next in rooms[room]) {
                if (!closed[next] && !reach[next]) {
                    dfs(rooms, closed, next, reach);
                }
            }
        }
    }
}