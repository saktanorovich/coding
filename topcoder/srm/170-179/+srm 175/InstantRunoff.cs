using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class InstantRunoff {
        public string outcome(string candidates, string[] ballots) {
            if (!string.IsNullOrWhiteSpace(candidates)) {
                var votes = new int[candidates.Length];
                foreach (var ballot in ballots) {
                    ++votes[candidates.IndexOf(ballot[0])];
                }
                var minimumVotes = votes.Min();
                for (var pos = 0; pos < candidates.Length; ++pos) {
                    if (2 * votes[pos] > ballots.Length) {
                        return candidates[pos].ToString();
                    }
                }
                var next = candidates;
                for (var pos = 0; pos < votes.Length; ++pos) {
                    if (votes[pos] == minimumVotes) {
                        for (var i = 0; i < ballots.Length; ++i) {
                            ballots[i] = ballots[i].Remove(ballots[i].IndexOf(candidates[pos]) , 1);
                        }
                        next = next.Remove(next.IndexOf(candidates[pos]), 1);
                    }
                }
                return outcome(next, ballots);
            }
            return string.Empty;
        }
    }
}