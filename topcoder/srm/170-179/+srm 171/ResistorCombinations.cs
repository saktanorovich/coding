using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class ResistorCombinations {
        public double closestValue(string[] resistances, double target) {
            return closestValue(resistances.Select(double.Parse).ToArray(), target);
        }

        private static double closestValue(double[] resistances, double target) {
            var result = 1e12;
            for (var set = 1; set < 1 << resistances.Length; ++set) {
                var subset = new List<double>();
                for (var i = 0; i < resistances.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        subset.Add(resistances[i]);
                    }
                }
                foreach (var ohms in connect(subset)) {
                    result = closest(result, ohms, target);
                }
            }
            return result;
        }

        private static IEnumerable<double> connect(IList<double> resistances) {
            if (resistances.Count > 1) {
                var result = new List<double>();
                for (var set = 1; set < 1 << resistances.Count; ++set) {
                    var subset1 = new List<double>();
                    var subset2 = new List<double>();
                    for (var i = 0; i < resistances.Count; ++i) {
                        if ((set & (1 << i)) != 0)
                            subset1.Add(resistances[i]);
                        else
                            subset2.Add(resistances[i]);
                    }
                    if (subset1.Count > 0 && subset2.Count > 0) {
                        foreach (var res1 in connect(subset1))
                            foreach (var res2 in connect(subset2)) {
                                result.Add(par(res1, res2));
                                result.Add(ser(res1, res2));
                            }
                    }
                }
                return result;
            }
            return resistances;
        }

        private static double closest(double a, double b, double target) {
            var diffa = Math.Abs(target - a);
            var diffb = Math.Abs(target - b);
            if (diffb.CompareTo(diffa) < 0) {
                return b;
            }
            return a;
        }

        private static double ser(double r1, double r2) {
            return r1 + r2;
        }

        private static double par(double r1, double r2) {
            return r1 * r2 / (r1 + r2);
        }
    }
}