using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class RandomFA {
        public double getProbability(string[] rulesa, string[] rulesb, string[] rulesc, int finalState, int maxLength) {
            return getProbability(new[] { rulesa, rulesb, rulesc}, rulesa.Length + 1, finalState, maxLength);
        }

        private static double getProbability(string[][] rules, int numOfStates, int finalState, int maxLength) {
            var rfa = new int[numOfStates, 3][];
            for (var state = 0; state < numOfStates; ++state) {
                for (var input = 0; input < 3; ++input) {
                    rfa[state, input] = new int[numOfStates];
                }
            }
            for (var input = 0; input < rules.Length; ++input) {
                for (var curr = 1; curr < numOfStates; ++curr) {
                    foreach (var item in rules[input][curr - 1].Split(' ')) {
                        if (item.IndexOf(':') >= 0) {
                            var data = item.Split(':');
                            var next = int.Parse(data[0]) + 1;
                            var prob = int.Parse(data[1]);
                            rfa[curr, input][next] = prob;
                        }
                    }
                    rfa[curr, input][0] = 100;
                    for (var next = 1; next < numOfStates; ++next) {
                        rfa[curr, input][0] -= rfa[curr, input][next];
                    }
                }
                rfa[0, input][0] = 100;
            }
            return getProbability(rfa, numOfStates, (finalState + 1) % 1000, maxLength);
        }

        private static double getProbability(int[,][] rfa, int numOfStates, int target, int maxLength) {
            var prob = new double[maxLength + 1, numOfStates];
            var chns = new double[maxLength + 1];
            prob[0, 1] = 1.0; chns[0] = 1;
            for (var len = 0; len < maxLength; ++len) {
                for (var curr = 0; curr < numOfStates; ++curr) {
                    for (var input = 0; input < 3; ++input) {
                        for (var next = 0; next < numOfStates; ++next) {
                            prob[len + 1, next] += prob[len, curr] * rfa[curr, input][next] / 300.0;
                        }
                    }
                }
                chns[len + 1] = 3 * chns[len];
            }
            var total = 0.0;
            for (var len = 0; len <= maxLength; ++len) {
                total += prob[len, target] * chns[len] / chns.Sum();
            }
            return total;
        }
    }
}