using System;

namespace TopCoder.Algorithm {
    public class ScoringEfficiency {
        public double getPointsPerShot(string[] gameLog) {
            var totalPoints = 0;
            var fieldGoalsAttempted = 0;
            var freeThrowsAttempted = 0;
            foreach (var log in gameLog) {
                switch (log) {
                    case "Made 2 point field goal":   ++fieldGoalsAttempted; totalPoints += 2; break;
                    case "Missed 2 point field goal": ++fieldGoalsAttempted; totalPoints += 0; break;
                    case "Made 3 point field goal":   ++fieldGoalsAttempted; totalPoints += 3; break;
                    case "Missed 3 point field goal": ++fieldGoalsAttempted; totalPoints += 0; break;
                    case "Made free throw":           ++freeThrowsAttempted; totalPoints += 1; break;
                    case "Missed free throw":         ++freeThrowsAttempted; totalPoints += 0; break;
                }
            }
            return totalPoints / (fieldGoalsAttempted + 0.5 * freeThrowsAttempted);
        }
    }
}