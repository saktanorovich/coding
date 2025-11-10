using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class VolumeGuess {
        public int correctVolume(string[] queries, int numberOfBoxes, int ithBox) {
            var guess = new List<int>();
            foreach (var query in queries) {
                var data = query.Split(',');
                var box1 = int.Parse(data[0]);
                var box2 = int.Parse(data[1]);
                if (box1 == ithBox || box2 == ithBox) {
                    var volume = int.Parse(data[2]);
                    if (guess.Contains(volume)) {
                        return volume;
                    }
                    guess.Add(volume);
                }
            }
            throw new Exception();
        }
    }
}