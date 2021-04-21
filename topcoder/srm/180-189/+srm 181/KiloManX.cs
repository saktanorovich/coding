using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class KiloManX {
        public int leastShots(string[] damageChart, int[] bossHealth) {
            return leastShots(Array.ConvertAll(damageChart, chart => {
                return Array.ConvertAll(chart.ToCharArray(), damage => int.Parse(damage.ToString()));
            }), bossHealth);
        }

        private int leastShots(int[][] damage, int[] bossHealth) {
            for (var set = 1; set < 1 << bossHealth.Length; ++set) {
                best[set] = -1;
            }
            return leastShots(damage, bossHealth, (1 << bossHealth.Length) - 1);
        }

        private int leastShots(int[][] damage, int[] bossHealth, int set) {
            if (best[set] == -1) {
                best[set] = int.MaxValue;
                var weapons = new List<int>();
                for (var boss = 0; boss < bossHealth.Length; ++boss) {
                    if ((set & (1 << boss)) == 0) {
                        weapons.Add(boss);
                    }
                }
                for (var boss = 0; boss < bossHealth.Length; ++boss) {
                    if ((set & (1 << boss)) != 0) {
                        var shots = bossHealth[boss];
                        foreach (var weapon in weapons) {
                            var health = damage[weapon][boss];
                            if (health > 0) {
                                shots = Math.Min(shots, (bossHealth[boss] + health - 1) / health);
                            }
                        }
                        best[set] = Math.Min(best[set], shots + leastShots(damage, bossHealth, set ^ (1 << boss)));
                    }
                }
            }
            return best[set];
        }

        private static readonly int[] best = new int[1 << 15];
    }
}