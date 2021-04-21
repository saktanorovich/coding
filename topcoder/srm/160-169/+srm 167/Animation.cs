using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class Animation {
        public string[] animate(int speed, string init) {
            return animate(speed, init.ToCharArray());
        }

        private string[] animate(int speed, char[] init) {
            var result = new List<string>();
            while (possible(init)) {
                result.Add(encode(init));
                init = animate(init, speed);
            }
            result.Add(encode(init));
            return result.ToArray();
        }

        private static char[] animate(char[] init, int speed) {
            var next = "".PadLeft(init.Length, '.').ToCharArray();
            for (var i = 0; i < init.Length; ++i) {
                if ("RD".IndexOf(init[i]) >= 0) {
                    if (0 <= i + speed && i + speed < init.Length)
                        next[i + speed] = next[i + speed] != '.' ? 'D' : 'R';
                }
                if ("LD".IndexOf(init[i]) >= 0) {
                    if (0 <= i - speed && i - speed < init.Length) {
                        next[i - speed] = next[i - speed] != '.' ? 'D' : 'L';
                    }
                }
            }
            return next;
        }

        private static bool possible(char[] init) {
            foreach (var pos in init) {
                if (pos != '.') {
                    return true;
                }
            }
            return false;
        }

        private string encode(char[] init) {
            var stringBuilder = new StringBuilder();
            foreach (var c in init) {
                stringBuilder.Append(c != '.' ? 'X' : '.');
            }
            return stringBuilder.ToString();
        }
    }
}