using System;

namespace TopCoder.Algorithm {
    public class WaterLevel {
        public double netAmt(int evapNorma, int evapFlood, int[] rain) {
            var waterLevel = 0.0;
            for (var day = 0; day < rain.Length; ++day) {
                if (waterLevel > 0) {
                    var gain = rain[day] - evapFlood;
                    if (gain + waterLevel < 0) {
                        waterLevel = (1 - waterLevel / -gain) * (rain[day] - evapNorma);
                        if (waterLevel > 0)
                            waterLevel = 0;
                    }
                    else waterLevel += gain;
                }
                else {
                    var gain = rain[day] - evapNorma;
                    if (gain + waterLevel > 0) {
                        waterLevel = (1 - waterLevel / -gain) * (rain[day] - evapFlood);
                        if (waterLevel < 0)
                            waterLevel = 0;
                    }
                    else waterLevel += gain;
                }
            }
            return waterLevel;
        }
    }
}