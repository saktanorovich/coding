using System;

namespace TopCoder.Algorithm {
    public class Dragon {
        public double winFight(int heads, int knights, int probDragon, int probKnight) {
            var pd = probDragon / 100.0;
            var pk = probKnight / 100.0;
            var winKnight = new double[knights + 1, heads + 1];
            winKnight[0, 0] = 1.0;
            for (var k = 1; k <= knights; ++k) {
                winKnight[k, 0] = 1.0;
                for (var h = 1; h <= heads; ++h) {
                    winKnight[k, h] = pk * winKnight[k - 1, h - 1] + (1 - pk) * winKnight[k - 1, h];
                }
            }
            var winDragon = new double[heads + 1, knights + 1];
            for (var h = 1; h <= heads; ++h) {
                winDragon[h, 0] = 1.0;
                for (var k = 1; k <= knights; ++k) {
                    winDragon[h, k] = Math.Max(1 - winKnight[k, h], pd * winDragon[h, k - 1] + (1 - pd) * winDragon[h - 1, k]);
                }
            }
            return 1 - winDragon[heads, knights];
        }
    }
}