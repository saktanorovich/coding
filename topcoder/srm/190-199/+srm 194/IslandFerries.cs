using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TopCoder.Algorithm {
    public class IslandFerries {
        public int[] costs(string[] legs, string[] prices) {
            return costs(legs, prices, legs.Length, prices.Length);
        }

        private static int[] costs(string[] legs, string[] prices, int numOfFerries, int numOfIslands) {
            var graph = new bool[numOfFerries, numOfIslands, numOfIslands];
            var price = new int[numOfIslands, numOfFerries];
            for (var ferry = 0; ferry < numOfFerries; ++ferry) {
                foreach (var leg in legs[ferry].Split(' ')) {
                    var data = leg.Split('-');
                    var src = int.Parse(data[0]);
                    var dst = int.Parse(data[1]);
                    graph[ferry, src, dst] = true;
                }
            }
            for (var island = 0; island < numOfIslands; ++island)
                for (var ferry = 0; ferry < numOfFerries; ++ferry) {
                    price[island, ferry] = int.MaxValue / 2;
                }
            for (var island = 0; island < numOfIslands; ++island) {
                var ferryPrices = Array.ConvertAll(prices[island].Split(' '), int.Parse);
                for (var ferry = 0; ferry < numOfFerries; ++ferry) {
                    price[island, ferry] = ferryPrices[ferry];
                }
            }
            return costs(graph, price, numOfFerries, numOfIslands);
        }

        private static int[] costs(bool[,,] graph, int[,] price, int numOfFerries, int numOfIslands) {
            var best = new int[numOfIslands, 1 << numOfFerries];
            for (var island = 0; island < numOfIslands; ++island)
                for (var tickets = 0; tickets < 1 << numOfFerries; ++tickets) {
                    best[island, tickets] = int.MaxValue / 2;
                }
            var queue = new Queue<State>();
            for (queue.Enqueue(new State()); queue.Count > 0; queue.Dequeue()) {
                var island = queue.Peek().Island;
                var tickets = queue.Peek().Tickets;
                var spent = queue.Peek().Spent;
                if (best[island, tickets] > spent) {
                    best[island, tickets] = spent;
                    if (cardinality(tickets) < 3) {
                        for (var ferry = 0; ferry < numOfFerries; ++ferry) {
                            if (!contains(tickets, ferry)) {
                                queue.Enqueue(new State(island, tickets | (1 << ferry), spent + price[island, ferry]));
                            }
                        }
                    }
                    for (var dst = 0; dst < numOfIslands; ++dst) {
                        for (var ferry = 0; ferry < numOfFerries; ++ferry)
                            if (contains(tickets, ferry)) {
                                if (graph[ferry, island, dst]) {
                                    var next = tickets ^ (1 << ferry);
                                    if (best[dst, next] > spent) {
                                        queue.Enqueue(new State(dst, next, spent));
                                    }
                                }
                            }
                    }
                }
            }
            var result = new List<int>();
            for (var island = 1; island < numOfIslands; ++island) {
                var min = int.MaxValue / 2;
                for (var tickets = 0; tickets < 1 << numOfFerries; ++tickets) {
                    min = Math.Min(min, best[island, tickets]);
                }
                if (min < int.MaxValue / 2)
                    result.Add(min);
                else
                    result.Add(-1);
            }
            return result.ToArray();
        }

        private static bool contains(int set, int x) {
            return (set & (1 << x)) != 0;
        }

        private static int cardinality(int set) {
            if (set > 0) {
                return 1 + cardinality(set & (set - 1));
            }
            return 0;
        }

        [DebuggerDisplay("Island = {Island}, Tickets = {Tickets}, Spent = {Spent}")]
        private struct State {
            public readonly int Island;
            public readonly int Tickets;
            public readonly int Spent;

            public State(int island, int tickets, int spent) {
                Island = island;
                Tickets = tickets;
                Spent = spent;
            }
        }
    }
}