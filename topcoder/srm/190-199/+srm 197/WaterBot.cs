using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TopCoder.Algorithm {
    public class WaterBot {
        public int minTime(string[] garden, int carryLimit) {
            var well = new Pos();
            var init = new Pos();
            var plants = new List<Pos>();
            for (var i = 0; i < garden.Length; ++i)
                for (var j = 0; j < garden[i].Length; ++j) {
                    if ("12345".IndexOf(garden[i][j]) >= 0) {
                        plants.Add(new Pos(i, j));
                    }
                    else if (garden[i][j] == 'W') {
                        well = new Pos(i, j);
                    }
                    else if (garden[i][j] == 'R') {
                        init = new Pos(i, j);
                    }
                }
            return minTime(garden, init, well, plants, carryLimit);
        }

        private static int minTime(string[] garden, Pos init, Pos well, IList<Pos> plants, int carry) {
            var wellObj = Region.MakeBy(garden, well);
            if (wellObj == null) {
                return -1;
            }
            var plantObjs = new List<Plant>();
            foreach (var pos in plants) {
                var plantObj = Plant.MakeBy(garden, pos);
                if (plantObj != null) {
                    plantObjs.Add(plantObj);
                }
                else return -1;
            }
            if (!plantObjs.Any()) {
                return 0;
            }
            /* move robot from init pos to well region, build states and perform bfs.. */
            var shortestDistance = new int[20][,];
            var planteByPosition = new int[20];
            for (var pos = 0; pos < wellObj.Count(); ++pos) {
                shortestDistance[pos] = bfs(garden, wellObj[pos]);
                planteByPosition[pos] = -1;
            }
            for (int i = 0, pos = wellObj.Count(); i < plantObjs.Count; ++i) {
                foreach (var plant in plantObjs[i]) {
                    shortestDistance[pos] = bfs(garden, plant);
                    planteByPosition[pos] = i;
                    pos = pos + 1;
                }
            }
            /* the number of important positions is (4+1)*4=20, the number of plants states are
             * 6^4=1'296, the number of robot states is 6, so the total number of states is 155'520.. */
            var memo = new int[20, 6, 6, 6, 6, 6];
            for (var pos = 0; pos < 20; ++pos)
                for (var robot = 0; robot < 6; ++robot)
                    for (var plant0 = 0; plant0 < 6; ++plant0)
                    for (var plant1 = 0; plant1 < 6; ++plant1)
                    for (var plant2 = 0; plant2 < 6; ++plant2)
                    for (var plant3 = 0; plant3 < 6; ++plant3) {
                        memo[pos, robot, plant0, plant1, plant2, plant3] = int.MaxValue;
                    }
            var queue = new Queue<State>();
            for (var pos = 0; pos < wellObj.Count(); ++pos) {
                var eval = shortestDistance[pos][init.X, init.Y];
                if (eval < int.MaxValue) {
                    var water = new int[4];
                    for (var i = 0; i < plantObjs.Count(); ++i) {
                        water[i] = plantObjs[i].Water;
                    }
                    enqueue(queue, new State(pos, 0, water), memo, eval);
                }
            }
            for (; queue.Any(); queue.Dequeue()) {
                var state = queue.Peek();
                var posit = state.Position;
                var robot = state.Robot;
                var water = state.Water;
                var value = state[memo];
                /* determine pos type and decide whether to add water or water a plant.. */
                if (posit < wellObj.Count()) {
                    if (robot + 1 <= carry) {
                        enqueue(queue, new State(posit, robot + 1, water), memo, value + 1);
                    }
                }
                else if (robot == 0) {
                    /* in this case robot should move to well and add water.. */
                    for (var next = 0; next < wellObj.Count(); ++next) {
                        var add = shortestDistance[posit][wellObj[next].X, wellObj[next].Y];
                        if (add < int.MaxValue) {
                            enqueue(queue, new State(next, 0, water), memo, value + add);
                        }
                    }
                }
                else if (water[planteByPosition[posit]] > 0) {
                    enqueue(queue, new State(posit, robot - 1, dec(water, planteByPosition[posit])), memo, value + 1);
                }
                if (robot > 0) {
                    /* in this case it makes sense to water a plant.. */
                    var next = wellObj.Count();
                    foreach (var plant in plantObjs) {
                        foreach (var plantPos in plant) {
                            var add = shortestDistance[posit][plantPos.X, plantPos.Y];
                            if (add < int.MaxValue) {
                                enqueue(queue, new State(next, robot, water), memo, value + add);
                            }
                            next = next + 1;
                        }
                    }
                }
            }
            var result = int.MaxValue;
            for (var pos = 0; pos < 20; ++pos) {
                result = Math.Min(result, memo[pos, 0, 0, 0, 0, 0]);
            }
            if (result < int.MaxValue)
                return result;
            return -1;
        }

        private static void enqueue(Queue<State> queue, State state, int[,,,,,] memo, int value) {
            if (state[memo] > value) {
                state[memo] = value;
                queue.Enqueue(state);
            }
        }

        private static int[] dec(int[] water, int plant) {
            var result = (int[])water.Clone();
            --result[plant];
            return result;
        }

        private static int[,] bfs(string[] garden, Pos source) {
            int n = garden.Length, m = garden[0].Length;
            var dist = new int[n, m];
            for (var i = 0; i < n; ++i)
                for (var j = 0; j < m; ++j) {
                    dist[i, j] = int.MaxValue;
                }
            dist[source.X, source.Y] = 0;
            var queue = new Queue<Pos>();
            for (queue.Enqueue(source); queue.Count > 0; queue.Dequeue()) {
                var xcurr = queue.Peek().X;
                var ycurr = queue.Peek().Y;
                for (var k = 0; k < 4; ++k) {
                    var xnext = xcurr + dx[k];
                    var ynext = ycurr + dy[k];
                    if (0 <= xnext && xnext < n &&
                        0 <= ynext && ynext < m) {
                        if (garden[xnext][ynext] == '.' || garden[xnext][ynext] == 'R') {
                            if (dist[xnext, ynext] > dist[xcurr, ycurr] + 1) {
                                dist[xnext, ynext] = dist[xcurr, ycurr] + 1;
                                queue.Enqueue(new Pos(xnext, ynext));
                            }
                        }
                    }
                }
            }
            return dist;
        }

        private static readonly int[] dx = { -1,  0, +1,  0 };
        private static readonly int[] dy = {  0, -1,  0, +1 };

        [DebuggerDisplay("Position = {Position}, Robot = {Robot}")]
        private struct State {
            public readonly int Position;
            public readonly int Robot;
            public readonly int[] Water;

            public State(int position, int robot, int[] water) {
                Position = position;
                Robot = robot;
                Water = water;
            }

            public int this[int[,,,,,] memo] {
                get { return memo[
                    Position, Robot,
                        Water[0],
                        Water[1],
                        Water[2],
                        Water[3]];
                }
                set { memo[
                    Position, Robot,
                        Water[0],
                        Water[1],
                        Water[2],
                        Water[3]] = value; }
            }
        }

        private class Plant : IEnumerable<Pos> {
            public readonly Region Region;
            public readonly int Water;

            private Plant(Region region, int water) {
                Region = region;
                Water = water;
            }

            public Pos this[int index] {
                get {
                    return Region[index];
                }
            }

            public IEnumerator<Pos> GetEnumerator() {
                return Region.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public static Plant MakeBy(string[] garden, Pos origin) {
                var region = Region.MakeBy(garden, origin);
                if (region != null) {
                    return new Plant(region, int.Parse(garden[origin.X][origin.Y].ToString()));
                }
                return null;
            }
        }

        private class Region : IEnumerable<Pos> {
            public readonly IList<Pos> Neighbours;

            private Region(IList<Pos> neighbours) {
                Neighbours = neighbours;
            }

            public Pos this[int index] {
                get {
                    return Neighbours[index];
                }
            }

            public IEnumerator<Pos> GetEnumerator() {
                return Neighbours.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public static Region MakeBy(string[] garden, Pos origin) {
                int n = garden.Length, m = garden[0].Length;
                var neighbours = new List<Pos>();
                for (var k = 0; k < 4; ++k) {
                    var x = origin.X + dx[k];
                    var y = origin.Y + dy[k];
                    if (0 <= x && x < n &&
                        0 <= y && y < m) {
                        if (garden[x][y] == '.' || garden[x][y] == 'R') {
                            neighbours.Add(new Pos(x, y));
                        }
                    }
                }
                if (neighbours.Count > 0) {
                    return new Region(neighbours);
                }
                return null;
            }
        }

        [DebuggerDisplay("({X}, {Y})")]
        private struct Pos {
            public readonly int X;
            public readonly int Y;

            public Pos(int x, int y) {
                X = x;
                Y = y;
            }
        }
    }
}