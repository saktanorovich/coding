using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
    public class TennisRallies {
        public int howMany(int numLength, string[] forbidden, int allowed) {
            List<string> hittingSequences = new List<string>(new string[] { "" });
            for (int length = 1; length <= numLength; ++length) {
                List<string> next = new List<string>();
                foreach (string sequence in hittingSequences) {
                    next.Add(sequence + "c");
                    next.Add(sequence + "d");
                }
                hittingSequences = next;
            }
            int result = 0;
            foreach (string sequence in hittingSequences) {
                if (accepted(sequence, forbidden, allowed)) {
                    ++result;
                }
            }
            return result;
        }

        private bool accepted(string sequence, string[] forbidden, int allowed) {
            int count = 0;
            foreach (string current in forbidden) {
                for (int i = 0; i + current.Length <= sequence.Length; ++i) {
                    if (match(sequence, current, i)) {
                        ++count;
                        if (count >= allowed) {
                            return false;
                        }
                    }
                }
            }
            return count < allowed;
        }

        private bool match(string text, string pattern, int index) {
            if (index + pattern.Length <= text.Length) {
                for (int i = 0; i < pattern.Length; ++i) {
                    if (text[index + i] != pattern[i]) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}