using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class CombinationIterator {
        private readonly string characters;
        private readonly int[] state;
        private readonly int length;

        public CombinationIterator(string characters, int length) {
            this.characters = characters;
            this.length = length;
            this.state = new int[length + 1];
            for (var i = 0; i < length; ++i) {
                state[i] = i;
            }
            state[length] = characters.Length;
        }

        public string Next() {
            if (HasNext()) {
                var builder = new StringBuilder();
                for (var i = 0; i < length; ++i) {
                    builder.Append(characters[state[i]]);
                }
                Inc();
                return builder.ToString();
            }
            throw new InvalidOperationException();
        }
        
        public bool HasNext() {
            return state[0] < characters.Length - length + 1;
        }

        private void Inc() {
            var index = length - 1;
            while (index > 0) {
                if (state[index] + 1 < state[index + 1]) {
                    break;
                }
                index = index - 1;
            }
            state[index]++;
            while (index + 1 < length) {
                state[index + 1] = state[index] + 1;
                index = index + 1;
            }
        }
    }
}
