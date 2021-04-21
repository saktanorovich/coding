using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1348 {
        public class TweetCounts {
            private readonly Dictionary<string, List<int>> tweets;

            public TweetCounts() {
                tweets = new Dictionary<string, List<int>>();
            }
            
            public void RecordTweet(string tweetName, int time) {
                tweets.TryAdd(tweetName, new List<int>());
                tweets[tweetName].Add(time);
            }

            public IList<int> GetTweetCountsPerFrequency(string freq, string tweetName, int startTime, int endTime) {
                var chunk = GetChunkSize(freq);
                var count = new int[(endTime - startTime) / chunk + 1];
                if (tweets.TryGetValue(tweetName, out var timestamps)) {
                    foreach (var time in timestamps) {
                        if (startTime <= time && time <= endTime) {
                            count[(time - startTime) / chunk] ++;
                        }
                    }
                }
                return count;
            }

            private static int GetChunkSize(string freq) {
                if (freq[0] == 'm') return 60;
                if (freq[0] == 'h') return 60 * 60;
                if (freq[0] == 'd') return 60 * 60 * 24;
                return 1;
            }
        }
    }
}
