using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1912 {
        public class MovieRentingSystem {
            private readonly SortedDictionary<int, SortedSet<(int shop, int price)>> movies;
            private readonly SortedSet<(int shop, int movie, int price)> rented;
            private readonly Dictionary<int, Dictionary<int, int>> prices;

            public MovieRentingSystem(int n, int[][] entries) {
                prices = new Dictionary<int, Dictionary<int, int>>();
                movies = new SortedDictionary<int, SortedSet<(int shop, int price)>>();
                rented = new SortedSet<(int, int, int)>(
                    Comparer<(int, int, int)>.Create(((int, int, int) x, (int, int, int) y) => {
                        if (x.Item3 != y.Item3)
                            return x.Item3 - y.Item3;
                        if (x.Item1 != y.Item1)
                            return x.Item1 - y.Item1;
                        else
                            return x.Item2 - y.Item2;
                    }));
                foreach (var e in entries) {
                    movies.TryAdd(e[1], new SortedSet<(int, int)>(
                        Comparer<(int, int)>.Create(((int, int) x, (int, int) y) => {
                            if (x.Item2 != y. Item2)
                                return x.Item2 - y.Item2;
                            else
                                return x.Item1 - y.Item1;
                        })));
                    movies[e[1]].Add((e[0], e[2]));
                    prices.TryAdd(e[1], new Dictionary<int, int>());
                    prices[e[1]][e[0]] = e[2];
                }
            }

            public IList<int> Search(int movie) {
                if (movies.TryGetValue(movie, out var shops)) {
                    return shops.Take(5).Select(e => e.shop).ToList();
                }
                return new int[0];
            }

            public void Rent(int shop, int movie) {
                var price = prices[movie][shop];
                rented.Add((shop, movie, price));
                movies[movie].Remove((shop, price));
            }

            public void Drop(int shop, int movie) {
                var price = prices[movie][shop];
                rented.Remove((shop, movie, price));
                movies[movie].Add((shop, price));
            }

            public IList<IList<int>> Report() {
                return rented.Take(5).Select(e => (IList<int>)new[] { e.shop, e.movie }).ToList();
            }
        }
    }
}