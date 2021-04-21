using System;
using System.Diagnostics;

namespace TopCoder.Algorithm {
    public class ChangeOptimizer {
        public int[] fewestCoins(int[] coinTypes, int value) {
            var coins = new CoinType[coinTypes.Length];
            for (var pos = 0; pos < coinTypes.Length; ++pos) {
                coins[pos] = new CoinType(coinTypes[pos], pos);
            }
            return optimalCoins(coins, value);
        }

        private int[] optimalCoins(CoinType[] coins, int value) {
            Array.Sort(coins);
            best = new int[coins.Length];
            prev = new int[coins.Length];
            for (var i = 0; i < coins.Length; ++i) {
                best[i] = -1;
                prev[i] = -1;
            }
            calc(value, coins, 0);
            var result = new int[coins.Length];
            for (var pos = 0; value > 0; pos = prev[pos]) {
                var take = prev[pos] - 1;
                result[coins[take].Pos] = (value + 1) / coins[take] - 1;
                value = coins[take] - 1;
            }
            return result;
        }

        private int calc(int value, CoinType[] coins, int pos) {
            if (value > 0) {
                if (best[pos] == -1) {
                    best[pos] = int.MaxValue;
                    for (var ix = pos; ix < coins.Length; ++ix) {
                        if ((value + 1) % coins[ix] == 0) {
                            var eval = (value + 1) / coins[ix] - 1 + calc(coins[ix] - 1, coins, ix + 1);
                            if (eval < best[pos]) {
                                best[pos] = eval;
                                prev[pos] = ix + 1;
                            }
                        }
                    }
                }
                return best[pos];
            }
            return 0;
        }

        private int[] best;
        private int[] prev;

        [DebuggerDisplay("Coin = {Coin}, Pos = {Pos}")]
        private struct CoinType : IComparable<CoinType> {
            public readonly int Coin;
            public readonly int Pos;

            public CoinType(int coin, int pos) {
                Coin = coin;
                Pos = pos;
            }

            public int CompareTo(CoinType other) {
                return -1 * Coin.CompareTo(other.Coin);
            }

            public static implicit operator int(CoinType coin) {
                return coin.Coin;
            }
        }
    }
}