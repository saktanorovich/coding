using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0535 {
        public class Codec {
            private readonly string tinybase = "https://tinyurl.com/";
            private readonly string alphabet =
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789";
            private readonly Dictionary<string, string> store;

            public Codec() {
                store = new Dictionary<string, string>();
            }

            // Encodes an URL to a shortened URL
            public string encode(string longUrl) {
                var url = new StringBuilder();
                using (var sha256 = System.Security.Cryptography.SHA256.Create()) {
                    var sha2 = sha256.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
                    var hash = 0L;
                    for (var i = 0; i < 6; ++i) {
                        hash = (hash << 8) | sha2[i];
                    }
                    for (var i = 0; i < 8; ++i) {
                        var c = alphabet[(int)(hash % alphabet.Length)];
                        url.Append(c);
                        hash /= alphabet.Length;
                    }
                    store.Add(url.ToString(), longUrl);
                }
                return tinybase + url.ToString();
            }

            // Decodes a shortened URL to its original URL
            public string decode(string tinyUrl) {
                var hash = tinyUrl.Substring(tinybase.Length);
                if (store.TryGetValue(hash, out var url)) {
                    return url;
                }
                return String.Empty;
            }
        }
    }
}
