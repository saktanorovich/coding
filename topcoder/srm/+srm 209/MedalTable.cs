using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class MedalTable {
        public string[] generate(string[] results) {
            var stat = new Dictionary<string, Country>();
            foreach (var res in results) {
                var items = res.Split(' ');
                for (var i = 0; i < 3; ++i) {
                    var code = items[i];
                    if (stat.ContainsKey(code) == false) {
                        stat.Add(code, new Country(code));
                    }
                    ++stat[code].medals[i];
                }
            }
            return stat.Values.OrderBy(x => x).Select(country => country.ToString()).ToArray();
        }

        private class Country : IComparable<Country> {
            public readonly string code;
            public readonly int[] medals;

            public Country(string code) {
                this.code = code;
                this.medals = new int[3];
            }

            public override string ToString() {
                return string.Format("{0} {1} {2} {3}", code, medals[0], medals[1], medals[2]);
            }

            public int CompareTo(Country other) {
                for (var i = 0; i < 3; ++i) {
                    if (medals[i] > other.medals[i]) {
                        return -1;
                    }
                    if (medals[i] < other.medals[i]) {
                        return +1;
                    }
                }
                return code.CompareTo(other.code);
            }
        }
    }
}
