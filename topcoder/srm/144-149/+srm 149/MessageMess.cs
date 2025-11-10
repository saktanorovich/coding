using System;
using System.Collections.Generic;

namespace topcoder.algorithm {
      public class MessageMess {
            public string restore(string[] dictionary, string message) {
                  long result = count(dictionary, message);
                  if (result > 0) {
                        if (result > 1) {
                              return ambiguouss;
                        }
                        return build(dictionary, message);
                  }
                  return impossible;
            }

            private long count(string[] dictionary, string message) {
                  if (!cache.ContainsKey(message)) {
                        append(message, 0);
                        if (!string.IsNullOrEmpty(message)) {
                              foreach (string word in dictionary) {
                                    if (message.StartsWith(word)) {
                                          append(message, count(dictionary, message.Substring(word.Length)));
                                    }
                              }
                        }
                        else {
                              append(message, 1);
                        }
                  }
                  return cache[message];
            }

            private string build(string[] dictionary, string message) {
                  if (!string.IsNullOrEmpty(message)) {
                        foreach (string word in dictionary) {
                              if (message.StartsWith(word)) {
                                    if (cache[message.Substring(word.Length)] == 1) {
                                          return string.Format("{0} {1}", word, build(dictionary, message.Substring(word.Length))).Trim();
                                    }
                              }
                        }
                  }
                  return string.Empty;
            }

            private void append(string word, long inc) {
                  if (!cache.ContainsKey(word)) {
                        cache.Add(word, 0);
                  }
                  cache[word] += inc;
            }

            private readonly Dictionary<string, long> cache = new Dictionary<string, long>();
            private readonly string impossible = "IMPOSSIBLE!";
            private readonly string ambiguouss = "AMBIGUOUS!";
      }
}