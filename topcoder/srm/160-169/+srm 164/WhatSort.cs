using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class WhatSort {
        public string sortType(string[] name, int[] age, int[] weight) {
            var objects = new List<IComparable[]>();
            for (var i = 0; i < name.Length; ++i) {
                objects.Add(new IComparable[] {
                    name[i], age[i], weight[i]
                });
            }
            return sortType(objects);
        }

        private static string sortType(IList<IComparable[]> objects) {
            var match = new List<Attribute[]>();
            foreach (var permutation in PermuteUtils.Permute(Attribute.Attributes)) {
                if (sortedBy(objects, permutation)) {
                    match.Add(permutation);
                }
            }
            if (match.Count > 0) {
                if (match.Count == 1) {
                    return string.Join("", match[0]);
                }
                return "IND";
            }
            return "NOT";
        }

        private static bool sortedBy(IList<IComparable[]> objects, Attribute[] attributes) {
            return sortedBy(objects, attributes, 0, objects.Count - 1, 0);
        }

        private static bool sortedBy(IList<IComparable[]> objects, Attribute[] attributes, int lo, int hi, int at) {
            if (at < attributes.Length) {
                var lastIx = lo;
                for (var i = lo + 1; i <= hi; ++i) {
                    var comparison = attributes[at].CompareTo(objects[i - 1], objects[i]);
                    if (comparison <= 0) {
                        if (comparison != 0) {
                            if (!sortedBy(objects, attributes, lastIx, i - 1, at + 1)) {
                                return false;
                            }
                            lastIx = i;
                        }
                    }
                    else return false;
                }
                return sortedBy(objects, attributes, lastIx, hi, at + 1);
            }
            return true;
        }

        private struct Attribute {
            public static readonly Attribute[] Attributes = {
                  new Attribute("N", 0, +1),
                  new Attribute("A", 1, +1),
                  new Attribute("W", 2, -1)
              };

            private readonly string _name;
            private readonly int _seqno;
            private readonly int _order;

            private Attribute(string name, int seqno, int order) {
                _name = name;
                _seqno = seqno;
                _order = order;
            }

            public int CompareTo(IComparable[] obj1, IComparable[] obj2) {
                return obj1[_seqno].CompareTo(obj2[_seqno]) * _order;
            }

            public override string ToString() {
                return _name;
            }
        }

        private static class PermuteUtils {
            public static IList<T[]> Permute<T>(T[] list) {
                var result = new List<T[]>();
                if (list.Length == 0) {
                    result.Add(new T[0]);
                }
                else {
                    for (var i = 0; i < list.Length; ++i) {
                        foreach (var res in Permute(Remove(list, i))) {
                            result.Add(Concat(list[i], res));
                        }
                    }
                }
                return result.ToArray();
            }

            private static T[] Concat<T>(T item, T[] list) {
                var result = new List<T>(new[] { item });
                result.AddRange(list);
                return result.ToArray();
            }

            private static T[] Remove<T>(T[] list, int index) {
                var result = new List<T>(list);
                result.RemoveAt(index);
                return result.ToArray();
            }
        }
    }
}