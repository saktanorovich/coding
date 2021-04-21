using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TopCoder.Algorithm {
    public class NameCheck {
        public string[] formatList(string[] namelist) {
            var result = new List<string>();
            foreach (var name in namelist) {
                var formatted = format(name);
                if (formatted != null) {
                    result.Add(formatted);
                }
            }
            return result.ToArray();
        }

        private static string format(string userName) {
            if (userName.StartsWith(" ") || userName.EndsWith(" ") || userName.EndsWith(".")) {
                return null;
            }
            var parts = userName.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (2 <= parts.Length && parts.Length <= 3) {
                var result = new StringBuilder();
                for (var i = 0; i < parts.Length; ++i) {
                    var name = formatImpl(parts[i]);
                    if (name != null) {
                        result.Append(name);
                        if (i + 1 < parts.Length) {
                            result.Append(" ");
                        }
                    }
                    else return null;
                }
                return result.ToString();
            }
            return null;
        }

        private static string formatImpl(string name) {
            if (Rule.IsMatch(name)) {
                if (name.EndsWith(".")) {
                    if (name.Length > 2) return null;
                }
                else {
                    if (name.Length < 2) return null;
                }
                return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            }
            return null;
        }

        private static readonly Regex Rule = new Regex("^[a-z,A-Z]+[.]?$");
    }
}