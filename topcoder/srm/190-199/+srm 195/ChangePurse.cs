using System;
using System.Diagnostics;

namespace TopCoder.Algorithm {
    public class ChangePurse {
        public int[] optimalCoins(int[] coinTypes, int value) {
            var coins = new CoinType[coinTypes.Length];
            for (var pos = 0; pos < coinTypes.Length; ++pos) {
                coins[pos] = new CoinType(coinTypes[pos], pos);
            }
            return optimalCoins(coins, value);
        }

        private static int[] optimalCoins(CoinType[] coins, int value) {
            /* Assume we have a solution (A1, A2, .., Ak). This solution should satisfy the following conditions:
             *   (*) A1 * C1 + A2 * C2 + .. + Ak-1 * Ck-1 = Ck - 1 because we should represent any value between 1 and Ck - 1;
             *   (*) Ck evenly divide (value + 1).
             */ 
            Array.Sort(coins);
            var result = new int[coins.Length];
            for (var i = 0; i < coins.Length; ++i) {
                if ((value + 1) % coins[i].Coin == 0) {
                    result[coins[i].Pos] = (value + 1) / coins[i].Coin - 1;
                    value = coins[i].Coin - 1;
                }
            }
            return result;
        }

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
        }
    }
}