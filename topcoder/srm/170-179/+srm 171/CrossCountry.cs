using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class CrossCountry {
        public string scoreMeet(int numTeams, string finishOrder) {
            var results = new List<CrossCountryResult>();
            for (var team = 0; team < numTeams; ++team) {
                var score = new List<int>();
                for (var pos = 0; pos < finishOrder.Length; ++pos) {
                    if (finishOrder[pos] == "ABCDEFGHIJ"[team]) {
                        score.Add(pos + 1);
                    }
                }
                if (score.Count >= 5) {
                    results.Add(new CrossCountryResult("ABCDEFGHIJ"[team], score));
                }
            }
            return new string(results.OrderBy(res => res).Select(res => res.Team).ToArray());
        }

        private struct CrossCountryResult : IComparable<CrossCountryResult> {
            public readonly char Team;
            public readonly int[] Score;

            public CrossCountryResult(char team, IEnumerable<int> score) {
                Team = team;
                Score = score.OrderBy(x => x).ToArray();
            }

            public int CompareTo(CrossCountryResult other) {
                if (GetScore() != other.GetScore()) {
                    return GetScore() - other.GetScore();
                }
                if (GetScore(5) != other.GetScore(5)) {
                    return GetScore(5) - other.GetScore(5);
                }
                return Team.CompareTo(other.Team);
            }

            public override string ToString() {
                return string.Format("[{0}]: {1}", Team, Score.Sum());
            }

            private int GetScore() {
                return Score.Take(5).Sum();
            }

            private int GetScore(int pos) {
                if (pos < Score.Length) {
                    return Score[pos];
                }
                return int.MaxValue;
            }
        }
    }
}