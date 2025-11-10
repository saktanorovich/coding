using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class TournamentSchedule {
        public int ways(int numTeams, string[] preferences) {
            var pref = new bool[numTeams - 1][,];
            for (var round = 0; round < numTeams - 1; ++round) {
                pref[round] = new bool[numTeams, numTeams];
            }
            foreach (var s in preferences) {
                var items = s.Split(':', '-');
                var round = int.Parse(items[0]);
                var teamA = int.Parse(items[1]);
                var teamB = int.Parse(items[2]);
                pref[round][teamA, teamB] = true;
                pref[round][teamB, teamA] = true;
            }
            var state = new int[numTeams, numTeams];
            for (var round = 1; round < numTeams; ++round) {
                for (var teamA = 0; teamA < numTeams; ++teamA) {
                    for (var teamB = teamA + 1; teamB < numTeams; ++teamB) {
                        if (pref[round - 1][teamA, teamB]) {
                            if (state[teamA, teamB] > 0) {
                                return 0;
                            }
                            state[teamA, teamB] = round;
                            state[teamB, teamA] = round;
                        }
                    }
                }
            }
            return ways(numTeams, state, 1);
        }

        private static int ways(int numTeams, int[,] state, int round) {
            if (round < numTeams) {
                var result = 0;
                foreach (var next in enumerate(numTeams, state, round, 0)) {
                    result += ways(numTeams, next, round + 1);
                }
                return result;
            }
            return 1;
        }

        private static IEnumerable<int[,]> enumerate(int numTeams, int[,] state, int round, int team) {
            if (team == numTeams) {
                return new List<int[,]> { state };
            }
            for (var opp = 0; opp < numTeams; ++opp) {
                if (state[team, opp] == round) {
                    return enumerate(numTeams, state, round, team + 1);
                }
            }
            var result = new List<int[,]>();
            for (var opp = team + 1; opp < numTeams; ++opp) {
                if (state[team, opp] == 0) {
                    var next = (int[,])state.Clone();
                    next[team, opp] = round;
                    next[opp, team] = round;
                    result.AddRange(enumerate(numTeams, next, round, team + 1));
                }
            }
            return result;
        }
    }
}
