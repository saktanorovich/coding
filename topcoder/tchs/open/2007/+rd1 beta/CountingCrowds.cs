using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class CountingCrowds {
        public int minimalInterpretation(string[] sensorData) {
            return minimal(string.Join("", sensorData).Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private static int minimal(string[] sensorData) {
            return sensorData.Sum(data => minimal(data, new HashSet<char>()));
        }

        private static int minimal(string sensorData, HashSet<char> state) {
            if (string.IsNullOrEmpty(sensorData)) {
                return state.Count;
            }
            state.Add(sensorData[0]);
            return state.RemoveWhere(x => x < sensorData[0]) + minimal(sensorData.Substring(1), state);
        }
    }
}
