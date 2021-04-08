using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using coding.leetcode;

namespace interview {
    public class Program {
        public static void Main(string[] args) {
            var solver = new Solution_1595();
            Console.WriteLine(solver.ConnectTwoGroups(new List<IList<int>> {
                new int[] { 1, 3, 5 },
                new int[] { 4, 1, 1 },
                new int[] { 1, 5, 3 },
            }));
            Console.ReadKey();
        }

        private static void Write<T>(IList<IList<T>> a) {
            for (var i = 0; i < a.Count; ++i) {
                for (var j = 0; j < a[i].Count; ++j) {
                    Console.Write(a[i][j]);
                    if (j + 1 < a[i].Count) {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Write<T>(T[][] a) {
            for (var i = 0; i < a.Length; ++i) {
                for (var j = 0; j < a[i].Length; ++j) {
                    Console.Write(a[i][j]);
                }
                Console.WriteLine();
            }
        }

        private static void Write<T>(IEnumerable<T> a) {
            foreach (var x in a) {
                Console.WriteLine(x);
            }
        }
    }
}