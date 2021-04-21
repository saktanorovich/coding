using System;

namespace TopCoder.Algorithm {
    public class PointSystem {
        public double oddsOfWinning(int pointsToWin, int pointsToWinBy, int skill) {
            return oddsOfWinning(2000, pointsToWin, pointsToWinBy, skill);
        }

        private double oddsOfWinning(int scores, int pointsToWin, int pointsToWinBy, int skil) {
            var juniorProb = 1.0 * skil / 100;
            var seniorProb = 1.0 - juniorProb;
            var prob = new double[scores + 1, scores + 1]; prob[0, 0] = 1;
            for (var senior = 0; senior < scores; ++senior)
                for (var junior = 0; junior < scores; ++junior) {
                    if (senior >= pointsToWin && senior - junior >= pointsToWinBy) continue;
                    if (junior >= pointsToWin && junior - senior >= pointsToWinBy) continue;

                    prob[senior + 1, junior] += seniorProb * prob[senior, junior];
                    prob[senior, junior + 1] += juniorProb * prob[senior, junior];
                }
            var result = 0.0;
            for (var junior = 0; junior <= scores; ++junior) {
                if (junior >= pointsToWin) {
                    for (var senior = 0; senior + pointsToWinBy <= junior; ++senior) {
                        result += prob[senior, junior];
                    }
                }
            }
            return result;
        }
    }
}