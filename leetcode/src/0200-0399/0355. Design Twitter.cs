using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Twitter {
        private readonly Dictionary<int, Tweets> ownTweets;
        private readonly Dictionary<int, Tweets> allTweets;
        private readonly Dictionary<int, HashSet<int>> followers;
        private int timestamp;

        /// <summary>
        /// Initialize Twitter data structure here.
        /// </summary>
        public Twitter() {
            ownTweets = new Dictionary<int, Tweets>();
            allTweets = new Dictionary<int, Tweets>();
            followers = new Dictionary<int, HashSet<int>>();
        }

        /// <summary>
        /// Compose a new tweet.
        /// </summary>
        public void PostTweet(int userId, int tweetId) {
            EnsureUser(userId);
            var tweet = new Tweet(userId, tweetId, timestamp);
            timestamp = timestamp + 1;
            ownTweets[userId].Add(tweet);
            allTweets[userId].Add(tweet);
            foreach (var follower in followers[userId]) {
                allTweets[follower].Add(tweet);
            }
        }

        /// <summary>
        /// Retrieve the 10 most recent tweet ids in the user's news feed.
        /// Each item in the news feed must be posted by users who the user
        /// followed or by the user herself. Tweets must be ordered from most
        /// recent to least recent.
        /// </summary>
        public IList<int> GetNewsFeed(int userId) {
            EnsureUser(userId);
            var topTweets = allTweets[userId].Take(10);
            return topTweets.Select(tweet => tweet.tweetId).ToArray();
        }

        /// <summary>
        /// Follower follows a followee. If the operation is invalid,
        /// it should be a no-op.
        /// </summary>
        public void Follow(int followerId, int followeeId) {
            EnsureUser(followerId);
            EnsureUser(followeeId);
            if (followerId == followeeId || followers[followeeId].Contains(followerId)) {
                return;
            }
            followers[followeeId].Add(followerId);
            var tweets = new Tweets();
            var t1 = allTweets[followerId];
            var t2 = ownTweets[followeeId];
            var i1 = 0;
            var i2 = 0;
            while (i1 < t1.Count && i2 < t2.Count) {
                if (t1[i1].timestamp < t2[i2].timestamp) {
                    tweets.Add(t1[i1++]);
                } else {
                    tweets.Add(t2[i2++]);
                }
            }
            while (i1 < t1.Count) tweets.Add(t1[i1++]);
            while (i2 < t2.Count) tweets.Add(t2[i2++]);
            allTweets[followerId] = tweets;
        }

        /// <summary>
        /// Follower unfollows a followee. If the operation is invalid,
        /// it should be a no-op.
        /// </summary>
        public void Unfollow(int followerId, int followeeId) {
            EnsureUser(followerId);
            EnsureUser(followeeId);
            if (followerId == followeeId || followers[followeeId].Contains(followerId) == false) {
                return;
            }
            followers[followeeId].Remove(followerId);
            var tweets = new Tweets();
            for (var i = 0; i < allTweets[followerId].Count; ++i) {
                if (allTweets[followerId][i].userId != followeeId) {
                    tweets.Add(allTweets[followerId][i]);
                }
            }
            allTweets[followerId] = tweets;
        }

        private void EnsureUser(int userId) {
            EnsureUser(userId, ownTweets);
            EnsureUser(userId, allTweets);
            EnsureUser(userId, followers);
        }

        private void EnsureUser<S>(int userId, Dictionary<int, S> store) where S : new() {
            if (store.ContainsKey(userId) == false) {
                store.Add(userId, new S());
            }
        }

        private sealed class Tweet {
            public readonly int userId;
            public readonly int tweetId;
            public readonly int timestamp;

            public Tweet(int userId, int tweetId, int timestamp) {
                this.userId = userId;
                this.tweetId = tweetId;
                this.timestamp = timestamp;
            }
        }

        private sealed class Tweets : IEnumerable<Tweet> {
            private List<Tweet> tweets;

            public Tweets() {
                tweets = new List<Tweet>();
            }

            public int Count {
                get {
                    return tweets.Count;
                }
            }

            public Tweet this[int index] {
                get {
                    return tweets[index];
                }
            }

            public void Add(Tweet tweet) {
                tweets.Add(tweet);
            }

            public IEnumerator<Tweet> GetEnumerator() {
                for (var i = tweets.Count - 1; i >= 0; --i) {
                    yield return tweets[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }
        }
    }
}