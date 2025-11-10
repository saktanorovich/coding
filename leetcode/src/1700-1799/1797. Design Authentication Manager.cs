using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_1797 {
        public class AuthenticationManager {
            private readonly Dictionary<string, int> tokens;
            private readonly int lifetime;

            public AuthenticationManager(int timeToLive) {
                tokens = new Dictionary<string, int>();
                lifetime = timeToLive;
            }
            
            public void Generate(string tokenId, int currentTime) {
                tokens.TryAdd(tokenId, 0);
                tokens[tokenId] = currentTime + lifetime;
            }
            
            public void Renew(string tokenId, int currentTime) {
                if (tokens.TryGetValue(tokenId, out var expiredAt)) {
                    if (expiredAt > currentTime) {
                        tokens[tokenId] = currentTime + lifetime;
                    }
                }
            }
            
            public int CountUnexpiredTokens(int currentTime) {
                var result = 0;
                foreach (var token in tokens) {
                    if (token.Value > currentTime) {
                        result++;
                    }
                }
                return result;
            }
        }
    }
}
